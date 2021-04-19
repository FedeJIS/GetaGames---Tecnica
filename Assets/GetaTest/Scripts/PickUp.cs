using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class is used as a blueprint for Pickups.
public abstract class PickUp: MonoBehaviour, IPickUp
{

   protected GameModeManager myManager;
   protected CarController myKart;
   protected AudioSource myAudioSrc;
   protected BoxCollider myCollider;
   public GameObject myParticles;

   virtual protected void Start() {
        myManager = GameObject.FindObjectOfType<GameModeManager>();
        myKart = GameObject.FindGameObjectWithTag("Player").GetComponent<CarController>();
        myAudioSrc = GetComponent<AudioSource>();
        myCollider = GetComponent<BoxCollider>();
   }

   virtual public void DoAction()
   {
       if(myManager != null){
          if(myAudioSrc!= null) myAudioSrc.Play();
          if(myParticles != null) Instantiate(myParticles,transform.position,transform.rotation);
       }
    }
   
    void IPickUp.PickedUp()
    {
        DoAction();
    }

}