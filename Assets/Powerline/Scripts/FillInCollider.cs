using System;
using UnityEngine;
using TouchScript.Behaviors;
using TouchScript.Gestures.TransformGestures;

namespace Talking
{
    class FillInCollider : MonoBehaviour
    {
        private TransformGesture gesture;

        private Transformer transformer;

        private GameController game;

        private bool transformEnabled = false;

        public FillInLine fillInLine;

        private void Start()
        {
            game = (GameController) GameObject.FindGameObjectWithTag("gameController").GetComponent(typeof(IGameController));
        }

        private void OnEnable()
        {
            gesture = GetComponent<TransformGesture>();
            transformer = GetComponent<Transformer>();

            transformer.enabled = false;

            gesture.TransformStarted += transformStartedHandler;
            gesture.TransformCompleted += transformCompletedHandler;
        }

        private void transformStartedHandler(object sender, EventArgs e)
        {
            //transformer.enabled = true;
            transformEnabled = true;
        }


        private void transformCompletedHandler(object sender, EventArgs e)
        {
            //transformer.enabled = false;
            transformEnabled = false;
        }

        private void Update()
        {
            if (transformEnabled)
            {
                Ray ray = Camera.main.ScreenPointToRay(gesture.ScreenPosition);
                RaycastHit hitinfo;

                if (Physics.Raycast(ray, out hitinfo) && hitinfo.transform == transform)
                {
                    fillInLine.setTouchLocation(hitinfo.point);
                }
            }              
        }

    }
}
