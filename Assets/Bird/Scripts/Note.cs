using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchScript.Gestures;

public class Note : MonoBehaviour
{
    private TapGesture gesture;
    private GameController game;

    private void Start()
    {
        game = GameObject.FindGameObjectWithTag("gameController").GetComponent<GameController>();
    }
    private void OnEnable()
    {
        gesture = GetComponent<TapGesture>();
        gesture.Tapped += tappedHandler;
    }

    private void OnDisable()
    {
        gesture.Tapped -= tappedHandler;
    }

    private void tappedHandler(object sender, System.EventArgs e)
    {
        Vector2 ray = Camera.main.ScreenToWorldPoint(gesture.ScreenPosition);

        RaycastHit2D hit = Physics2D.Raycast(ray, Vector2.zero);

        if (hit.transform == transform)
        {
            this.GetComponent<AudioSource>().Play();
            game.AddNote(this.name);
        }
    }
}
