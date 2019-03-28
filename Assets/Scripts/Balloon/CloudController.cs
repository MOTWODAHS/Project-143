using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CloudController : MonoBehaviour
{
    Tweener tweenerForward;

    Tweener tweenerBackward;

    public bool flag = true;

    void Start()
    {
        tweenerBackward = transform.DOMoveX(-5f, Random.Range(100f,150f)).SetEase(Ease.Linear);
        tweenerForward = transform.DOMoveX(7f, Random.Range(100f,150f)).SetEase(Ease.Linear);
         
        if(flag)
        {
            tweenerBackward.Pause();
            tweenerForward.OnComplete(PlayBackward);
        }
        else
        {
            tweenerForward.Pause();
            tweenerBackward.OnComplete(PlayForward);
        }
        
    }

    void PlayForward()
    {
        tweenerForward = transform.DOMoveX(7f, Random.Range(100f,150f)).SetEase(Ease.Linear);
        tweenerForward.PlayForward();
        tweenerForward.OnComplete(PlayBackward);
    }

    void PlayBackward()
    {
        tweenerBackward = transform.DOMoveX(-5f, Random.Range(100f,150f)).SetEase(Ease.Linear);
        tweenerBackward.PlayForward();
        tweenerBackward.OnComplete(PlayForward);
    }
}
