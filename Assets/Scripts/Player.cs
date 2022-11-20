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
    private GameObject _laserPrefab;

    // Start is called before the first frame update
    void Start()
    {
        // Take the current position = new position(0, 0, 0)
        transform.position = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0))
        {
            // Quaternion.identity = default rotation
            Instantiate(_laserPrefab, transform.position, Quaternion.identity);
        }
    }

    void CalculateMovement()
    {
        // FYI: Keyboard is not on, off. It ramps up/down this value
        // Controller is instant
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(_speed * Time.deltaTime * direction);

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
}
