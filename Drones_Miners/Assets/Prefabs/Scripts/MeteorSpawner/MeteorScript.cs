using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorScript : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(0.01f, 0.01f, 0);

        transform.position += Vector3.right * 0.02f;

        if(transform.position.x >= 50) { Destroy(gameObject); }
    }
}
