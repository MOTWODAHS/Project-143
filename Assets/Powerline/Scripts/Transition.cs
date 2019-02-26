using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{
    public IEnumerator FadeIn()
    {
        float duration = 1f;

        AnimationCurve curve = AnimationCurve.EaseInOut(0f, 0.0f, 0f + duration, 1.0f);
        float startTime = Time.time;
        while (Time.time < startTime + duration)
        {
            GetComponent<LineRenderer>().material.SetFloat("_Transparency", curve.Evaluate(Time.time - startTime));
            yield return null;
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
