using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Singing
{

    class PlaySongButton : TapableObject
    {

        private GameController game;

        private void Start()
        {
            game = GameObject.FindGameObjectWithTag("gameController").GetComponent<GameController>();
        }

        public override void OnTap()
        {
            StartCoroutine(game.ComposedNotesPlayEnum());
            StartCoroutine(game.PlaySongEnum());
        }
    }
}
