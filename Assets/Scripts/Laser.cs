using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8f;
    [SerializeField]
    private float _cleanupHeight = 8f;

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        Cleanup();
    }

    void CalculateMovement()
    {
        // Translate laser up
        Vector3 direction = Vector3.up;
        transform.Translate(_speed * Time.deltaTime * direction);
    }

    void Cleanup()
    {
        if (transform.position.y > _cleanupHeight)
        {
            Destroy(this.gameObject);
        }
    }
}
