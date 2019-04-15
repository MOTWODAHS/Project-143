using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ClickHand2Control : MonoBehaviour
{
    float delaytime = 0.3f;
    void Start()
    {
        Sequence mysequence = DOTween.Sequence();
        //mysequence.PrependInterval(delaytime);
        mysequence.Append(transform.DOMoveX(-1.07f,1f));
        //mysequence.PrependInterval(delaytime);
        mysequence.Append(transform.DOMoveX(-0.63f,1f));
        //mysequence.PrependInterval(delaytime);
        mysequence.Append(transform.DOMoveX(-0.05f,1f));
        mysequence.PrependInterval(delaytime);
        mysequence.AppendInterval(delaytime);
        //mysequence.Append(transform.DOMoveX(-1.64f,0.2f));
        mysequence.SetLoops(-1,LoopType.Yoyo);
    }
}
