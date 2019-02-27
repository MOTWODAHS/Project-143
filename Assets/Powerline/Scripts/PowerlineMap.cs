using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PowerlineMap : MonoBehaviour
{
    public Transform destinationPole;
    public Transform originPole;
    public Transform powerPoles;
    [Space]
    public GameObject[] powerlines;
    public GameObject interactableLineRight;
    public GameObject interactableLineLeft;

    [Space]
    public GameObject fakeShadows;
    public Material powerDestinatonDefault;

    public RectTransform prompt1;

    private float horizontalExtent = 6.66f;
    private float verticalExtent = 5f;
    private bool pickedUp = false;
    private bool placed = false;
    private Sequence s;
    public static Bounds powerPoleBounds;

    private Vector3 destinationScale;
    private Vector3 originScale;
    private float originalHeight;

    private void PlacePowerline(Vector3 point)
    {
        destinationPole.position = new Vector3(point.x, point.y, destinationPole.position.z);
        ZoomToPoint();
    }


    private void ZoomToPoint()
    {
        //Get bounds of the two poles
        Bounds bounds = originPole.GetComponent<Collider2D>().bounds;
        bounds.Encapsulate(destinationPole.GetComponent<Collider2D>().bounds);
        powerPoleBounds = bounds;
        //Calculate middle point
        //Vector3 middlePoint = (destinationPole.position + originPole.position)/2;
        Vector3 middlePoint = bounds.center;
        //Calculate new bound
        float distanceX = bounds.extents.x;
        float distanceY = bounds.extents.y;
        verticalExtent = 2f * Camera.main.orthographicSize;
        horizontalExtent = verticalExtent * Camera.main.aspect;
        float zoomFactor = Mathf.Max(2f * distanceX / horizontalExtent, 2f * distanceY / verticalExtent);
        //float zoomFactor = distanceY / (verticalExtent/2f);
        //Move to the point
        Vector3 newPosition = new Vector3(middlePoint.x, middlePoint.y, Camera.main.transform.position.z);

        float newHeight = 0.1f * Vector3.Distance(destinationPole.position, originPole.position);

        float scaleFactor = newHeight / originalHeight;
        s = DOTween.Sequence();
        float duration = 3f;

        float newOrthoSize = Camera.main.orthographicSize * zoomFactor * 1.2f;
        
        s.Append(Camera.main.transform.DOMove(newPosition, duration));
        s.Join(Camera.main.DOOrthoSize(Camera.main.orthographicSize * zoomFactor * 1.2f, duration));
        s.Join(destinationPole.DOScale(scaleFactor * destinationScale, duration));
        s.Join(originPole.DOScale(scaleFactor * originScale, duration));
        s.Play();
    }

    private void pickUp(GameObject o, Vector3 position)
    {
        o.transform.rotation = Quaternion.Euler(0, 0, 0);
        o.transform.position = position;
    }

    private void InitializePowerline(GameObject interactableLine)
    {
        interactableLine.GetComponent<PowerlineSpline>().enabled = true;
        StartCoroutine(interactableLine.GetComponent<Transition>().FadeIn());
        foreach (GameObject powerline in powerlines){
            StartCoroutine(powerline.GetComponent<Transition>().FadeIn());
        }
    }

    private void HidePrompt()
    {
        prompt1.DOAnchorPosY(400f, 2f);
    }


    // Start is called before the first frame update
    void Start()
    {
        destinationScale = destinationPole.localScale;
        originScale = originPole.localScale;
        originalHeight = originPole.GetComponent<Collider2D>().bounds.extents.y;
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
                HidePrompt();
            }
        } else if (Input.GetMouseButton(0) && pickedUp && !placed)
        {
            Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            point.z = 0;
            PlacePowerline(point);
            destinationPole.GetComponent<SpriteRenderer>().material = powerDestinatonDefault;
            fakeShadows.SetActive(false);
        } else if (Input.GetMouseButtonUp(0) && !placed)
        {
            placed = true;
            GameObject interactableLine;
            if (destinationPole.position.x - originPole.position.x > 0)
            {
                interactableLine = interactableLineRight;
                powerlines = GameObject.FindGameObjectsWithTag("PowerlineRight");
                interactableLine.GetComponent<PowerlineSpline>().right = true;
            } else
            {
                interactableLine = interactableLineLeft;
                powerlines = GameObject.FindGameObjectsWithTag("PowerlineLeft");
                interactableLine.GetComponent<PowerlineSpline>().right = false;
            }
            ZoomToPoint();
            s.OnComplete(()=>
            {
                InitializePowerline(interactableLine);
            });
        }
    }
}
