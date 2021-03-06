﻿using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

namespace Talking
{
    class GameController : MonoBehaviour, IGameController
    {

        private int gameStage = 0;

        private const float TIME_LIMIT = 300f;

        private float timer = 0f;

        private bool gameIsOver = false;

        private IGameController game;

        private GameObject interactiveLine;

        private delegate void StageTransition();

        private StageTransition[] transitions;

        private FillInLine fillInLine;

        private FillInCollider fillInCollider;

        private string message;

        private const float delay_to_send = 4f;

        private Vector3 anchor = new Vector3(-2.9165f, 6.3208f, 3.75161f);

        private Vector3 anchorToCenter;  

        Bounds bound;

        [Header("Before Picking Up An Pole")]
        public Collider2D destinationPoleCollider;
        public PowerlinePiece powerlinePiece;
        public GameObject anchorObj;

        [Header("Hand")]
        public GameObject hand;
        public GameObject firstHand;

        [Header("Keyboard")]
        public GameObject keyboard;

        [Header("NetworkingManager")]
        public NetworkingController network;

        [Header("UI")]
        public GameObject endingUI;
        public GameObject UI;
        public Collider doNotTouch;


        private void Start()
        {
            anchorToCenter = Camera.main.transform.position - anchor;
            game = (IGameController)GameObject.FindGameObjectWithTag("gameController").GetComponent(typeof(IGameController));
            transitions = new StageTransition[]
            {
                () =>
                {
                    hand.SetActive(false);
                    fillInLine = interactiveLine.GetComponentInChildren<FillInLine>();
                    fillInCollider = interactiveLine.GetComponentInChildren<FillInCollider>();
                    fillInCollider.GetComponent<BoxCollider>().enabled = true;
                    interactiveLine.GetComponent<Collider2D>().enabled = false;
                    fillInLine.enabled = true;
                    ZoomToPoint();
                },
                () =>
                {
                    fillInLine.enabled =false;
                    fillInCollider.GetComponent<BoxCollider>().enabled = false;
                    fillInCollider.enabled = false;
                    keyboard.transform.DOLocalMoveY(-0.83f, 2f).OnComplete(()=>{
                        doNotTouch.enabled = true;
                    });
                },
                () =>
                {
                    keyboard.transform.DOLocalMoveY(-2f, 2f);
                    interactiveLine.GetComponent<PowerlineSpline>().SendLineMessage();
                    UI.SetActive(false);
                    GetComponent<AudioSource>().Play();
                }
            };
        }

        private void Update()
        {
            timer += Time.deltaTime;
            if (timer > TIME_LIMIT)
            {
                SceneManager.LoadScene("HoldScene");
            }
        }
        public void SendMessageToNetwork()
        {
            network.SendAction(3, -1, message);
            network.InternetQuit();
            print(message);   
            StartCoroutine(JumpToEndScene());
        }

        private void ZoomToPoint()
        {
            //Get bounds of the two poles
            Bounds bounds = anchorObj.GetComponent<Collider2D>().bounds;
            bounds.Encapsulate(powerlinePiece.destinationPole.GetComponent<Collider2D>().bounds);
            //Calculate middle point
            Vector3 middlePoint = bounds.center;
            //Calculate new bound
            float distanceX = bounds.extents.x;
            float distanceY = bounds.extents.y;
            float verticalExtent = 2f * Camera.main.orthographicSize;
            float horizontalExtent = verticalExtent * Camera.main.aspect;
            float zoomFactor = Mathf.Max(2f * distanceX / horizontalExtent, 2f * distanceY / verticalExtent);
            zoomFactor = Mathf.Min(zoomFactor, 1 / 1.2f);
            //Move to the point
            Vector3 newPosition = anchor + anchorToCenter * zoomFactor * 1.2f;
            newPosition = new Vector3(newPosition.x, newPosition.y, Camera.main.transform.position.z);

            Sequence s = DOTween.Sequence();

            Vector3 newCameraScale = Camera.main.transform.localScale * zoomFactor * 1.2f;
            float newOrthoSize = Camera.main.orthographicSize * zoomFactor * 1.2f;

            destinationPoleCollider.enabled =false;
            s.Append(Camera.main.transform.DOMove(newPosition, 2f));
            s.Join(Camera.main.DOOrthoSize(newOrthoSize, 2f));
            s.Join(Camera.main.transform.DOScale(newCameraScale, 2f));
            s.Play();
        }

        internal void SetInteractiveLine(GameObject obj)
        {
            interactiveLine = obj;
        }

        public int GetGameStage()
        {
            throw new System.NotImplementedException();
        }

        public void Proceed()
        {
            transitions[gameStage]();
            gameStage++;
            timer = 0f;
        }

        public void StartGame()
        {
            destinationPoleCollider.enabled = true;
            firstHand.GetComponent<Transition>().TransitionIn();
        }

        public Bounds getBound()
        {
            return bound;
        }

        public void setBound(Bounds bound)
        {
            this.bound = bound;
        }

        public void setMessage(string str)
        {
            message = str;
        }

        public string GetMessage()
        {
            return message;
        }

        IEnumerator JumpToEndScene()
        {
            yield return new WaitForSeconds(8f);
            SceneManager.LoadScene("EndScene");
        }
    }
}
