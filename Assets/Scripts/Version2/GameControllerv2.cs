using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerv2 : MonoBehaviour
{
    //Step 1
    public GameObject upPart;

    public GameObject[] balloons;

    public float time = 2f;

    public int count = 4;

    public Vector2 upPartDistance = new Vector2(0,0);

    public Vector2 balloonDistance = new Vector2(0, 0.6f);

    public bool isStep1Finished = false;

    //Step 2

    public bool isBalloonSelected = false;

    public int selectedBalloonNumber = -1;

    public bool isStep2Finished = false;

    //Step 3
    public bool isStep3Finished = false;

    public bool moveFlag = false;

    private Camera cam;

    public float speed = 1f;

    public float startTime;

    public float distance = 3f;

    public Vector3 startCamPosition;

    public Vector3 startBalloonPosition;

    public GameObject Handle;
    public GameObject Body;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if(isStep3Finished && !moveFlag)
        {
            startTime = Time.time;
            moveFlag = true;
            startCamPosition = cam.transform.position;
            startBalloonPosition = balloons[selectedBalloonNumber].transform.position;
            StartCoroutine(FadeAndMove(Handle,time,new Vector2(0,0),count,true));
            StartCoroutine(FadeAndMove(Body,time,new Vector2(0,0),count,true));

        }
        if(moveFlag)
        {
            float coverDistance = (Time.time - startTime) * speed;
            float scale = coverDistance/distance;
            cam.transform.position = Vector3.Lerp(startCamPosition, startCamPosition + new Vector3(0,distance,0), scale);
            balloons[selectedBalloonNumber].transform.position = Vector3.Lerp(startBalloonPosition, startBalloonPosition + new Vector3(0,distance,0), scale);
        }

    }

    public void Step1Event()
    {
        StartCoroutine(Step1Transform());
    }

    IEnumerator FadeAndMove(GameObject obj, float time, Vector2 distance, int count, bool alphaFlag)
    {
        float oneTime = time/count;
        for(int i=0; i < count; i++)
        {
            yield return new WaitForSeconds(oneTime);
            if(alphaFlag)
                obj.GetComponent<SpriteRenderer>().color -= new Color(0f,0f,0f,1f/count);
            else
                obj.GetComponent<SpriteRenderer>().color += new Color(0f,0f,0f,1f/count);
            obj.transform.position += new Vector3((float)distance.x/count, (float)distance.y/count, 0);
        }
    }

    IEnumerator Step1Transform()
    {
        StartCoroutine(FadeAndMove(upPart,time,upPartDistance,count,true));
        yield return new WaitForSeconds(1f);
        foreach(GameObject balloon in balloons)
        {
            StartCoroutine(FadeAndMove(balloon,time,balloonDistance,count,false));
        }
        yield return new WaitForSeconds(time);
        isStep1Finished = true;
    }
}
