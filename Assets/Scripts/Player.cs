using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Parts of a variable
    // 0 attribute
    // 1 public or private reference
    // 2 data type (int, float, bool, and strings)
    // 3 variable name. Standard is to underscore private variables
    // (4) optional value assigned
    [SerializeField]
    private float _speed = 5f;
    [SerializeField]
    private float _topPositionLimit = 0f;
    [SerializeField]
    private float _bottomPositionLimit = -3.8f;
    [SerializeField]
    private float _sidePositionLimit = 11.3f;
    [SerializeField]
    private GameObject _laserContainer;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleLaserPrefab;
    [SerializeField]
    private Vector2 _laserSpawnOffset = new(0, 1.0f);
    [SerializeField]
    private float _laserFireRate = 0.15f;
    private float _laserCanFire = -1;
    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private float _powerupCooldownTime = 5f;
    private List<float> _tripleLaserActive = new(); // Currently the number value does nothing. We just check length
    private List<float> _speedPowerupActive = new(); // Currently the number value does nothing. We just check length
    private List<float> _shieldPowerupActive = new(); // Currently the number value does nothing. We just check length
    [SerializeField]
    private float _speedPowerupMultiplier = 2.0f;
    [SerializeField]
    private GameObject _shieldVisualizer;
    private int _score = 0;
    private UIManager _uiManager;


    private SpawnManager _spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        // Zero player position on start
        // transform.position = Vector3.zero;

        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is null");
        }

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_uiManager == null)
        {
            Debug.LogError("The UI Manager is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        TryFireLaser();
    }

    void CalculateMovement()
    {
        // FYI: Keyboard is not on, off. It ramps up/down this value
        // Controller is instant
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        float speedPowerupMultiplier = 1f;
        if (_speedPowerupActive.Count > 0)
        {
            speedPowerupMultiplier = _speedPowerupMultiplier;
        }

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(_speed * speedPowerupMultiplier * Time.deltaTime * direction);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, _bottomPositionLimit, _topPositionLimit), 0);

        // Wrap the player around the side
        if (transform.position.x > _sidePositionLimit)
        {
            transform.position = new Vector3(-_sidePositionLimit, transform.position.y, 0);
        }
        else if (transform.position.x < -_sidePositionLimit)
        {
            transform.position = new Vector3(_sidePositionLimit, transform.position.y, 0);
        }
    }

    void TryFireLaser()
    {
        // Check player input
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse0))
        {
            // Check fire cooldown
            if (Time.time > _laserCanFire)
            {
                _laserCanFire = Time.time + _laserFireRate;
                FireLaser();
            }
        }
    }

    void FireLaser()
    {
        // If tripleLaser active, fire triple
        if (_tripleLaserActive.Count > 0)
        {
            // DON'T user _laserSpawnOffset
            GameObject newTripleLaser = Instantiate(_tripleLaserPrefab, transform.position, Quaternion.identity);
            newTripleLaser.transform.SetParent(_laserContainer.transform);
        }
        else
        {
            // Use _laserSpawnOffset
            Vector3 spawnOffset = new(_laserSpawnOffset.x, _laserSpawnOffset.y, 0);
            // Quaternion.identity = default rotation
            GameObject newLaser = Instantiate(_laserPrefab, transform.position + spawnOffset, Quaternion.identity);
            newLaser.transform.SetParent(_laserContainer.transform);
        }
    }

    public void Damage()
    {
        if (_shieldPowerupActive.Count() > 0)
        {
            // To Allow more than 1 shield stored
            _shieldPowerupActive = _shieldPowerupActive.Skip(1).ToList();
            // Or Only 1 shield
            // _shieldPowerupActive = new List<float>();
            _shieldVisualizer.SetActive(false);
            return;
        }
        else
        {
            // Remove 1 life
            _lives--;
        }

        // Check if dead
        if (_lives <= 0)
        {
            // Communicate with Spawn Manager
            // Let them know to stop spawning
            _spawnManager.StopSpawning();

            Destroy(this.gameObject);
        }
    }

    // Triple Laser Powerup
    public void EnableTripleLaserPowerup()
    {
        _tripleLaserActive.Add(_powerupCooldownTime); // Currently the number value does nothing. We just check length
        // start the power down coroutine for triple laser
        StartCoroutine(TripleLaserPowerDownRoutine(_powerupCooldownTime));
    }

    // IEnumerator TripleLaserPowerDownRoutine()
    // Wait 5 seconds
    // set _tripleLaserActive to false
    IEnumerator TripleLaserPowerDownRoutine(float cooldownTime)
    {
        yield return new WaitForSeconds(cooldownTime);
        _tripleLaserActive = _tripleLaserActive.Skip(1).ToList();
    }

    // Speed Powerup
    public void EnableSpeedPowerup()
    {
        _speedPowerupActive.Add(_powerupCooldownTime); // Currently the number value does nothing. We just check length
        StartCoroutine(SpeedShutDownRoutine(_powerupCooldownTime));
    }

    IEnumerator SpeedShutDownRoutine(float cooldownTime)
    {
        yield return new WaitForSeconds(cooldownTime);
        _speedPowerupActive = _speedPowerupActive.Skip(1).ToList();
    }

    // Shield Powerup
    public void EnableShieldPowerup()
    {
        _shieldPowerupActive.Add(_powerupCooldownTime); // Currently the number value does nothing. We just check length
        _shieldVisualizer.SetActive(true);
    }

    // Add 10 to score
    // Communicate with UI to update score
    public void AddScore(int points)
    {
        _score += points;
        // Update UI
        _uiManager.UpdateScore(_score);
    }
}
