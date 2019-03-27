using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchScript.Behaviors;
using TouchScript.Gestures.TransformGestures;
using System;

namespace Loving
{
    class PuzzlePieceLast : MonoBehaviour
    {
        private Vector3 position;

        private PuzzlePiece puzzlePiece;

        private void ResetTransform()
        {
            transform.position = position;
        }

        void Start()
        {
            puzzlePiece = GetComponent<PuzzlePiece>();
            position = base.GetComponent<Transform>().position;
        }

        private void FixedUpdate()
        {
            if (!puzzlePiece.GetTransformer().enabled)
            {
                ResetTransform();
            }
        }

    }
}
