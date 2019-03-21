using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Singing
{
    public class NewSong : TapableObject
    {
        private GameController game;

        private void Start()
        {
            game = GameObject.FindGameObjectWithTag("gameController").GetComponent<GameController>();
        }

        public override void OnTap()
        {
            game.ClearSong();
        }

    }
}

