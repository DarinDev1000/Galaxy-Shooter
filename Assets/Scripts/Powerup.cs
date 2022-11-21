using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;
    [SerializeField]
    private float _spawnHeight = 8f;
    [SerializeField]
    private float _cleanupHeight = -5f;

    // Update is called once per frame
    void Update()
    {

        CalculateMovement();

    }

    void CalculateMovement()
    {
        // Move down at speed
        // When we leave screen, destroy this object

        // Translate powerup down
        Vector3 direction = Vector3.down;
        transform.Translate(_speed * Time.deltaTime * direction);

        if (transform.position.y < _cleanupHeight)
        {
            Destroy(gameObject);
        }
    }

    // OnTriggerCollision
    // Only be collectable by the player (use tags)
    // on collected, destroy
    void OnTriggerEnter2D(Collider2D other)
    {
        // If other is player, destroy us and damage player
        if (other.CompareTag("Player"))
        {
            // Give player powerup
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.EnableTripleLaserPowerup();
            }

            // Make sure to do everything else before destroying self
            Destroy(this.gameObject);
        }
    }
}
