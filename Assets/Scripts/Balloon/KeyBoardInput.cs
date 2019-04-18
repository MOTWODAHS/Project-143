using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBoardInput : TapableObject3D
{
    public KeyBoardController controller;
    public string inputString;

    public AudioSource keyboardAudio;

    public AudioClip tapSound1;
    public AudioClip tapSound2;

    public override void OnTap()
    {
        controller.AddInput(inputString);
        if(Random.Range(0,1f) <= 0.5f)
        {
            keyboardAudio.PlayOneShot(tapSound1);
        }
        else
        {
            keyboardAudio.PlayOneShot(tapSound2);
        }
    }

}
