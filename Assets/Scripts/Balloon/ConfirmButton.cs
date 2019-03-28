using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmButton : TapableObject3D
{
    public GameControllerv2 gameController;
    public override void OnTap()
    {
        gameController.pencilButton[gameController.selectedBalloonNumber].GetComponent<PencilButton>().OnButtonDown();
    }
}
