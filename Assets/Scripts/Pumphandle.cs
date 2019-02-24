using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pumphandle : MonoBehaviour
{

    public GameController gamecontroller;
    public float volume_amount;
    public Zipper ray;
    [SerializeField]
    Vector3 currentPosition;
    [SerializeField]
    string btnName;

    [SerializeField]
    Vector3 temp;

    [SerializeField]
    Vector3 startPosition;
    [SerializeField]
    float distance;

    [SerializeField]
    float volume = 0;

    [SerializeField]
    float lastpoint = 0;

    [SerializeField]
    Vector3 startScale = Vector3.zero;
    void Start()
    {
        startPosition = this.transform.localPosition;
    }


    void Update()
    {
        currentPosition = ray.currentPosition;
        btnName = ray.btnName;

        distance = this.transform.localPosition.y - startPosition.y;
        if(distance < 0.001f)
        {
            this.transform.localPosition = startPosition;
            distance = 0f;
        }
        else if(distance > 2f)
        {
            this.transform.localPosition = startPosition + new Vector3(0f, 2f, 0f);
            distance = 2f;
        }

        if(gamecontroller.active_balloon != null)
        {
            if(startScale == Vector3.zero)
            {
                startScale = gamecontroller.active_balloon.transform.localScale;
            }
            if(this.transform.localPosition.y - lastpoint < 0)
            {
                volume += lastpoint - this.transform.localPosition.y;
                gamecontroller.active_balloon.transform.localScale = Vector3.Lerp(startScale, startScale + new Vector3(0.6f,0.6f,0), volume/6f );
            }
            lastpoint = this.transform.localPosition.y;
        }
    }

    void OnMouseDrag()
    {
        if(btnName != null)
        {
            temp = transform.position;
            temp.y = currentPosition.y;
            transform.position = temp;
            ray.isDrag = true;
        }
        else
        {
            temp = Vector3.zero;
            ray.isDrag = false;
        }
    }

    void OnMouseUp()
    {
        ray.isDrag = false;
    }
}
