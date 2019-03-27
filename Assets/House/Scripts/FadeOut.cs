using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Loving
{
    class FadeOut : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            GetComponent<SpriteRenderer>().DOColor(new Color(157 / 255, 150 / 255, 255 / 255, 0), 5f);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
