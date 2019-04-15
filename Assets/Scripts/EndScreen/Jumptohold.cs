using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Jumptohold : TapableObject
{
    public override void OnTap()
    {
        SceneManager.LoadScene("HoldScene");
    }
}
