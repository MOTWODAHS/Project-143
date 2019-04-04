using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Loving
{
    class FillInColor : MonoBehaviour
    {
        private Material myMaterial;

        private void Start()
        {
            myMaterial = GetComponent<SpriteRenderer>().material;
        }
        public void FillColor(Color myColor)
        {         
            myMaterial.SetColor("_PickedColor", myColor);
            myMaterial.DOFloat(1.0f, "_Distance", 1f).OnComplete(
                () =>
                {
                    myMaterial.SetColor("_Color", myColor);
                    myMaterial.SetFloat("_Distance", 0f);
                });
            if (GetComponent<BackgroundPuzzlePiece>() != null)
            {
                GetComponent<BackgroundPuzzlePiece>().setColor(myColor);
            }
        }
    }
}
