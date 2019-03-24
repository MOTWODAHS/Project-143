using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Loving
{
    class GameController : MonoBehaviour, IGameController
    {
        [Header("Cover")]
        public GameObject cover;

        private int gameStage;

        void Start()
        {
            gameStage = 0;
        }

        void IGameController.StartGame()
        {
            cover.SetActive(false);
        }

        
    }
}
