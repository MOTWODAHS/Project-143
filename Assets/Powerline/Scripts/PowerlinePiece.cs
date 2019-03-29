using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TouchScript.Behaviors;
using TouchScript.Gestures.TransformGestures;

public class PowerlinePiece : MonoBehaviour
{
    private TransformGesture gesture;
    private Transformer transformer;

    private IGameController game;

    private void Start()
    {
        game = (IGameController)GameObject.FindGameObjectWithTag("gameController").GetComponent(typeof(IGameController));
        gesture = GetComponent<TransformGesture>();
        transformer = GetComponent<Transformer>();
        gesture.TransformStarted += transformStartedHandler;

    }

    private void transformStartedHandler(object sender, EventArgs e)
    {
        transformer.enabled = true;
    }

    private void transformCompletedHandler(object sender, EventArgs e)
    {
        transformer.enabled = false;
    }
    
}
