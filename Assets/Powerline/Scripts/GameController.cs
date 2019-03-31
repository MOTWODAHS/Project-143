using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Talking
{
    class GameController : MonoBehaviour, IGameController
    {

        private int gameStage = 0;

        private bool gameIsOver = false;

        private IGameController game;

        private delegate void StageTransition();

        private StageTransition[] transitions;

        Bounds bound;

        [Header("Before Picking Up An Pole")]
        public Collider2D destinationPoleCollider;

        private void Start()
        {
            game = (IGameController)GameObject.FindGameObjectWithTag("gameController").GetComponent(typeof(IGameController));
        }

        public int GetGameStage()
        {
            throw new System.NotImplementedException();
        }

        public void Proceed()
        {
            throw new System.NotImplementedException();
        }

        public void StartGame()
        {
            destinationPoleCollider.enabled = true;
        }

        public Bounds getBound()
        {
            return bound;
        }

        public void setBound(Bounds bound)
        {
            this.bound = bound;
        }
    }
}
