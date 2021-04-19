using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class makes an object rotate. 
public class Rotate : MonoBehaviour
{
 
    public float amount = 5;
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(0,amount,0);
    }
}
