using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YesButton : TapableObject3D
{
    public bool isReplayYes = true;
    public override void OnTap()
    {
        if(isReplayYes)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        else
            SceneManager.LoadScene("Menu");
    }
}
