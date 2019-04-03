using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BansheeGz.BGSpline.Components;
using BansheeGz.BGSpline.Curve;
using DG.Tweening;

namespace Talking
{
    class FillInLine : MonoBehaviour
    {
        

        private BGCcMath math;

        private List<Vector3> positions = new List<Vector3>();

        private LineRenderer fillrenderer;

        private float distance;

        private bool finished = false;

        private bool startdrawing = false;

        private bool touched = false;

        private Vector3 touchPosition;

        private GameController game;

        public BGCurve curve;

        public Transform controlPoints;

        public Material material;

        public Transform particle;

        // Start is called before the first frame update
        void Start()
        {
            particle.transform.position = new Vector3(curve[0].PositionWorld.x, curve[0].PositionWorld.y, particle.transform.position.z - 1f);
            fillrenderer = GetComponent<LineRenderer>();
            math = curve.GetComponent<BGCcMath>();
            particle.gameObject.SetActive(true);
            game = game = (GameController)GameObject.FindGameObjectWithTag("gameController").GetComponent(typeof(IGameController));
        }

        public void StartEndPoints(Transform start, Transform end)
        {
            Vector3 startHandlePos1 = (curve[0].PositionWorld - start.position) * 0.1f + start.position;
            Vector3 startHandlePos2 = -(curve[0].PositionWorld - start.position) * 0.1f + start.position;

            Vector3 endHandlePos1 = -(curve[curve.PointsCount - 1].PositionWorld - end.position) * 0.1f + start.position;
            Vector3 endHandlePos2 = (curve[curve.PointsCount - 1].PositionWorld - end.position) * 0.1f + start.position;

            int childCount = controlPoints.childCount;
            controlPoints.GetChild(0).transform.position = start.position;
            controlPoints.GetChild(childCount - 1).transform.position = end.position;
        }

        private float FillIntoLine(Vector3 position, float distance, float newDistance)
        {
            float temp = distance;
            while (temp < newDistance)
            {
                positions.Add(math.CalcByDistance(BGCurveBaseMath.Field.Position, temp));
                temp += 0.01f;
            }
            positions.Add(position);
            Vector3[] positionsArray = positions.ToArray();
            fillrenderer.positionCount = positionsArray.Length;
            fillrenderer.SetPositions(positionsArray);
            return newDistance;
        }

        [ContextMenu("Stretchline")]
        private void StretchLine()
        {
            //First disable the line renderer
            fillrenderer.enabled = false;
            curve.GetComponent<LineRenderer>().material = material;
            //calculate seven points between the start and end point
            Vector3 startPoint = curve[0].PositionWorld;
            Vector3 endPoint = curve[curve.PointsCount - 1].PositionWorld;
            int pointCount = curve.PointsCount;
            Vector3[] vector3s = new Vector3[pointCount];
            for (int i = 0; i < pointCount; i++)
            {
                vector3s[i] = startPoint + i * (endPoint - startPoint) * 1f / (pointCount - 1);
            }
            //DOTWeen
            Sequence sequence = DOTween.Sequence();
            for (int i = 1; i < pointCount - 1; i++)
            {
                MoveHandleAtPointI(i, vector3s);
                Transform transform = controlPoints.GetChild(i);
                if (i == 1)
                {
                    sequence.Append(transform.DOMove(vector3s[i], 2f));
                }
                else
                {
                    sequence.Join(transform.DOMove(vector3s[i], 2f));
                }
            }
            sequence.Play();
        }

        private void MoveHandleAtPointI(int index, Vector3[] vector3s)
        {

            Vector3 handle1Pos = vector3s[index] + (vector3s[index - 1] - vector3s[index]) * 0.1f;

            Vector3 handle2Pos = vector3s[index] + (vector3s[index + 1] - vector3s[index]) * 0.1f;
            StartCoroutine(MoveHandleTo(curve[index], handle1Pos, handle2Pos));
        }

        private IEnumerator MoveHandleTo(BGCurvePointI point, Vector3 pos1, Vector3 pos2)
        {
            Vector3 currentPos1 = point.ControlFirstWorld;
            Vector3 currentPos2 = point.ControlSecondWorld;

            float duration = 2f;
            float startTime = Time.time;
            while (Time.time - startTime < duration)
            {
                point.ControlFirstWorld = currentPos1 + (pos1 - currentPos1) * ((Time.time - startTime) / duration);
                point.ControlSecondWorld = currentPos2 + (pos2 - currentPos2) * ((Time.time - startTime) / duration);
                yield return null;
            }

        }

        // Update is called once per frame
        void Update()
        {

            if (!finished && touchPosition != null)
            {
                Vector3 point = touchPosition;
                point.z = 0;

                float newDistance;
                Vector3 pointOnCurve = curve.GetComponent<BGCcMath>().CalcPositionByClosestPoint(point, out newDistance);
                if (newDistance - distance < 1 && newDistance - distance > 0)
                {
                    distance = FillIntoLine(pointOnCurve, distance, newDistance);
                    particle.transform.position = pointOnCurve;
                    startdrawing = true;
                }
                else if (newDistance == math.GetDistance() && newDistance - distance < 1 && startdrawing)
                {
                    finished = true;
                    particle.gameObject.SetActive(false);
                    StretchLine();
                    game.Proceed();
                }
            
            }
        }

        public void setTouchLocation(Vector3 position)
        {
            touchPosition = position;
        }
    }
}
