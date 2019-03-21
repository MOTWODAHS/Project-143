using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchScript.Gestures;
using DG.Tweening;

namespace Singing
{
    public class Note : TapableObject
    {
        private GameController game;

        private void Start()
        {
            game = GameObject.FindGameObjectWithTag("gameController").GetComponent<GameController>();
        }

        public void Destroy()
        {
            GetComponent<SpriteRenderer>().DOFade(0f, 1f).OnComplete(() =>
            {
                Destroy(this.gameObject);
            });
        }

        public override void OnTap()
        {
            this.GetComponent<AudioSource>().Play();
            game.AddNote(this.name);
        }


    }
}
