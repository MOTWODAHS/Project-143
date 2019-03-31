using System.Collections;
using BansheeGz.BGSpline.Components;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

namespace Talking
{
    public class PowerlineSpline : MonoBehaviour
    {

        private GameController game;
        public Transform originPole;
        public Transform destinationPole;
        public fillInLine fillLine;
        public TextMeshPro message;
        public Text GUImessage;
        public BGCcMath nextLineMathRight;
        public BGCcMath nextLineMathLeft;

        public LineRenderer[] otherLines;

        public bool right;

        private float zoomFactor;

        public void SendLineMessage()
        {
            if (this.enabled)
            {
                this.message.transform.gameObject.SetActive(true);
                this.message.text = GUImessage.text;
                this.message.transform.position = originPole.transform.position;
                this.message.transform.localScale *= zoomFactor;
                StartCoroutine(SendText());
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
                distance += 3 * Time.deltaTime;
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
                distance += 3 * Time.deltaTime;
                yield return null;
            }
        }

        public void EnableFilling()
        {
            //StartCoroutine(GetComponent<Transition>().FadeIn(0.5f, 1f));
            this.fillLine.enabled = true;
        }

        private void EnableParticle()
        {
            ParticleSystem.EmissionModule emission = transform.GetChild(3).GetComponent<ParticleSystem>().emission;
            emission.enabled = true;
        }


        // Start is called before the first frame update
        void OnEnable()
        {

            Debug.Log("PowerlineSplineEnabled");
            game = (GameController)GameObject.FindGameObjectWithTag("gameController").GetComponent(typeof(IGameController));
            transform.position = game.getBound().center;

            //Scale
            float boundX = game.getBound().extents.x;
            float boundY = game.getBound().extents.y;

            Bounds thisBound = GetComponent<Collider2D>().bounds;
            zoomFactor = Mathf.Max(0.5f * boundX / thisBound.extents.x, (0.5f * boundY / thisBound.extents.y));

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
            ParticleSystem.EmissionModule emission = transform.GetChild(3).GetComponent<ParticleSystem>().emission;
            emission.enabled = false;
            transform.GetChild(3).GetComponent<ParticleSystem>().Clear();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.PageDown))
            {
                Scene scene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(scene.name);
            }
        }
    }
}
