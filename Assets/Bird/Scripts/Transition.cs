using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Singing
{
    public class Transition : MonoBehaviour
    {
        private SpriteRenderer spriteRenderer;

        private void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void TransitionOut()
        {
            spriteRenderer.DOFade(0f, 1f);
            if (GetComponent<BoxCollider2D>() != null)
            {
                GetComponent<BoxCollider2D>().enabled = false;
            }
        }

        public void TransitionIn()
        {
            spriteRenderer.DOFade(1.0f, 1f);
            if (GetComponent<BoxCollider2D>() != null)
            {
                GetComponent<BoxCollider2D>().enabled = true;
            }
        }
    }
}
