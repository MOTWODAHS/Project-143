using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BansheeGz.BGSpline.Components;
using BansheeGz.BGSpline.Curve;

public class fillInLine : MonoBehaviour
{
    public BGCurve curve;

    private BGCcMath math;
    private List<Vector3> positions = new List<Vector3>();
    private LineRenderer renderer;
    private float distance;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<LineRenderer>();
        math = curve.GetComponent<BGCcMath>();
    }

    private float fillIntoLine(Vector3 position, float distance, float newDistance)
    {
        float temp = distance;
        while (temp < newDistance)
        {
            positions.Add(math.CalcByDistance(BGCurveBaseMath.Field.Position, temp));
            temp += 0.01f;
        }
        positions.Add(position);
        Vector3[] positionsArray = positions.ToArray();
        renderer.positionCount = positionsArray.Length;
        renderer.SetPositions(positionsArray);
        return temp;
    }

    

    // Update is called once per frame
    void Update()
    {
        //If Left Mouse Button is Held Down
        if (Input.GetMouseButton(0))
        {
            Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            point.z = 0;
            float newDistance;
            Vector3 pointOnCurve = curve.GetComponent<BGCcMath>().CalcPositionByClosestPoint(point, out newDistance);
            
            if (newDistance - distance < 10 && newDistance - distance > 0)
            {
                distance = fillIntoLine(pointOnCurve, distance, newDistance);
            
            }
           
        }
    }
}
