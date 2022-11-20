using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rand : MonoBehaviour
{
     private float nextActionTime = 0f;
    public float period = 5f;
    
    void Update () {
        if (Time.time > nextActionTime ) {
            nextActionTime = Time.time + period;

            randomAPosition();
        }
    }
    
    void randomAPosition()
    {
        float x = Random.Range(-48.54f, 48.45f);
        float z = Random.Range(-49.22f, 49.2f);
        transform.position = new Vector3(x, 0, z);
    }
}