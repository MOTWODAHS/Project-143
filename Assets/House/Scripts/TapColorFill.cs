using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Loving
{
    class TapColorFill : TapableObject
    {
        private GameController game;

        private void Start()
        {
            game = GameObject.FindGameObjectWithTag("gameController").GetComponent<GameController>();
        }
        public override void OnTap()
        {
            if (game.colorSelected && game.selectedColor != null)
            {
                print("inside");
                print("Selected Color is: " + game.selectedColor);
                if (GetComponent<DoorWindowPiece>() != null) {
                    print("is placed is: " + GetComponent<DoorWindowPiece>().isPlaced());
                }

                if ((GetComponent<BackgroundPuzzlePiece>() != null) ||
                    (GetComponent<DoorWindowPiece>() != null && GetComponent<DoorWindowPiece>().isPlaced()))
                GetComponent<FillInColor>().FillColor(game.selectedColor);
            }
        }
    }
}

