using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

namespace Talking
{
    public class PowerlineMap : MonoBehaviour
    {
        public Transform destinationPole;
        public Transform originPole;
        public Transform powerPoles;
        [Space]
        public GameObject[] powerlines;
        public GameObject interactableLineRight;
        public GameObject interactableLineLeft;

        [Space]
        public GameObject fakeShadows;
        public Material powerDestinatonDefault;

        public RectTransform prompt1;

        private float horizontalExtent = 6.66f;
        private float verticalExtent = 5f;
        private bool pickedUp = false;
        private bool placed = false;
        private Sequence s;
        public static Bounds powerPoleBounds;

        private Vector3 destinationScale;
        private Vector3 originScale;
        private float originalHeight;
        private GameObject interactableLine;

        private Coroutine coroutine;

        private void PlacePowerline(Vector3 point)
        {
            destinationPole.position = new Vector3(point.x, point.y, destinationPole.position.z);
            ZoomToPoint();
        }


        private void ZoomToPoint(bool camerazoom = false)
        {
            //Get bounds of the two poles
            Bounds bounds = originPole.GetComponent<Collider2D>().bounds;
            bounds.Encapsulate(destinationPole.GetComponent<Collider2D>().bounds);
            powerPoleBounds = bounds;
            //Calculate middle point
            Vector3 middlePoint = bounds.center;
            //Calculate new bound
            float distanceX = bounds.extents.x;
            float distanceY = bounds.extents.y;
            verticalExtent = 2f * Camera.main.orthographicSize;
            horizontalExtent = verticalExtent * Camera.main.aspect;
            float zoomFactor = Mathf.Max(2f * distanceX / horizontalExtent, 2f * distanceY / verticalExtent);
            //Move to the point
            Vector3 newPosition = new Vector3(middlePoint.x, middlePoint.y, Camera.main.transform.position.z);

            float newHeight = 0.1f * Vector3.Distance(destinationPole.position, originPole.position);

            float scaleFactor = newHeight / originalHeight;
            s = DOTween.Sequence();
            float duration = 3f;

            float newOrthoSize = Camera.main.orthographicSize * zoomFactor * 1.2f;

            if (camerazoom)
            {
                s.Append(Camera.main.transform.DOMove(new Vector3(middlePoint.x, middlePoint.y, Camera.main.transform.position.z), 2f));
                s.Join(Camera.main.DOOrthoSize(newOrthoSize, 2f));
                s.Play();
            }
        }

        private void pickUp(GameObject o, Vector3 position)
        {
            o.transform.rotation = Quaternion.Euler(0, 0, 0);
            o.transform.position = position;
        }

        private void InitializePowerline(GameObject interactableLine)
        {
            interactableLine.GetComponent<PowerlineSpline>().enabled = true;
            Transition.coroutines.Add(StartCoroutine(interactableLine.GetComponent<Transition>().FadeIn(0f, 0.5f)));
            //Invoke("EnableParticle", 0.5f);
            foreach (GameObject powerline in powerlines)
            {
                Transition.coroutines.Add(StartCoroutine(powerline.GetComponent<Transition>().FadeIn(0f, 0.5f)));
            }
        }

        private void HidePowerlines(GameObject interactableLine)
        {
            foreach (Coroutine coroutine in Transition.coroutines)
            {
                StopCoroutine(coroutine);
            }
            Transition.coroutines = new List<Coroutine>();
            if (interactableLine)
            {

                interactableLine.GetComponent<LineRenderer>().material.SetFloat("_Transparency", 0);
                interactableLine.GetComponent<PowerlineSpline>().enabled = false;
            }
            foreach (GameObject powerline in powerlines)
            {
                powerline.GetComponent<LineRenderer>().material.SetFloat("_Transparency", 0);
            }

        }


        private void HidePrompt()
        {
            prompt1.DOAnchorPosY(100f, 2f);
        }


        // Start is called before the first frame update
        void Start()
        {
            destinationScale = destinationPole.localScale;
            originScale = originPole.localScale;
            originalHeight = originPole.GetComponent<Collider2D>().bounds.extents.y;
        }


        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0) && !pickedUp)
            {
                Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                point.z = 0;

                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (hit.collider != null)
                {
                    if (hit.collider.gameObject.name == "powerlineDest")
                    {
                        pickedUp = true;
                        placed = false;
                        pickUp(destinationPole.gameObject, point);
                        HidePowerlines(interactableLine);
                        HidePrompt();
                    }
                    else if (hit.collider.gameObject.Equals(interactableLine))
                    {
                        ZoomToPoint(camerazoom: true);
                        interactableLine.GetComponent<PowerlineSpline>().EnableFilling();
                        foreach (GameObject powerline in powerlines)
                        {
                            StartCoroutine(powerline.GetComponent<Transition>().FadeIn(0.5f, 1f));
                        }
                        hit.collider.enabled = false;
                        destinationPole.GetComponent<BoxCollider2D>().enabled = false;
                    }
                }
            }
            else if (Input.GetMouseButton(0) && pickedUp && !placed)
            {
                Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                point.z = 0;
                PlacePowerline(point);
                destinationPole.GetComponent<SpriteRenderer>().material = powerDestinatonDefault;
                fakeShadows.SetActive(false);
            }
            else if (Input.GetMouseButtonUp(0) && !placed)
            {
                placed = true;
                pickedUp = false;
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
                ZoomToPoint();

                InitializePowerline(interactableLine);

            }

        }

        private void FixedUpdate()
        {
            if (destinationPole.transform.position.x > 11f)
            {
                destinationPole.transform.position = new Vector3(11f, destinationPole.transform.position.y, destinationPole.transform.position.z);
            }
            else if (destinationPole.transform.position.x < -20f)
            {
                destinationPole.transform.position = new Vector3(-21f, destinationPole.transform.position.y, destinationPole.transform.position.z);
            }

            if (destinationPole.transform.position.y > 7.7f)
            {
                destinationPole.transform.position = new Vector3(destinationPole.transform.position.x, 7.7f, destinationPole.transform.position.z);
            }
            else if (destinationPole.transform.position.y < -8.8f)
            {
                destinationPole.transform.position = new Vector3(destinationPole.transform.position.x, -8.8f, destinationPole.transform.position.z);
            }
        }
    }
}
