using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Loving
{
    class BackgroundPuzzlePiece : MonoBehaviour
    {
        private Color color;

        public void HoverColor()
        {
            color = new Color(0.5f, 0.5f, 0.5f);
            GetComponent<SpriteRenderer>().material.SetColor("_Color", color);
        }

        public void DisableHoverColor()
        {
            color = new Color(1.0f, 1.0f, 1.0f);
            GetComponent<SpriteRenderer>().material.SetColor("_Color", color);
        }

        //private void OnTriggerEnter2D(Collider2D other)
        //{
        //    if (other.GetComponent<DoorWindowPiece>() != null) HoverColor();
        //}

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.GetComponent<DoorWindowPiece>() != null) DisableHoverColor();
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.GetComponent<DoorWindowPiece>() != null && !other.GetComponent<DoorWindowPiece>().GetTransformer().enabled)
            {
                DisableHoverColor();
            }

            if (other.GetComponent<DoorWindowPiece>() != null && other.GetComponent<DoorWindowPiece>().GetTransformer().enabled)
            {
                HoverColor();
            }
        }
    }
}
