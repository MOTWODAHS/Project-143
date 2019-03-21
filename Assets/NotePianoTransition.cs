using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Singing
{
    class NotePianoTransition : MonoBehaviour
    {
        public SpriteRenderer sStaff;
        public Transform notes;
        public SpriteRenderer closeButton;

        private const int _duration = 2;

        void OnEnable()
        {
            sStaff.DOFade(1f, _duration);
            closeButton.DOFade(1f, _duration);
            foreach(Transform child in notes)
            {
                child.GetComponent<SpriteRenderer>().DOFade(1f, _duration);
                child.GetComponent<Collider2D>().enabled = true;
            }
        }
    }
}
