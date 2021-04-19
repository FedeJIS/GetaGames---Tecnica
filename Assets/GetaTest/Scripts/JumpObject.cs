using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class is used as a PickUp to force the player jumps. Is not implemented in game.
public class JumpObject : PickUp, IPickUp
{
    float jumpForce = 100;
    override public void DoAction()
    {
       Debug.Log("Jump jump!");
       myKart.ForceJump(jumpForce);
    }
   
    void IPickUp.PickedUp()
    {
        DoAction();
    }
}
