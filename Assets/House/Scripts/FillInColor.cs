using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FillInColor : MonoBehaviour
{
    private Material myMaterial;

    private void Start()
    {
        myMaterial = GetComponent<SpriteRenderer>().material;
    }
    public void FillColor(Color myColor)
    {
        myMaterial.SetColor("_PickedColor", myColor);
        myMaterial.DOFloat(1.0f, "_Distance", 1f);
    }
}
