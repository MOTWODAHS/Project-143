using System;
using UnityEngine;
using TouchScript.Behaviors;
using TouchScript.Gestures.TransformGestures;

namespace Talking
{
    class FillLineParticle : MonoBehaviour
    {
        private TransformGesture gesture;

        private Transformer transformer;

        private GameController game;

        private void Start()
        {
            game = (GameController)GameObject.FindGameObjectWithTag("gameController").GetComponent(typeof(IGameController));
            gesture = GetComponent<TransformGesture>();
            transformer = GetComponent<Transformer>();
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
    }
}

