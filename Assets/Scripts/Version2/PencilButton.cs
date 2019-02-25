using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PencilButton : MonoBehaviour
{

    public GameControllerv2 gameController;

    public GameObject[] textfield;

    public Text inputFieldText;

    public Animator inputFieldAnimator;

    void Start()
    {
        
    }

    void Update()
    {
        if(gameController.selectedBalloonNumber != -1)
        {
            if(inputFieldText.text.Length <= 5)
                textfield[gameController.selectedBalloonNumber].GetComponent<TMP_Text>().text = inputFieldText.text;
            else
                textfield[gameController.selectedBalloonNumber].GetComponent<TMP_Text>().text = inputFieldText.text.Substring(0, 5);
        }
    }

    void OnMouseDown()
    {
        inputFieldAnimator.SetTrigger("DowntoUp");
        gameController.isEdited = false;
    }

    public void OnButtonDown()
    {
        if(textfield[gameController.selectedBalloonNumber].GetComponent<TMP_Text>().text != null)
        {
            inputFieldAnimator.SetTrigger("UptoDown");
            gameController.isEdited = true;
        }
    }
}
