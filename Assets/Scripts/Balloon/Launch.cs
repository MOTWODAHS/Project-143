using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launch : TapableObject3D
{
    public InstructionController instruction;
    public GameControllerv2 gameController;

    public override void OnTap()
    {
        if(gameController.isEdited == true)
        {
            gameController.isLanuched = true;
            gameController.startTime = Time.time;
            gameController.startCamPosition = Camera.main.transform.position;
            gameController.startBalloonPosition = gameController.balloons[gameController.selectedBalloonNumber].transform.position;
            instruction.DestroyHandInstruction(4);
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
