using System.Collections;
using BansheeGz.BGSpline.Components;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

namespace Talking
{
    class PowerlineSpline: MonoBehaviour
    {

        private GameController game;

        private float zoomFactor;

        public Transform originPole;

        public Transform destinationPole;

        public FillInLine fillLine;

        public TextMeshPro message;

        public KeyBoardController keyboardController;

        public BGCcMath nextLineMathRight;

        public BGCcMath nextLineMathLeft;

        public LineRenderer[] otherLines;

        public bool right;

        

        public void SendLineMessage()
        {
            if (enabled)
            {
                message.transform.gameObject.SetActive(true);
                message.text = keyboardController.inputString;
                game.setMessage(message.text);
                message.transform.position = originPole.transform.position;
                message.transform.localScale *= zoomFactor;
                StartCoroutine(SendText());
                StartCoroutine(StartTimer());
            }
        }

        private IEnumerator SendText()
        {
            float distance = 0;
            //calculate position and tangent
            while (distance < GetComponent<BGCcMath>().GetDistance())
            {
                Vector3 tangent;
                //objectToMove.position = GetComponent<BGCcMath>().CalcPositionAndTangentByDistance(distance, out tangent);
                this.message.transform.position = GetComponent<BGCcMath>().CalcPositionAndTangentByDistance(distance, out tangent);
                this.message.transform.position = new Vector3(this.message.transform.position.x, this.message.transform.position.y, this.message.transform.position.z - 7);
                //this is a version for 2D. For 3D, comment this line and uncomment the next one
                distance += 4 * zoomFactor * Time.deltaTime;
                yield return null;
            }
            StartCoroutine(SendTextToNextLine());
        }

        private IEnumerator SendTextToNextLine()
        {
            BGCcMath math;
            if (right) { math = nextLineMathRight; }
            else { math = nextLineMathLeft; }
            float distance = 0;
            //calculate position and tangent
            while (distance < math.GetDistance())
            {
                Vector3 tangent;
                //objectToMove.position = GetComponent<BGCcMath>().CalcPositionAndTangentByDistance(distance, out tangent);
                this.message.transform.position = math.CalcPositionAndTangentByDistance(distance, out tangent);
                this.message.transform.position = new Vector3(this.message.transform.position.x, this.message.transform.position.y, this.message.transform.position.z - 7);
                //this is a version for 2D. For 3D, comment this line and uncomment the next one
                distance += 4 * zoomFactor * Time.deltaTime;
                yield return null;
            }
        }

        private IEnumerator StartTimer()
        {
            yield return new WaitForSeconds(5f);
            game.SendMessageToNetwork();
        }

        public void EnableFilling()
        {
            //StartCoroutine(GetComponent<Transition>().FadeIn(0.5f, 1f));
            this.fillLine.enabled = true;
        }

        // Start is called before the first frame update
        void OnEnable()
        {
            Debug.Log("Enabling Powerline");
            game = (GameController)GameObject.FindGameObjectWithTag("gameController").GetComponent(typeof(IGameController));
            transform.position = game.getBound().center + new Vector3(0f, 1f, 0f);

            //Scale
            float boundX = game.getBound().extents.x;
            float boundY = game.getBound().extents.y;

            GetComponent<Collider2D>().enabled = true;
            Bounds thisBound = GetComponent<Collider2D>().bounds;
            zoomFactor = Mathf.Max(0.6f * boundX / thisBound.extents.x, (0.6f * boundY / thisBound.extents.y));

            //Set the scale
            transform.localScale *= zoomFactor;

            //Set the width of the line
            GetComponent<LineRenderer>().widthMultiplier = zoomFactor * 0.1f;
            fillLine.GetComponent<LineRenderer>().widthMultiplier = zoomFactor * 0.1f;
            fillLine.StartEndPoints(originPole, destinationPole);

            foreach (LineRenderer otherLine in otherLines)
            {
                otherLine.widthMultiplier = zoomFactor * 0.1f;
            }

        }

        private void OnDisable()
        {
            GetComponent<Collider2D>().enabled = false;
        }
    }
}
