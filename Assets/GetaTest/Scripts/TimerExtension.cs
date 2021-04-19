using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class represents a pickup object that adds time to the counter.
public class TimerExtension : PickUp, IPickUp
{
    [SerializeField]
    private int timeToAdd = 5;

    // User Inputs
    public float degreesPerSecond = 15.0f;
    public float amplitude = 0.5f;
    public float frequency = 1f;
 
    // Position Storage Variables
    Vector3 posOffset = new Vector3 ();
    Vector3 tempPos = new Vector3 ();
    void IPickUp.PickedUp()
    {
        DoAction();
    }

    private void Awake() {
        // Store the starting position & rotation of the object
        posOffset = transform.position;
    }

    override public void DoAction()
    {
        if(myManager != null){
            myManager.AddTime(timeToAdd);
            base.DoAction();
            Destroy(gameObject,myAudioSrc.clip.length/2);
       }
    }
 
 
     
    // Update is called once per frame
    void FixedUpdate () {
        // Spin object around Y-Axis
        transform.Rotate(new Vector3(0f, Time.deltaTime * degreesPerSecond, 0f), Space.World);
        // Float up/down with a Sin()
        tempPos = posOffset;
        tempPos.y += Mathf.Sin (Time.fixedTime * Mathf.PI * frequency) * amplitude;
        transform.position = tempPos;
    }
}