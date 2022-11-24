using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;
    [SerializeField]
    private float _spawnHeight = 8f;
    private Player _player;

    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

    }

    void CalculateMovement()
    {
        // Move down at 4 m/sec
        // Check for if bottom of screen, respawn at top
        // with a new random x position

        // Translate enemy down
        Vector3 direction = Vector3.down;
        transform.Translate(_speed * Time.deltaTime * direction);

        if (transform.position.y < -5f)
        {
            // Random.Range(-8, 9) int max value exclusive
            // Random.Range(-8f, 8f) float max value inclusive
            float randomX = Random.Range(-8f, 8f);
            transform.position = new Vector3(randomX, _spawnHeight, 0);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // If other is player, destroy us and damage player
        if (other.CompareTag("Player"))
        {
            // Damage player first
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
            }

            // Make sure to do everything else before destroying self
            Destroy(this.gameObject);
        }

        // If other is laser, destroy us and destroy laser
        if (other.CompareTag("Laser"))
        {
            Destroy(other.gameObject);
            // Add 10 to player score
            if (_player != null)
            {
                _player.AddScore(10);
            }
            // Make sure to do everything else before destroying self
            Destroy(this.gameObject);
        }
    }
}
