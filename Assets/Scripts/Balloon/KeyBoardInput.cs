using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBoardInput : TapableObject3D
{
    public KeyBoardController controller;
    public string inputString;

    public override void OnTap()
    {
        controller.AddInput(inputString);
    }

}
