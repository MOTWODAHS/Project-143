using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zipper : MonoBehaviour
{
    private Camera cam;
    private GameObject go;
    public GameObject joint;

    public GameController gamecontroller;
    [SerializeField]
    Transform jointTrans;

    public string btnName;
    public Vector3 screenSpace;
    [SerializeField]
    private Vector3 offset;
    [SerializeField]
    public bool isDrag = false;
    [SerializeField]
    private Vector3 startPosition;
    [SerializeField]
    Vector3 currentScreenSpace;
    public Vector3 currentPosition;

    [SerializeField]
    float StartAngle = 0f;
    [SerializeField]
    float EndAngle = 15f;

    [SerializeField]
    Quaternion startQ;
    [SerializeField]
    Quaternion endQ;

    [SerializeField]
    float distance;

    void Start()
    {
        cam = Camera.main;
        startPosition = this.transform.localPosition;
        startQ = Quaternion.Euler(0,0,StartAngle);
        endQ = Quaternion.Euler(0,0,EndAngle);
        jointTrans = joint.GetComponent<Transform>();
    }

    
    void Update()
    {
        CastRay();
        distance = this.transform.localPosition.x - startPosition.x;
        if(distance > 0.001f)
        {
            this.transform.localPosition = startPosition;
            distance = 0f;
        }
        else if(distance < -2f)
        {
            this.transform.localPosition = startPosition - new Vector3(2f, 0f, 0f);
            distance = -2f;
        }
        jointTrans.rotation = Quaternion.Lerp(startQ, endQ, Mathf.Abs(distance/2f));
    }


    void CastRay()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitinfo;

        if(isDrag == false)
        {
            if(Physics.Raycast(ray, out hitinfo))
            {
                Debug.DrawLine(ray.origin, hitinfo.point);
                go = hitinfo.collider.gameObject;
                screenSpace = cam.WorldToScreenPoint(go.transform.position);
                offset = go.transform.position - cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));
                btnName = go.name;
            }
            else
            {
                btnName = null;
            }
        }
        currentScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);
        currentPosition = cam.ScreenToWorldPoint(currentScreenSpace) + offset;
    }

    void OnMouseDrag()
    {
        if(btnName != null)
        {
            Vector3 temp = transform.position;
            temp.x = currentPosition.x;
            transform.position = temp;
            isDrag = true;
        }
        else
        {
            isDrag = false;
        }
    }

    IEnumerator Set_Pack_Fade()
    {
        yield return new WaitForSeconds(1f);
        gamecontroller.Pack_Fade_Out();
        yield return new WaitForSeconds(2.5f);
        gamecontroller.Set_Balloon_Collider();
    }

    void OnMouseUp()
    {
        isDrag = false;
        if(distance <= -1.99f)
        {
            StartCoroutine("Set_Pack_Fade");
            Destroy(this.gameObject.GetComponent<BoxCollider>());
        }
    }

}
