using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorScript : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(30f*Time.deltaTime, 30f * Time.deltaTime, 0);

        transform.position += Vector3.right * Time.deltaTime * 4;

        if(transform.position.x >= 50) { Destroy(gameObject); }
    }
}
