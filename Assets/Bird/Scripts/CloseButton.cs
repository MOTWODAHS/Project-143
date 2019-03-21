﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Singing
{
    public class CloseButton : TapableObject
    {
        private GameController game;

        private void Start()
        {
            game = GameObject.FindGameObjectWithTag("gameController").GetComponent<GameController>();
        }

        public override void OnTap()
        {
            game.ToggleRecording();
        }
    }
}
