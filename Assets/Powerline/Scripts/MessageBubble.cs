using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Talking
{
    class MessageBubble : MonoBehaviour
    {

        public GameController game;
       
        private void OnBecameInvisible()
        {
            game.SendMessageToNetwork();
        }
    }

}
