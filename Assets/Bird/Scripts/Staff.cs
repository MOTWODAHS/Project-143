using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Singing
{
    public class Staff : TapableObject
    {
        private GameController game;

        private void Start()
        {
            game = GameObject.FindGameObjectWithTag("gameController").GetComponent<GameController>();
        }

        public override void OnTap()
        {
            game.ToggleRecording();
            GetComponent<Collider2D>().enabled = false;
        }
    }
}
