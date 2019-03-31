using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TouchScript.Behaviors;
using TouchScript.Gestures.TransformGestures;

namespace Talking
{
    class PowerlinePiece : MonoBehaviour
    {
        private TransformGesture gesture;

        private Transformer transformer;

        private IGameController game;

        private GameObject interactableLine;

        private GameObject[] powerlines;

        [Header("Before player picks up a powerline")]
        public GameObject fakeShadow;

        public Transform originPole;

        public Transform destinationPole;

        public GameObject interactableLineRight;

        public GameObject interactableLineLeft;


        private void Start()
        {
            game = (IGameController)GameObject.FindGameObjectWithTag("gameController").GetComponent(typeof(IGameController));
            gesture = GetComponent<TransformGesture>();
            transformer = GetComponent<Transformer>();
            gesture.TransformStarted += transformStartedHandler;

        }

        private void InitializePowerline(GameObject interactableLine)
        {
            interactableLine.GetComponent<PowerlineSpline>().enabled = true;
            Transition.coroutines.Add(StartCoroutine(interactableLine.GetComponent<Transition>().FadeIn(0f, 0.5f)));
            foreach (GameObject powerline in powerlines)
            {
                Transition.coroutines.Add(StartCoroutine(powerline.GetComponent<Transition>().FadeIn(0f, 0.5f)));
            }
        }

        private void PickUp()
        {
            destinationPole.transform.rotation = Quaternion.Euler(0, 0, 0);
            fakeShadow.SetActive(false);
        }

        private void Drop()
        {
            if (destinationPole.position.x - originPole.position.x > 0)
            {
                interactableLine = interactableLineRight;
                powerlines = GameObject.FindGameObjectsWithTag("PowerlineRight");
                interactableLine.GetComponent<PowerlineSpline>().right = true;
            }
            else
            {
                interactableLine = interactableLineLeft;
                powerlines = GameObject.FindGameObjectsWithTag("PowerlineLeft");
                interactableLine.GetComponent<PowerlineSpline>().right = false;
            }

            InitializePowerline(interactableLine);

        }

        private void transformStartedHandler(object sender, EventArgs e)
        {
            transformer.enabled = true;
            PickUp();
        }

        private void transformCompletedHandler(object sender, EventArgs e)
        {
            transformer.enabled = false;
            Drop();
        }

    }
}
