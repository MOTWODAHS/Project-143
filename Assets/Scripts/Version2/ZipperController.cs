using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZipperController : MonoBehaviour
{
    public Raycaster raycaster;
    public GameObject joint;

    public GameControllerv2 gameController;

    public float dragRange = 2f;

    public float angle = 15f;

    [SerializeField]
    Vector3 startPosition;

    [SerializeField]
    Quaternion startAngle;

    [SerializeField]
    Quaternion endAngle;

    [SerializeField]
    float distance;

    private Transform jointTransform;
    void Start()
    {
        jointTransform = joint.GetComponent<Transform>();
        startAngle = Quaternion.Euler(0,0,0);
        endAngle = Quaternion.Euler(0,0,angle);
        startPosition = this.transform.localPosition;
    }

    
    void Update()
    {
        distance = this.transform.localPosition.x - startPosition.x;
        if(distance > 0.001f)
        {
            this.transform.localPosition = startPosition;
            distance = 0f;
        }
        else if(distance < -dragRange)
        {
            this.transform.localPosition = startPosition - new Vector3(dragRange, 0f, 0f);
            distance = -dragRange;
        }
        jointTransform.rotation = Quaternion.Lerp(startAngle, endAngle, Mathf.Abs(distance / dragRange));
    }

    void OnMouseDrag()
    {
        Vector3 temp = transform.position;
        temp.x = raycaster.currentPosition.x;
        transform.position = temp;
        raycaster.isDrag = true;
    }

    void OnMouseUp()
    {
        raycaster.isDrag = false;
        if(distance <= -dragRange + 0.001f)
        {
            Destroy(this.gameObject.GetComponent<BoxCollider>());
            gameController.Step1Event();
        }
    }
}
