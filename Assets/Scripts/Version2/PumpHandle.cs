using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpHandle : MonoBehaviour
{
    public Raycaster raycaster;
    public GameControllerv2 gameController;

    public float dragRange = 1.5f;

    public Vector3 startPosition;

    public bool isColliderOn = false;

    public float distance;

    public float volume = 0;
    public float lastPoint = 0;

    public Sprite[] inflatBalloon;

    
    void Update()
    {
        if(gameController.isStep2Finished && !isColliderOn)
        {
            this.gameObject.GetComponent<BoxCollider>().enabled = true;
            isColliderOn = true;
            startPosition = this.transform.localPosition;
        }
        if(isColliderOn)
        {
            distance = this.transform.localPosition.y - startPosition.y;
            if(distance < 0.001f)
            {
                this.transform.localPosition = startPosition;
                distance = 0f;
            }
            else if(distance > dragRange)
            {
                this.transform.localPosition = startPosition + new Vector3(0f, dragRange, 0f);
                distance = dragRange;
            }

            if(this.transform.localPosition.y - lastPoint < 0)
            {
                volume += lastPoint - this.transform.localPosition.y;
            }
            lastPoint = this.transform.localPosition.y;
            if(volume >= 6f)
            {
                gameController.balloons[gameController.selectedBalloonNumber].GetComponent<SpriteRenderer>().sprite = inflatBalloon[gameController.selectedBalloonNumber];
            }
        }
    }

    void OnMouseDrag()
    {
        Vector3 temp = transform.position;
        temp.y = raycaster.currentPosition.y;
        transform.position = temp;
        raycaster.isDrag = true;
    }

    void OnMouseUp()
    {
        raycaster.isDrag = false;
        if(volume >= 6f)
        {
            Destroy(this.gameObject.GetComponent<BoxCollider>());
            gameController.isStep3Finished = true;
        }      
    }
}
