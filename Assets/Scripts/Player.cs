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
    private float _speed = 3.5f;

    // Start is called before the first frame update
    void Start()
    {
        // Take the current position = new position(0, 0, 0)
        transform.position = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(_speed * Time.deltaTime * Vector3.right);
    }
}
