using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Talking
{
    class PowerlineConfirmButton : TapableObject3D
    {
        public GameController game;

        public override void OnTap()
        {

            game.Proceed();
        }
    }

}
