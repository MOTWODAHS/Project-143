using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchScript.Gestures;

public class Note : TapableObject
{
    private GameController game;

    private void Start()
    {
        game = GameObject.FindGameObjectWithTag("gameController").GetComponent<GameController>();
    }

    public override void OnTap()
    {
        this.GetComponent<AudioSource>().Play();
        game.AddNote(this.name);
    }
}
