using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BansheeGz.BGSpline.Components;
using BansheeGz.BGSpline.Curve;
using System;

[RequireComponent(typeof(BGCcCursor))]
public class Bird : MonoBehaviour
{
    //The bird object
    public Transform objectToMove;

    //The entry curve distance
    private float firstDistance;
    //Looping curve
    private BGCurve curve;
    //If the object is in loop
    private bool inLoop = false;
    //If the object is exiting the scene
    private bool inExit = false;
    private bool exiting = false;
    //distance on the curve
    private float distance;
    //Collection of bird.
    private static Queue<Bird> birds = new Queue<Bird>();

    //current number of bird in the scene
    private static int size = 0;
    //max number of birds allowed in the scene
    public const int MAX = 6;




    // Start is called before the first frame update
    void Start()
    {
        birds.Enqueue(this);
        size++;
        curve = GetComponent<BGCurve>();
        firstDistance = curve.GetComponent<BGCcMath>().GetDistance();
    }

    //Returns how many birds is in the scene.
    public static int Count() { return Bird.size; }

    //Pop the first one in the queue.
    public static void ExitFirst()
    {
        Bird first = birds.Dequeue();
        size--;
        first.inExit = true;
    }

    public IEnumerator DestroyBird()
    {
        //3f is distance to kill.
        while(Vector3.Distance(objectToMove.position, new Vector3(10f, 0, 0) ) > 3f){
            yield return null;
        }
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        distance = GetComponent<BGCcCursor>().Distance;
        if (firstDistance - distance < 0.01 * firstDistance && !inLoop)
        {
            float sectionLength = curve.GetComponent<BGCcMath>().Math[0].DistanceFromEndToOrigin;
            curve.Delete(0);
            curve.Closed = true;
            inLoop = true;
            GetComponent<BGCcCursor>().Distance = distance - sectionLength;
            firstDistance = GetComponent<BGCcMath>().GetDistance();
        }
        else if (distance < 0.1f * firstDistance && inExit && !exiting)
        {
            float sectionLength = curve.GetComponent<BGCcMath>().Math[0].DistanceFromEndToOrigin;
            curve.Closed = false;
            curve.AddPoint(new BGCurvePoint(curve, new Vector3(10f, 0, 0), BGCurvePoint.ControlTypeEnum.BezierSymmetrical,
            new Vector3(-9f, 0f, 0f), new Vector3(11f, 0f, 0f), useWorldCoordinates: true));
            StartCoroutine(DestroyBird());
            exiting = true;
        }
        else
        {
            transform.Rotate(10 * Time.deltaTime, 0, 0);
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
