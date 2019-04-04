using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Loving {
    class DoorWindowPiece : PuzzlePiece
    {
        private static List<PuzzlePiece> pieces = new List<PuzzlePiece>();

        private const int MAX_CAP = 5;

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

        protected override void transformCompletedHandler(object sender, EventArgs e)
        {
            transformer.enabled = false;
            base.ToggleScale();
            thisBound = GetComponent<BoxCollider2D>().bounds;

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
                }
                ResetTransform();
            }
            if (placementType == -1 && !overlap)
            {
                if (pieces.Contains(this))
                {
                    pieces.Remove(this);
                }
                ResetTransform();
            }
            else if (placementType == 1 && pieces.Count + 1 <= MAX_CAP && !pieces.Contains(this) && !overlap)
            {
                placed = true;
                pieces.Add(this);
                OnPieceCountIncrement();
            }


        }

        protected override void OnTriggerStay2D(Collider2D other)
        {
            return;
        }

    }
}
