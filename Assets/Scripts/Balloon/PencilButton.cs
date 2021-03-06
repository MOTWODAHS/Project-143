﻿using System.Collections;
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

    public GameObject keyboard;

    public KeyBoardController keyBoardController;

    private Tweener tweForward;
    private Tweener tweBackward;
    public float movingTime = 3f;

    public bool isInstructionSet = false;

    void Update()
    {
        if(gameController.selectedBalloonNumber != -1)
        {
            textfield[gameController.selectedBalloonNumber].GetComponent<TMP_Text>().text = keyBoardController.inputString;
            gameController.resultString = keyBoardController.inputString;
        }
    }

    public override void OnTap()
    {
        gameController.pencilButton[gameController.selectedBalloonNumber].GetComponent<SpriteRenderer>().color = new Color (1f,1f,1f,0f);
        gameController.pencilButton[gameController.selectedBalloonNumber].GetComponent<Collider>().enabled = false;
        keyboard.SetActive(true);
        gameController.weight[gameController.selectedBalloonNumber].GetComponent<BoxCollider>().enabled = false;
        tweForward = keyboard.transform.DOLocalMoveY(6.3f,movingTime);
        tweForward.PlayForward();
        gameController.isEdited = false;
        //instruction.DestroyHandInstruction(3);
    }

    public void OnButtonDown()
    {
        if(textfield[gameController.selectedBalloonNumber].GetComponent<TMP_Text>().text != "")
        {
            tweBackward = keyboard.transform.DOLocalMoveY(4.3f,movingTime);
            tweBackward.PlayForward();
            //gameController.pencilButton[gameController.selectedBalloonNumber].GetComponent<SpriteRenderer>().color = new Color (1f,1f,1f,1f);
            //gameController.pencilButton[gameController.selectedBalloonNumber].GetComponent<Collider>().enabled = true;
            gameController.isEdited = true;
            gameController.weight[gameController.selectedBalloonNumber].GetComponent<BoxCollider>().enabled = true;
            if(!isInstructionSet)
            {
                instruction.SetHandInstruction(4);
                isInstructionSet = true;
            }
        }
    }
}
