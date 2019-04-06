using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Loving
{
    class BackgroundPuzzlePiece : MonoBehaviour
    {
        private Color color;
        private Color saveColor = new Color(1f,1f,1f);

        public void HoverColor()
        {
            color = new Color(0.5f, 0.5f, 0.5f);
            GetComponent<SpriteRenderer>().material.SetColor("_Color", color);
        }

        public void DisableHoverColor()
        {
            GetComponent<SpriteRenderer>().material.SetColor("_Color", saveColor);
        }

        public void setColor(Color c)
        {
            saveColor = c;
        }

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
