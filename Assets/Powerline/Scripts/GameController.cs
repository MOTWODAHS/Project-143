using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Powerline
{
    class GameController : MonoBehaviour, IGameController
    {

        private int gameStage = 0;

        private bool gameIsOver = false;

        private IGameController game;

        private delegate void StageTransition();

        private StageTransition[] transitions;

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



        // Update is called once per frame
        void Update()
        {

        }
    }
}
