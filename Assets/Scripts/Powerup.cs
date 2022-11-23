using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;
    [SerializeField]
    private float _cleanupHeight = -5f;

    // ID for powerups
    // 0 = TripleLaser
    // 1 = Speed
    // 2 = Shields
    [SerializeField]
    private PowerupEnum _powerupType;

    public enum PowerupEnum
    {
        TripleLaser,
        Speed,
        Shields
    }

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
                // Switch powerup type
                switch (_powerupType)
                {
                    case PowerupEnum.TripleLaser:
                        player.EnableTripleLaserPowerup();
                        break;
                    case PowerupEnum.Speed:
                        player.EnableSpeedPowerup();
                        break;
                    case PowerupEnum.Shields:
                        player.EnableShieldPowerup();
                        break;
                    default:
                        Debug.LogError("Default Case Powerup");
                        break;
                }
            }

            // Make sure to do everything else before destroying self
            Destroy(this.gameObject);
        }
    }
}
