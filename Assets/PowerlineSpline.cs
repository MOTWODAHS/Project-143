using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerlineSpline : MonoBehaviour
{
    public Transform originPole;
    public Transform destinationPole;
    public fillInLine fillLine;


    // Start is called before the first frame update
    void Start()
    {
        Vector3 middlePoint = (destinationPole.position + originPole.position) / 2;
        transform.position = middlePoint;

        //Scale
        float boundX = Mathf.Abs((destinationPole.position - originPole.position).x);
        float boundY = Mathf.Abs((destinationPole.position - originPole.position).y);

        Bounds thisBound = GetComponent<Collider2D>().bounds;
        float zoomFactor = Mathf.Max(0.5f * boundX / thisBound.extents.x, (0.5f * boundY / thisBound.extents.y));

        transform.localScale *= zoomFactor;
        fillLine.StartEndPoints(originPole, destinationPole);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
