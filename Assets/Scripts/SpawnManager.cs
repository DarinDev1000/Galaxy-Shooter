using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // private IEnumerator coroutine;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private float _spawnTimer = 3f;
    [SerializeField]
    private float _enemySpawnHeight = 8f;

    private bool _spawningActive = true;

    // Start is called before the first frame update
    void Start()
    {
        // coroutine = SpawnRoutine();
        // StartCoroutine(coroutine);
        StartCoroutine(SpawnRoutine());
    }

    // Create a coroutine of type IEnumerator -- Yield Events
    // infinite while loop
    IEnumerator SpawnRoutine()
    {
        yield return null; // wait 1 frame

        // Instantiate enemy prefab
        // Yield wait for 5 seconds
        while (_spawningActive)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), _enemySpawnHeight, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.SetParent(_enemyContainer.transform);
            yield return new WaitForSeconds(_spawnTimer);
        }
        // After this never runs
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
