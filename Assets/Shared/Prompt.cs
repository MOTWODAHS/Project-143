using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Singing;
using Loving;

public class Prompt : MonoBehaviour
{

    public string prompt;
    private string displayedText;
    private IGameController game;

    void Start()
    {
        displayedText = "";
        StartCoroutine(DisplayPrompt());
        game = (IGameController) GameObject.FindGameObjectWithTag("gameController").GetComponent(typeof(IGameController));
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
