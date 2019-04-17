using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : TapableObject3D
{
    public GameObject replaybutton;
    public override void OnTap()
    {
        replaybutton.SetActive(true);
    }
}
