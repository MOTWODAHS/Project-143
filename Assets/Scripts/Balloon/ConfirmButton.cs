using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmButton : TapableObject3D
{
    public GameControllerv2 gameController;
    //public AudioSource keyboardAudio;

    //public AudioClip tapSound1;
    //public AudioClip tapSound2;
    public override void OnTap()
    {
        gameController.pencilButton[gameController.selectedBalloonNumber].GetComponent<PencilButton>().OnButtonDown();
        /* 
        if(Random.Range(0,1f) <= 0.5f)
        {
            keyboardAudio.PlayOneShot(tapSound1);
        }
        else
        {
            keyboardAudio.PlayOneShot(tapSound2);
        }
        */
    }
}
