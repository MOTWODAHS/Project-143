using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloonv2 : TapableObject3D
{
    public InstructionController instruction;
    public int balloonNumber;
    public GameControllerv2 gameController;

    public GameObject balloonPosition;

    public float speed = 1f;

    public float startTime;

    public float distance;

    public Vector3 startPosition;

    public bool isColliderOn = false;

    public bool isSelected = false;

    private Camera cam;

    public Vector3 startCameraPosition;
    public Vector3 cameraPositionChange = new Vector3(0,5.5f,0);

    public GameObject pump;

    public Vector3 startPumpPosition;

    public GameObject endPumpPoint;

    public bool moveFlag = true;

    Color[] balloonColor = new Color[4]{Color.yellow, Color.green, Color.blue, Color.red};

    public GameObject[] startPipe;

    void Start()
    {
        cam = Camera.main;
        startCameraPosition = cam.transform.position;
        startPumpPosition = pump.transform.position;
    }

    void Update()
    {
        if(gameController.isStep1Finished && !isColliderOn)
        {
            this.gameObject.GetComponent<CapsuleCollider>().enabled = true;
            startPosition = this.transform.position;
            isColliderOn = true;
            distance = Vector3.Distance(startPosition, balloonPosition.transform.position);
        }

        if(isSelected && moveFlag)
        {
            float coverDistance = (Time.time - startTime) * speed;
            float scale = coverDistance/distance;
            this.transform.position = Vector3.Lerp(startPosition, balloonPosition.transform.position, scale);
            cam.transform.position = Vector3.Lerp(startCameraPosition, startCameraPosition + cameraPositionChange, scale);
            //pump.transform.position = Vector3.Lerp(startPumpPosition, endPumpPoint.transform.position, scale);
            if(scale > 1f)
            {
                moveFlag = false;
                gameController.isStep2Finished = true;
                startPipe[gameController.selectedBalloonNumber].SetActive(true);
                instruction.SetHandInstruction(2);
            }
        }
    }

    public override void OnTap()
    {
        if(gameController.isBalloonSelected == false)
        {
            instruction.DestroyHandInstruction(1);
            isSelected = true;
            gameController.isBalloonSelected = true;
            gameController.selectedBalloonNumber = balloonNumber;
            startTime = Time.time;
            Destroy(this.gameObject.GetComponent<Collider>());
        }
        else
        {
            if(gameController.selectedBalloonNumber != -1)
            {
                if(gameController.balloons[gameController.selectedBalloonNumber] != this.gameObject)
                {
                    print("change color");
                    print("exchange select number in gamecontroller");
                    /* 
                    this.gameObject.GetComponent<SpriteRenderer>().material.color = balloonColor[gameController.selectedBalloonNumber];
                    gameController.balloons[gameController.selectedBalloonNumber].GetComponent<SpriteRenderer>().material.color = balloonColor[balloonNumber];
                    */
                }
            }
        }
    }

}
