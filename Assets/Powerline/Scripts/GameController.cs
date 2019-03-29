using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Powerline
{
    class GameController : MonoBehaviour, IGameController
    {

        private int gameStage = 0;
        private bool gameIsOver = false;

        private delegate void StageTransition();

        private StageTransition[] transitions;

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
            throw new System.NotImplementedException();
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
