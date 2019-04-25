using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EndGradient : MonoBehaviour
{
    public float wipeUpInterval;
    public float fadeToWhiteInterval;
    public SpriteRenderer whiteShield;

    public void DoFade()
    {
        Sequence s = DOTween.Sequence();
        s.Append(transform.DOScaleY(13f, wipeUpInterval));
        s.Append(whiteShield.DOFade(1f, fadeToWhiteInterval));
        s.Play();
    }

}
