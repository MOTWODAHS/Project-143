using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : TapableObject3D
{
    public GameObject menuButton;
    public override void OnTap()
    {
        menuButton.SetActive(true);
    }
}
