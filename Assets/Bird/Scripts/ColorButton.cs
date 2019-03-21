using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Singing
{
    public class ColorButton : TapableObject
    {
        private GameController game;

        private void Start()
        {
            game = GameObject.FindGameObjectWithTag("gameController").GetComponent<GameController>();
        }
        
        public override void OnTap()
        {
            game.ChangeBirdColor(this.gameObject.name);
            game.Proceed();
        }
    }
}
