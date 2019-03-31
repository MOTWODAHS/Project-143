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

        private GameController game;

        private GameObject interactableLine;

        private GameObject[] powerlines;

        private List<Coroutine> coroutines;

        [Header("Before player picks up a powerline")]
        public GameObject fakeShadow;

        public Transform originPole;

        public Transform destinationPole;

        public GameObject interactableLineRight;

        public GameObject interactableLineLeft;


        private void Start()
        {
            coroutines = new List<Coroutine>();
            game = (GameController)GameObject.FindGameObjectWithTag("gameController").GetComponent(typeof(IGameController));
            gesture = GetComponent<TransformGesture>();
            transformer = GetComponent<Transformer>();
            gesture.TransformStarted += transformStartedHandler;
            gesture.TransformCompleted += transformCompletedHandler;

        }

        private void InitializePowerline(GameObject interactableLine)
        {
            coroutines = new List<Coroutine>();

            interactableLine.GetComponent<PowerlineSpline>().enabled = true;

            Debug.Log("interactableLine is:" + interactableLine);
            Debug.Log("Transition is:" + interactableLine.GetComponent<Transition>());
            coroutines.Add(StartCoroutine(interactableLine.GetComponent<Transition>().lineRendererFadeIn()));
            foreach (GameObject powerline in powerlines)
            {
                coroutines.Add(StartCoroutine(powerline.GetComponent<Transition>().lineRendererFadeIn()));
            }
        }

        private void PickUp()
        {   
            destinationPole.transform.rotation = Quaternion.Euler(0, 0, 0);
            fakeShadow.SetActive(false);
            foreach(Coroutine c in coroutines)
            {
                StopCoroutine(c);
            }


            if (interactableLine != null)
            {
                interactableLine.GetComponent<PowerlineSpline>().enabled = false;
                interactableLine.GetComponent<Transition>().hide();
            }
          
            if (powerlines != null)
            {
                foreach (GameObject powerline in powerlines)
                {
                    powerline.GetComponent<Transition>().hide();
                }
            }         

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

            //Update Bounds
            Bounds bounds = originPole.GetComponent<Collider2D>().bounds;
            bounds.Encapsulate(destinationPole.GetComponent<Collider2D>().bounds);

            game.setBound(bounds);

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
