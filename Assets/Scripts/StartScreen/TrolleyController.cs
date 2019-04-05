using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BansheeGz.BGSpline.Components;

[RequireComponent(typeof(BGCcMath))]
public class TrolleyController : MonoBehaviour
{
    public Transform ObjectToMove;
    private float distance;

    void Update()
    {
        //increase distance
       distance += 1 * Time.deltaTime;

       //calculate position and tangent
       Vector3 tangent;
       ObjectToMove.position = GetComponent<BGCcMath>().CalcPositionAndTangentByDistance(distance, out tangent);
       //this is a version for 3D. For 2D, comment this line and uncomment the next one
       //ObjectToMove.rotation = Quaternion.LookRotation(tangent);
       ObjectToMove.rotation = Quaternion.AngleAxis(Mathf.Atan2(tangent.y, tangent.x) * Mathf.Rad2Deg, Vector3.forward);

    }
}
