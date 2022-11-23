using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // private IEnumerator coroutine;
    [SerializeField]
    private float _spawnWidth = 8f;

    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private float _spawnTimer = 3f;
    [SerializeField]
    private float _enemySpawnHeight = 8f;

    [SerializeField]
    private GameObject _powerupContainer;
    [SerializeField]
    private GameObject _tripleLaserPowerupPrefab;
    [SerializeField]
    private GameObject _speedPowerupPrefab;
    [SerializeField]
    private float _powerupSpawnHeight = 8f;
    [SerializeField]
    private float _powerupSpawnCooldownMin = 3f;
    [SerializeField]
    private float _powerupSpawnCooldownMax = 7f;

    private bool _spawningActive = true;

    // Start is called before the first frame update
    void Start()
    {
        // coroutine = SpawnRoutine();
        // StartCoroutine(coroutine);
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    // Create a coroutine of type IEnumerator -- Yield Events
    // infinite while loop
    IEnumerator SpawnEnemyRoutine()
    {
        yield return null; // wait 1 frame

        // Instantiate enemy prefab
        // Yield wait for 5 seconds
        while (_spawningActive)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-_spawnWidth, _spawnWidth), _enemySpawnHeight, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.SetParent(_enemyContainer.transform);
            yield return new WaitForSeconds(_spawnTimer);
        }
        // After this never runs
    }

    IEnumerator SpawnPowerupRoutine()
    {
        // Every 3 - 7 seconds, spawn in a powerup
        while (_spawningActive)
        {
            // Spawn powerup
            int powerupId = new System.Random().Next(0, 2);
            GameObject powerupPrefab;
            switch (powerupId)
            {
                case 0:
                    powerupPrefab = _tripleLaserPowerupPrefab;
                    break;
                case 1:
                    powerupPrefab = _speedPowerupPrefab;
                    break;
                default:
                    Debug.LogError("Default Case Spawner");
                    powerupPrefab = _tripleLaserPowerupPrefab;
                    break;
            }
            Vector3 posToSpawn = new Vector3(Random.Range(-_spawnWidth, _spawnWidth), _powerupSpawnHeight, 0);
            GameObject newPowerup = Instantiate(powerupPrefab, posToSpawn, Quaternion.identity);
            newPowerup.transform.SetParent(_powerupContainer.transform);

            // Wait to spawn next powerup
            float cooldownTimer = Random.Range(_powerupSpawnCooldownMin, _powerupSpawnCooldownMax);
            yield return new WaitForSeconds(cooldownTimer);
        }
    }

    public void StartSpawning()
    {
        _spawningActive = true;
    }

    public void StopSpawning()
    {
        _spawningActive = false;
    }
}
