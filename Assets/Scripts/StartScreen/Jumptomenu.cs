using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Jumptomenu : TapableObject
{
    public override void OnTap()
    {
        SceneManager.LoadScene("Menu");
    }
}
