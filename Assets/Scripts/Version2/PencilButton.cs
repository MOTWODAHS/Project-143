using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class PencilButton : TapableObject3D
{
    public InstructionController instruction;
    public GameControllerv2 gameController;

    public GameObject[] textfield;

    public Text inputFieldText;

    public GameObject inputField;

    private Tweener tweForward;
    private Tweener tweBackward;
    public float movingTime = 3f;

    void Update()
    {
        if(gameController.selectedBalloonNumber != -1)
        {
            textfield[gameController.selectedBalloonNumber].GetComponent<TMP_Text>().text = inputFieldText.text;
            gameController.resultString = inputFieldText.text;
        }
    }

    public override void OnTap()
    {
        tweForward = inputField.GetComponent<RectTransform>().DOAnchorPos3DY(-500f,movingTime);
        tweForward.PlayForward();
        gameController.isEdited = false;
        instruction.DestroyHandInstruction(3);
    }

    public void OnButtonDown()
    {
        if(textfield[gameController.selectedBalloonNumber].GetComponent<TMP_Text>().text != "")
        {
            tweBackward = inputField.GetComponent<RectTransform>().DOAnchorPos3DY(-600f,movingTime);
            tweBackward.PlayForward();
            gameController.isEdited = true;
            instruction.SetHandInstruction(4);
        }
    }
}
