using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Talking
{
    class PowerlineButton : TapableObject
    {
        private GameController game;

        private void Start()
        {
            game = GameObject.FindGameObjectWithTag("gameController").GetComponent<GameController>();
        }

        public override void OnTap()
        {
            game.Proceed();
            GetComponent<Collider2D>().enabled = false;
            enabled = false;
        }
    }
}

