using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Talking
{
    public class Transition : MonoBehaviour
    {
        public static List<Coroutine> coroutines = new List<Coroutine>();

        public IEnumerator FadeIn(float startValue, float toValue)
        {
            float duration = 1f;

            AnimationCurve curve = AnimationCurve.EaseInOut(startValue, 0.0f, 0f + duration, toValue);
            float startTime = Time.time;
            while (Time.time < startTime + duration)
            {
                GetComponent<LineRenderer>().material.SetFloat("_Transparency", curve.Evaluate(Time.time - startTime));
                yield return null;
            }

        }


    }
}

