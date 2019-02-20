using System;
using System.Collections;
using System.Collections.Generic;
using BansheeGz.BGSpline.Components;
using BansheeGz.BGSpline.Curve;
using UnityEngine;
using TMPro;

public class PowerlineMessage : MonoBehaviour
{
    //The text on the powerline
    public Transform objectToMove;
    //TextMeshPro object
    public TextMeshPro textMesh;
    //number of messages in the queue
    private static int size = 0;

    //Queue if Message to Show
    private Queue<string> messages = new Queue<string>();
    //distance on the curve
    private float distance = 0;
    //move with cursor or not
    private bool moveWithCursor = false;

    public void AddMessageToQueue(string message)
    {
    
        messages.Enqueue(message);
        size++;
        Debug.Log("Add message to queue");
    }

    public void NextMessage()
    {
        if (size > 0)
        {
            textMesh.text = messages.Dequeue();
            size--;
            objectToMove.gameObject.SetActive(true);
        }
        else
        {
            objectToMove.gameObject.SetActive(false);
        }
        distance = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        objectToMove.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        distance += 5 * Time.deltaTime;
        if (GetComponent<BGCcMath>().GetDistance() - distance < GetComponent<BGCcMath>().GetDistance() * 0.01)
        {
            NextMessage();
        }
        //calculate position and tangent
        Vector3 tangent;
        //objectToMove.position = GetComponent<BGCcMath>().CalcPositionAndTangentByDistance(distance, out tangent);
        objectToMove.position = GetComponent<BGCcMath>().CalcPositionAndTangentByDistance(distance, out tangent);
        //this is a version for 2D. For 3D, comment this line and uncomment the next one
        //objectToMove.rotation = Quaternion.LookRotation(tangent);
        objectToMove.rotation = Quaternion.AngleAxis(Mathf.Atan2(tangent.y, tangent.x) * Mathf.Rad2Deg, Vector3.forward);

    }
}
