using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchScript.Gestures;

public class ZipperController : TapableObject3D
{
    public InstructionController instruction;
    //public Raycaster raycaster;
    public GameObject joint;

    public GameControllerv2 gameController;

    public float dragRange = 2f;

    public float angle = 15f;

    [SerializeField]
    Vector3 startPosition;

    [SerializeField]
    Quaternion startAngle;

    [SerializeField]
    Quaternion autoStartAngle;

    private float autoStartTime;
    public float autoSpeed = 1f;

    [SerializeField]
    Quaternion endAngle;

    [SerializeField]
    float distance;

    [SerializeField]
    bool isDragOver = false;

    private Transform jointTransform;
    void Start()
    {
        jointTransform = joint.GetComponent<Transform>();
        startAngle = Quaternion.Euler(0,0,0);
        autoStartAngle = startAngle;
        endAngle = Quaternion.Euler(0,0,angle);
        startPosition = this.transform.localPosition;
    }

    
    void Update()
    {
        Vector3 temp = this.transform.localPosition;
        temp.y = 0f;
        this.transform.localPosition = temp;
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
        if(!isDragOver)
        {
            jointTransform.rotation = Quaternion.Lerp(startAngle, endAngle, Mathf.Abs(distance / dragRange));
            autoStartAngle = jointTransform.rotation;
            autoStartTime = Time.time;
        }
        else
        {
            jointTransform.rotation = Quaternion.Lerp(autoStartAngle, endAngle, Mathf.Abs((Time.time - autoStartTime) * autoSpeed));
        }
    }
    /* 
    void OnMouseDrag()
    {
        Vector3 temp = transform.position;
        temp.x = raycaster.currentPosition.x;
        transform.position = temp;
        raycaster.isDrag = true;
    }

    void OnMouseUp()
    {
        instruction.DestroyHandInstruction(0);
        raycaster.isDrag = false;
        isDragOver = true;
        Destroy(this.gameObject.GetComponent<BoxCollider>());
        gameController.Step1Event();
    }
    */

    public override void OnTap()
    {
        instruction.DestroyHandInstruction(0);
        isDragOver = true;
        Destroy(this.gameObject.GetComponent<BoxCollider>());
        gameController.Step1Event();
    }

    public void OnDragEnd()
    {
        instruction.DestroyHandInstruction(0);
        isDragOver = true;
        Destroy(this.gameObject.GetComponent<BoxCollider>());
        gameController.Step1Event();
    }
}
