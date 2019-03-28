using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycaster : MonoBehaviour
{
    public bool isDrag = false;
    public Vector3 currentPosition;
    public string btnName;

    [SerializeField]
    Vector3 screenSpace;
    [SerializeField]
    Vector3 currentScreenSpace;
    [SerializeField]
    Vector3 offset;

    private Camera cam;
    private GameObject go;
    void Start()
    {
        cam = Camera.main;
    }

    void Update()
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
}
