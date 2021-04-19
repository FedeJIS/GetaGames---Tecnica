using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This class is in charge of handling Audio clips in Game.
public class AudioManager : MonoBehaviour
{
    public AudioClip[] clips;
    AudioSource myAudioSrc;

    private void Awake() {
        myAudioSrc = GetComponent<AudioSource>();
    }
    public void PlayClip(int num)
    {
        if(num > 0 && num < clips.Length)
        {
            myAudioSrc.Stop();
            myAudioSrc.clip = clips[num];
            myAudioSrc.Play();
        }
    }
}
