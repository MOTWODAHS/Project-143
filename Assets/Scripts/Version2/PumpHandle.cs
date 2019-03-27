using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpHandle : MonoBehaviour
{
    public InstructionController instruction;
    //public Raycaster raycaster;
    public GameControllerv2 gameController;

    public float dragRange = 1f;

    public Vector3 startPosition;

    public bool isColliderOn = false;

    public float distance;

    public float current_volume = 0;
    public float volume = 0;

    public float max_volume = 3f;
    public float lastPoint = 0;

    public bool balloonChangeFlag = false;

    public Sprite[] inflatBalloon;

    public Sprite[] YellowBalloonAnimation;
    public Sprite[] GreenBalloonAnimation;
    public Sprite[] BlueBalloonAnimation;
    public Sprite[] RedBalloonAnimation;
    public Vector2[] YellowPipePosition;
    public Vector2[] GreenPipePosition;
    public Vector2[] BluePipePosition;
    public Vector2[] RedPipePosition;
    public int animationNumber = 0;    

    public GameObject[] startPipe;

    public GameObject[] EndPipe;

    
    void Update()
    {
        Vector3 temp = this.transform.localPosition;
        temp.x = 4.57f;
        this.transform.localPosition = temp;
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
            if(volume - current_volume >= max_volume/12)
            {
                if(animationNumber < 10)
                {
                    current_volume += max_volume/12;
                    switch(gameController.selectedBalloonNumber)
                    {
                        case 0:
                            gameController.balloons[0].GetComponent<SpriteRenderer>().sprite = YellowBalloonAnimation[animationNumber];
                            startPipe[0].transform.localPosition = YellowPipePosition[animationNumber];
                            animationNumber += 1;
                            break;
                        case 1:
                            gameController.balloons[1].GetComponent<SpriteRenderer>().sprite = GreenBalloonAnimation[animationNumber];
                            startPipe[1].transform.localPosition = GreenPipePosition[animationNumber];
                            animationNumber += 1;
                            break;
                        case 2:
                            gameController.balloons[2].GetComponent<SpriteRenderer>().sprite = BlueBalloonAnimation[animationNumber];
                            startPipe[2].transform.localPosition = BluePipePosition[animationNumber];
                            animationNumber += 1;
                            break;
                        default:
                            gameController.balloons[3].GetComponent<SpriteRenderer>().sprite = RedBalloonAnimation[animationNumber];
                            startPipe[3].transform.localPosition = RedPipePosition[animationNumber];
                            animationNumber += 1;
                            break;
                    }
                }
            }

            if(volume >= max_volume && !balloonChangeFlag)
            {
                gameController.balloons[gameController.selectedBalloonNumber].GetComponent<SpriteRenderer>().sprite = inflatBalloon[gameController.selectedBalloonNumber];
                startPipe[gameController.selectedBalloonNumber].SetActive(false);
                EndPipe[gameController.selectedBalloonNumber].SetActive(true);
                gameController.balloons[gameController.selectedBalloonNumber].transform.localScale = new Vector3(1.1f,1.1f,0f);
                balloonChangeFlag = true;
                instruction.DestroyHandInstruction(2);
            }
        }
    }
    /* 
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
        if(volume >= max_volume)
        {
            EndPipe[gameController.selectedBalloonNumber].SetActive(false);
            Destroy(this.gameObject.GetComponent<BoxCollider>());
            //isColliderOn = false;
            foreach(GameObject bal in gameController.balloons)
            {
                Destroy(bal.GetComponent<CapsuleCollider>());
            }
            gameController.isStep3Finished = true;
        }      
    }
    */

    public void OnDragEnd()
    {
        if(volume >= max_volume)
        {
            EndPipe[gameController.selectedBalloonNumber].SetActive(false);
            Destroy(this.gameObject.GetComponent<BoxCollider>());
            foreach(GameObject bal in gameController.balloons)
            {
                Destroy(bal.GetComponent<CapsuleCollider>());
            }
            gameController.isStep3Finished = true;
        }
    }
}
