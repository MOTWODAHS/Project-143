using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TouchScript.Behaviors;
using TouchScript.Gestures.TransformGestures;

public class Roof : MonoBehaviour
{
    private TransformGesture gesture;
    private Transformer transformer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        gesture = GetComponent<TransformGesture>();
        transformer = GetComponent<Transformer>();

        transformer.enabled = false;
        // Subscribe to gesture events
        gesture.TransformStarted += transformStartedHandler;
        gesture.TransformCompleted += transformCompletedHandler;
    }

    private void transformStartedHandler(object sender, EventArgs e)
    {
        transformer.enabled = true;
    }

    private void transformCompletedHandler(object sender, EventArgs e)
    {
        transformer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
