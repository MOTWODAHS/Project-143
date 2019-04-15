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
        game = (IGameController)GameObject.FindGameObjectWithTag("gameController").GetComponent(typeof(IGameController));
        StartCoroutine(DisplayPrompt());      
    }

    private IEnumerator DisplayPrompt()
    {
        game.StartGame();
        TextMeshProUGUI textmeshPro = GetComponent<TextMeshProUGUI>();
        textmeshPro.text = prompt;
        float startTime = Time.time;
        while(Time.time - startTime < 5f)
        {
            float a = (Time.time - startTime) / 5f;
            //Debug.Log(GetComponent<TextMeshPro>().color);
            textmeshPro.color = new Color(1, 1, 1, a);
            yield return new WaitForSeconds(0.2f);
        }
        GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 1);
    }


}
