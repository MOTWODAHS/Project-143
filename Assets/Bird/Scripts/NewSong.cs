using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSong : TapableObject
{
    private GameController game;

    private void Start()
    {
        Debug.Log("Start!");
        game = GameObject.FindGameObjectWithTag("gameController").GetComponent<GameController>();
    }

    public override void OnTap()
    {
        Debug.Log("Clear song");
        game.ClearSong();
    }
}
