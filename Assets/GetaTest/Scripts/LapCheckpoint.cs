using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class is used as a collision checker to notify the LapRegister.
public class LapCheckpoint : MonoBehaviour
{
    private bool reached = false;
    public LapRegister myLapRegister;
    private void Awake() {
        myLapRegister = GetComponentInParent<LapRegister>();
    }
    private void OnTriggerEnter(Collider other) {
        if(!reached && other.gameObject.CompareTag("Player"))
        {
            reached = true;
            myLapRegister.AddCheckpoint();
        }
    }

    public void Restore()
    {
        if(reached) reached = false;
    }
}
