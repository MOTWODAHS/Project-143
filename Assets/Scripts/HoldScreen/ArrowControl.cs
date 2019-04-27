using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ArrowControl : MonoBehaviour
{
    public Vector3 vec;

    private Vector3 endPoint;

    private Vector3 startPoint;
    void Start()
    {
        startPoint = this.transform.position;
        endPoint = startPoint + vec;
        this.transform.DOMove(endPoint,1f).SetEase(Ease.Linear).SetLoops(-1,LoopType.Yoyo);
    }

}
