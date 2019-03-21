using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchScript.Gestures;

namespace Singing
{
    
    public class Hand : MonoBehaviour
    {
        private FlickGesture gesture;
        private GameController game;

        private void OnEnable()
        {
            gesture = GetComponent<FlickGesture>();
            gesture.Flicked += flickedHandler;
            game = GameObject.FindGameObjectWithTag("gameController").GetComponent<GameController>();
        }

        private void OnDisable()
        {
            gesture.Flicked -= flickedHandler;
        }

        private void flickedHandler(object sender, System.EventArgs e)
        {
            if (gesture.ScreenFlickVector.x > 0 && gesture.ScreenFlickVector.y > 0)
            {
                game.SendBird();
                GetComponent<Collider2D>().enabled = false;
            }
        }
    }
}
