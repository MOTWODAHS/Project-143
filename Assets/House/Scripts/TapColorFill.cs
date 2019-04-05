﻿using System.Collections;
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
            if (game.selectedColor != null)
            {
                if ((GetComponent<BackgroundPuzzlePiece>() != null) ||
                    (GetComponent<DoorWindowPiece>() != null && GetComponent<DoorWindowPiece>().isPlaced()))
                GetComponent<FillInColor>().FillColor(game.selectedColor);
            }
        }
    }
}
