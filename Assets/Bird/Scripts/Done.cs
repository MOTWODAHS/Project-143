using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Singing
{

    public class Done : TapableObject
    {
        private GameController game;

        private void Start()
        {
            game = GameObject.FindGameObjectWithTag("gameController").GetComponent<GameController>();
        }

        public override void OnTap()
        {
            game.Proceed();
        }
    
    }
}
