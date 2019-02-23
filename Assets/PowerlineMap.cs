using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PowerlineMap : MonoBehaviour
{
    public Transform destinationPole;
    public Transform originPole;
    public Transform powerlines;
    public GameObject electricLine;

    private float horizontalExtent = 6.66f;
    private float verticalExtent = 5f;
    private bool pickedUp = false;
    private bool placed = false;
    private Sequence s;
    private Bounds powerPoleBounds;

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
            bounds.Encapsulate(child.GetComponent<Collider2D>().bounds);
        }
        powerPoleBounds = bounds;
        //Calculate middle point
        Vector3 middlePoint = (destinationPole.position + originPole.position)/2;
        //Vector3 middlePoint = bounds.center;
        //Calculate new bound
        float distanceX = bounds.extents.x;
        float distanceY = bounds.extents.y;
        verticalExtent = 2f * Camera.main.orthographicSize;
        horizontalExtent = verticalExtent * Camera.main.aspect;
        //float zoomFactor = Mathf.Min(distanceX / horizontalExtent, distanceY / verticalExtent);
        float zoomFactor = distanceY / (verticalExtent/2f);
        //Move to the point
        Vector3 newPosition = new Vector3(middlePoint.x, middlePoint.y, Camera.main.transform.position.z);

        s = DOTween.Sequence();
        float duration = 3f;
        s.Append(Camera.main.transform.DOMove(newPosition, duration));
        s.Join(Camera.main.DOOrthoSize(Camera.main.orthographicSize * zoomFactor + 0.2f, duration));
        s.Play();
    }

    private void pickUp(GameObject o, Vector3 position)
    {
        o.transform.rotation = Quaternion.Euler(0, 0, 0);
        o.transform.position = position;
    }

    private void InitializePowerline()
    {
        electricLine.SetActive(true);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !pickedUp)
        {
            Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            point.z = 0;
            //PlacePowerline(point);
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                pickedUp = true;
                pickUp(destinationPole.gameObject, point);
            }
        } else if (Input.GetMouseButton(0) && pickedUp && !placed)
        {
            Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            point.z = 0;
            PlacePowerline(point);
        } else if (Input.GetMouseButtonUp(0) && !placed)
        {
            placed = true;
            s.OnComplete(()=>
            {
                InitializePowerline();
            });
        }
    }
}
