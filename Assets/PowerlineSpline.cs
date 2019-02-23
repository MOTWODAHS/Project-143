using System.Collections;
using System.Collections.Generic;
using BansheeGz.BGSpline.Components;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PowerlineSpline : MonoBehaviour
{
    public Transform originPole;
    public Transform destinationPole;
    public fillInLine fillLine;
    public TextMeshPro message;
    public Text GUImessage;

    public void SendLineMessage()
    {
        this.message.text = GUImessage.text;
        this.message.transform.position = originPole.transform.position;
        StartCoroutine(SendText());
    }

    private IEnumerator SendText()
    {
        float distance = 0;
        //calculate position and tangent
        while (distance < GetComponent<BGCcMath>().GetDistance())
        {       
            Vector3 tangent;
            //objectToMove.position = GetComponent<BGCcMath>().CalcPositionAndTangentByDistance(distance, out tangent);
            this.message.transform.position = GetComponent<BGCcMath>().CalcPositionAndTangentByDistance(distance, out tangent);
            //this is a version for 2D. For 3D, comment this line and uncomment the next one
            distance += 2 * Time.deltaTime;
            yield return null;
        }
        
    }
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
