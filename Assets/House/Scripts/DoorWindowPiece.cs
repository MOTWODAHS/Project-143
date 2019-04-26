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
            print("in overlap");

            foreach (PuzzlePiece piece in pieces)
            {
                if (!piece.Equals(this) && piece.GetComponent<Collider2D>().IsTouching(GetComponent<Collider2D>()))
                {
                    return true;
                }
            }
            return false;

            //GameObject.FindGameObjectWithTag("roof").GetComponent<PolygonCollider2D>().enabled = false;
            //GameObject.FindGameObjectWithTag("wall").GetComponent<PolygonCollider2D>().enabled = false;
            //float z = transform.position.z;
            //Vector3 castPosition = transform.position;
            //transform.position = new Vector3(transform.position.x, transform.position.y, 1);
            //RaycastHit2D hit = Physics2D.Raycast(castPosition, Vector2.zero, 0);
            //print(hit.collider.gameObject);
            //if (hit.collider != null && hit.collider.gameObject != this.gameObject && hit.collider.GetComponent<DoorWindowPiece>() != null)
            //{
            //    print("inside loop");
            //    if (hit.collider.GetComponent<DoorWindowPiece>().placed){
            //        print("Overlapped!");
            //        return true;
            //    }
            //}

            //transform.position = new Vector3(transform.position.x, transform.position.y, z);

            //GameObject.FindGameObjectWithTag("roof").GetComponent<PolygonCollider2D>().enabled = true;
            //GameObject.FindGameObjectWithTag("wall").GetComponent<PolygonCollider2D>().enabled = true;

            //return false;
        }

        protected override void Start()
        {
            base.Start();
            position = GetComponent<Transform>().position;
            thisBound = GetComponent<Collider2D>().bounds;
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

            if (placementType == 1 && !overlap)
            {
                placed = true;
                pieces.Add(this);
                game.AddPiece(this);
                OnPieceCountIncrement();
                game.PlayDropDownSound();
            } else {
                if (pieces.Contains(this))
                {
                    pieces.Remove(this);
                    game.RemovePiece(this);
                }
                ResetTransform();
            }

            print("This piece is placed: " + placed);
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
