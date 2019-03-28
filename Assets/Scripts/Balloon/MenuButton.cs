using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : TapableObject3D
{
    public override void OnTap()
    {
        SceneManager.LoadScene("Menu");
    }
}
