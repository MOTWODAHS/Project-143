using System;
using System.Collections.Generic;
using UnityEngine;
using TouchScript.Behaviors;
using TouchScript.Gestures.TransformGestures;

namespace Loving {
    class DoorWindowPiece : PuzzlePiece
    {
        public static List<PuzzlePiece> pieces = new List<PuzzlePiece>();

        public static List<GameObject> windowLight = new List<GameObject>();

        private const int MAX_CAP = 20;

        private Vector3 startLocation;

        protected void OnPieceCountIncrement()
        {
            if (pieces.Count <= 1 && game.GetGameStage() == 2)
            {
                game.Proceed();
            }
        }

        protected bool Overlap()
        {
            foreach (PuzzlePiece piece in pieces)
            {
                if (!piece.Equals(this) && piece.GetComponent<BoxCollider2D>().bounds.Intersects(thisBound))
                {
                    return true;
                }
            }
            return false;
        }

        protected override void Start()
        {
            base.Start();
            position = GetComponent<Transform>().position;
            thisBound = GetComponent<BoxCollider2D>().bounds;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            pieces = new List<PuzzlePiece>();
            bound = GameObject.FindGameObjectWithTag("wall").GetComponent<PolygonCollider2D>().bounds;
            bound.Encapsulate(GameObject.FindGameObjectWithTag("roof").GetComponent<PolygonCollider2D>().bounds);
        }

        protected override void transformStartedHandler(object sender, EventArgs e)
        {
            base.transformStartedHandler(sender, e);
            startLocation = transform.position;
        }

        protected override void transformCompletedHandler(object sender, EventArgs e)
        {
            transformer.enabled = false;
            ToggleScale();
            thisBound = GetComponent<BoxCollider2D>().bounds;
            game.ResetTimer();

            int placementType = PlacementType();
            bool overlap = Overlap();

            if (overlap)
            {
                ResetTransform();
            }
            if (placementType == 1 && pieces.Count + 1 > MAX_CAP && !pieces.Contains(this))
            {
                ResetTransform();
            }
            else if (placementType == 0)
            {
                if (pieces.Contains(this))
                {
                    pieces.Remove(this);
                    game.RemovePiece(this);
                }
                ResetTransform();
            }
            if (placementType == -1 && !overlap)
            {
                if (pieces.Contains(this))
                {
                    pieces.Remove(this);
                    game.RemovePiece(this);
                }
                ResetTransform();
            }
            else if (placementType == 1 && pieces.Count + 1 <= MAX_CAP && !pieces.Contains(this) && !overlap)
            {
                placed = true;
                pieces.Add(this);
                game.AddPiece(this);
                OnPieceCountIncrement();
                game.PlayDropDownSound();
            }

            if (placed && Vector3.Distance(transform.position, startLocation) < 0.1f)
            {
                GetComponent<FillInColor>().FillColor(game.selectedColor);
            }
        }

        protected override void OnTriggerStay2D(Collider2D other)
        {
            return;
        }

    }
}
