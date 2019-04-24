using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TouchScript.Behaviors;
using TouchScript.Gestures.TransformGestures;
using DG.Tweening;

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

        private AudioSource[] audios;

        private const float HAND_Z_OFFSET = -2f;

        private Vector3 originPos;

        private Quaternion originRotation;

        [Header("Before player picks up a powerline")]
        public GameObject fakeShadow;

        public Transform originPole;

        public Transform destinationPole;

        [Header("After player drops a powerline")]
        public GameObject interactableLineRight;

        public GameObject interactableLineLeft;

        public GameObject hand;

        public GameObject firstHand;

        private void Start()
        {
            coroutines = new List<Coroutine>();
            game = (GameController)GameObject.FindGameObjectWithTag("gameController").GetComponent(typeof(IGameController));
            gesture = GetComponent<TransformGesture>();
            transformer = GetComponent<Transformer>();
            gesture.TransformStarted += transformStartedHandler;
            gesture.TransformCompleted += transformCompletedHandler;
            audios = GetComponents<AudioSource>();
            originPos = transform.position;
            originRotation = transform.rotation;
        }

        private void InitializePowerline(GameObject interactableLine)
        {
            coroutines = new List<Coroutine>();

            interactableLine.GetComponent<PowerlineSpline>().enabled = true;

            coroutines.Add(StartCoroutine(interactableLine.GetComponent<Transition>().lineRendererFadeIn()));
            foreach (GameObject powerline in powerlines)
            {
                coroutines.Add(StartCoroutine(powerline.GetComponent<Transition>().lineRendererFadeIn()));
            }

            hand.SetActive(true);
            if (interactableLine.Equals(interactableLineLeft))
            {
                hand.transform.position = new Vector3(game.getBound().center.x + game.getBound().extents.x * 0.2f, game.getBound().center.y, HAND_Z_OFFSET);
            }
            else
            {
                hand.transform.position = new Vector3(game.getBound().center.x, game.getBound().center.y, HAND_Z_OFFSET);
            }
        }

        private void PickUp()
        {
            firstHand.SetActive(false);
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

            hand.SetActive(false);
            audios[0].Play();
        }

        private void Drop()
        {
            if (Math.Abs(destinationPole.position.x - originPole.position.x) < 2.5f)
            {
                ResetTransform();
                firstHand.SetActive(true);
                return;
            }
            if (destinationPole.position.x - originPole.position.x > 0f)
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
            game.SetInteractiveLine(interactableLine);

            //Update Bounds
            Bounds bounds = originPole.GetComponent<Collider2D>().bounds;
            bounds.Encapsulate(destinationPole.GetComponent<Collider2D>().bounds);

            game.setBound(bounds);

            InitializePowerline(interactableLine);
            audios[1].Play();
        }

        private void ResetTransform()
        {
            transform.position = originPos;
            transform.rotation = originRotation;
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
