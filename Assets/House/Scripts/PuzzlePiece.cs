using System;
using UnityEngine;
using TouchScript.Behaviors;
using TouchScript.Gestures.TransformGestures;

namespace Loving
{
    class PuzzlePiece : MonoBehaviour
    {
        #region Private Variables
        private bool enlarged = false;
        #endregion

        #region Protected Variables
        protected Bounds bound;

        protected Bounds thisBound;

        protected TransformGesture gesture;

        protected Transformer transformer;

        protected Vector3 position;

        protected IGameController game;

        protected Vector3 enlargedScale { get; set; }

        protected Vector3 normalScale { get; set; }
        #endregion

        #region Public Variables
        public String boundsStr;
        #endregion

        #region Protected Methods
        protected virtual void Start()
        {
            normalScale = transform.localScale;
            enlargedScale = 1.2f * transform.localScale;
            position = GetComponent<Transform>().position;
            game = (IGameController)GameObject.FindGameObjectWithTag("gameController").GetComponent(typeof(IGameController));
        }

        protected void ResetTransform()
        {
            transform.position = position;
        }

        protected int PlacementType() // 1: inwall; 0: intersect; -1: mutual-exclusive
        {
            thisBound = GetComponent<Collider2D>().bounds;

            Debug.Log(bound);
            bool inWall = bound.Contains(new Vector2(thisBound.min.x, thisBound.min.y)) &&
                bound.Contains(new Vector2(thisBound.max.x, thisBound.max.y));
            bool intersect = bound.Intersects(thisBound);

            if (inWall && intersect)
            {
                return 1;
            }
            else if (!inWall && intersect)
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }

        protected void ToggleScale()
        {
            if (enlarged)
            {
                transform.localScale = normalScale;
            }
            else
            {
                transform.localScale = enlargedScale;
            }
            enlarged = !enlarged;
        }

        protected virtual void OnEnable()
        {
            gesture = GetComponent<TransformGesture>();
            transformer = GetComponent<Transformer>();

            transformer.enabled = false;
            // Subscribe to gesture events
            gesture.TransformStarted += transformStartedHandler;
            gesture.TransformCompleted += transformCompletedHandler;

            if (!boundsStr.Equals(""))
            {
                Debug.Log(boundsStr);
                bound = GameObject.FindGameObjectWithTag(boundsStr).GetComponent<PolygonCollider2D>().bounds;
            }
        }

        protected virtual void transformStartedHandler(object sender, EventArgs e)
        {
            transformer.enabled = true;
            ToggleScale();
        }

        protected virtual void transformCompletedHandler(object sender, EventArgs e)
        {
            transformer.enabled = false;
            ToggleScale();

            int placementType = PlacementType();

            if (placementType != 1)
            {
                ResetTransform();
            }
        }

        protected virtual void OnTriggerStay2D(Collider2D other)
        {

            if (other.gameObject.name.Equals(this.gameObject.name + "InPlace") && !transformer.enabled)
            {
                ResetTransform();
                if (!other.GetComponentsInChildren<SpriteRenderer>()[0].enabled)
                {

                    foreach (SpriteRenderer spriteRenderer in other.GetComponentsInChildren<SpriteRenderer>())
                    {
                        spriteRenderer.enabled = true;
                    }
                    game.Proceed();
                }

            }
        }
        #endregion

        #region Public Variables
        public Transformer GetTransformer()
        {
            return transformer;
        }
        #endregion
    }
}
