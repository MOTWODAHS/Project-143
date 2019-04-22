using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PowerlineConfirmButton : TapableObject3D
{
    public IGameController game;

    private void Start()
    {
        game = (IGameController)GameObject.FindGameObjectWithTag("gameController").GetComponent(typeof(IGameController));
    }
    public override void OnTap()
    {
        string message = GetComponentInParent<KeyBoardController>().inputString;

        if (message.Replace(" ", "").Length > 0){
            game.Proceed();
        }
       
    }
}

