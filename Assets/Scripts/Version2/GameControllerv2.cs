using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    //Step4
    public bool moveFlag = false;

    public bool isCamStop = false;

    private Camera cam;

    public float speed = 1f;

    public float startTime;

    public float distance = 3f;

    public Vector3 startCamPosition;

    public Vector3 startBalloonPosition;

    public GameObject Handle;
    public GameObject Body;

    public Sprite short_handle;

    public GameObject[] line;
    public GameObject[] panel;

    public GameObject[] panelVertex;

    public GameObject[] weight;

    public GameObject pencilButton;

    public bool isStep4Finished = false;

    //Step 5
    public bool isEdited = false;

    public bool isLanuched = false;

    public bool isWeightLeave = false;

    public bool isStep5Finished = false;

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
            Handle.GetComponent<SpriteRenderer>().sprite = short_handle;
            StartCoroutine(FadeAndMove(Handle,time,new Vector2(0,0),count,true));
            StartCoroutine(FadeAndMove(Body,time,new Vector2(0,0),count,true));

        }
        if(moveFlag && !isCamStop)
        {
            float coverDistance = (Time.time - startTime) * speed;
            float scale = coverDistance/distance;
            cam.transform.position = Vector3.Lerp(startCamPosition, startCamPosition + new Vector3(0,distance,0), scale);
            balloons[selectedBalloonNumber].transform.position = Vector3.Lerp(startBalloonPosition, startBalloonPosition + new Vector3(0,distance,0), scale);
            line[selectedBalloonNumber].GetComponent<SpriteRenderer>().color = Color.Lerp(new Color(0,0,0,0), new Color(0,0,0,1f), scale);
            if(scale >= 1f)
            {
                isCamStop = true;
                StartCoroutine(FadeAndMove(panel[selectedBalloonNumber],time, Vector2.zero, count, false));
                StartCoroutine(FadeAndMove(weight[selectedBalloonNumber],time, Vector2.zero, count, false));
                StartCoroutine(FadeAndMove(pencilButton,time, Vector2.zero, count, false));
                StartCoroutine(OpenPencilButtonCollider());
            }
        }

        if(isEdited && isLanuched && !isStep5Finished)
        {
            if(!isWeightLeave)
            {
                weight[selectedBalloonNumber].transform.parent = null;
                isWeightLeave = true;
                StartCoroutine(FadeAndMove(balloons[selectedBalloonNumber], 4f, Vector2.zero, 8, true));
                StartCoroutine(FadeAndMove(panel[selectedBalloonNumber], 4f, Vector2.zero, 8, true));
                StartCoroutine(FadeAndMove(line[selectedBalloonNumber], 4f, Vector2.zero, 8, true));
                StartCoroutine(FadeVertex(panelVertex[selectedBalloonNumber], 4f, 8));
            }
            float coverDistance = (Time.time - startTime) * speed;
            float scale = coverDistance/4f;
            cam.transform.position = Vector3.Lerp(startCamPosition, startCamPosition + new Vector3(0,4f,0), scale);
            balloons[selectedBalloonNumber].transform.position = Vector3.Lerp(startBalloonPosition, startBalloonPosition + new Vector3(0,4f,0), scale);
            if(scale > 1f)
            {
                isStep5Finished = true;
            }
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

    IEnumerator OpenPencilButtonCollider()
    {
        yield return new WaitForSeconds(time);
        pencilButton.GetComponent<Collider>().enabled = true;
        weight[selectedBalloonNumber].GetComponent<Collider>().enabled = true;
        isStep4Finished = true;
    }

    IEnumerator FadeVertex(GameObject obj, float time, int count)
    {
        float oneTime = time/count;
        for(int i=0; i< count; i++)
        {
            yield return new WaitForSeconds(oneTime);
            obj.GetComponent<TMP_Text>().color -= new Color(0,0,0,1f/count);
        }
    }
}
