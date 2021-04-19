using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class represents a pickup object that adds a speed bost to the player.
public class Turbo : PickUp, IPickUp
{

    // Update is called once per frame
    void FixedUpdate()
    {
    }

    override public void DoAction()
    {
       base.DoAction();
       myKart.AddTurbo();
    }
   
    void IPickUp.PickedUp()
    {
        DoAction();
    }
}
