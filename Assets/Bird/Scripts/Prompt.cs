using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Singing;

public class Prompt : MonoBehaviour
{

    public string prompt;
    private string displayedText;
    private GameController game;

    void Start()
    {
        displayedText = "";
        StartCoroutine(DisplayPrompt());
        game = GameObject.FindGameObjectWithTag("gameController").GetComponent<GameController>();
    }

    private IEnumerator DisplayPrompt()
    {
        foreach(char chr in prompt)
        {
            displayedText += chr;
            this.GetComponent<TextMeshProUGUI>().text = displayedText;
            if (chr != ' ')
            {
                yield return new WaitForSeconds(0.2f);
            }
        }
        game.StartGame();
    }


}
