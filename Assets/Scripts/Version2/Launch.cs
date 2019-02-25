using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launch : MonoBehaviour
{
    public GameControllerv2 gameController;

    void OnMouseDown()
    {
        if(gameController.isEdited == true)
        {
            gameController.isLanuched = true;
            gameController.startTime = Time.time;
            gameController.startCamPosition = Camera.main.transform.position;
            gameController.startBalloonPosition = gameController.balloons[gameController.selectedBalloonNumber].transform.position;
        }
    }
}
