using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Loving
{
    class EditName : TapableObject
    {
        private IGameController game;

        private void Start()
        {
           game = (IGameController)GameObject.FindGameObjectWithTag("gameController").GetComponent(typeof(IGameController));
        }
        public override void OnTap()
        {
            game.Proceed();
        }
    }
}
