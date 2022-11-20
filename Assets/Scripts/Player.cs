using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Take the current position = new position(0, 0, 0)
        transform.position = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(5 * Time.deltaTime * Vector3.forward);
    }
}
