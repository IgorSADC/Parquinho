using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseRotation : MonoBehaviour
{
    
    public float amplitude = 0.05f;
    

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + Random.Range(-amplitude*2, amplitude*2)) * Random.Range(1-amplitude, 1+amplitude) );
    }
}
