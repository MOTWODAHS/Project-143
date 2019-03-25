using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Loving
{
    class NameInputField : TapableObject
    {
        private IGameController game;

        private void Start()
        {
            game = (IGameController)GameObject.FindGameObjectWithTag("gameController").GetComponent(typeof(IGameController));
        }

        private void OnDisable()
        {
            game.Proceed();
        }

        public override void OnTap()
        {
            this.GetComponent<InputField>().Select();
            this.GetComponent<InputField>().ActivateInputField();
        }
    }
}
