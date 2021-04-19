using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class is used as a PickUp to make the player lose control in a period of time.
public class OilSplash : PickUp, IPickUp
{

    // Update is called once per frame
    void FixedUpdate()
    {
    }

    override public void DoAction()
    {
       base.DoAction();
       myKart.LoseControl();
    }
   
    void IPickUp.PickedUp()
    {   
        DoAction();
    }
}
