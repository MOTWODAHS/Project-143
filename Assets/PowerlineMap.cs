using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PowerlineMap : MonoBehaviour
{
    public Transform destinationPole;
    public Transform originPole;
    public Transform powerlines;

    private float horizontalExtent = 6.66f;
    private float verticalExtent = 5f;

    private void PlacePowerline(Vector3 point)
    {
        destinationPole.position = new Vector3(point.x, point.y, destinationPole.position.z);
        ZoomToPoint();
    }


    private void ZoomToPoint()
    {
        //Get bounds of the two poles
        Bounds bounds = new Bounds();
        foreach(Transform child in powerlines)
        {
            bounds.Encapsulate(child.GetComponent<Renderer>().bounds);
        }
        //Calculate middle point
        Vector3 middlePoint = (destinationPole.position + originPole.position)/2;
        //Vector3 middlePoint = bounds.center;
        //Calculate new bound
        float distanceX = bounds.extents.x;
        float distanceY = bounds.extents.y;

        float zoomFactor = Mathf.Max(distanceX / horizontalExtent, distanceY / verticalExtent);
        //Move to the point
        Vector3 newPosition = new Vector3(middlePoint.x, middlePoint.y, Camera.main.transform.position.z);

        Sequence s = DOTween.Sequence();
        float duration = 5f;
        s.Append(Camera.main.transform.DOMove(newPosition, duration));
        s.Join(Camera.main.DOOrthoSize(zoomFactor * 1.2f * 5, duration));
        s.Play();
    }

    


    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            point.z = 0;
            PlacePowerline(point);
        }
    }
}
