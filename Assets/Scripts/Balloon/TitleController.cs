using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class TitleController : MonoBehaviour
{
    private TMP_Text text;

    public GameObject pack;

    public InstructionController instruction;
    void Start()
    {
        text = this.gameObject.GetComponent<TMP_Text>();
        text.DOColor(new Color (0,0,0,1f),5f).SetEase(Ease.Linear);
        StartGame();
    }

    void StartGame()
    {
        pack.GetComponent<BoxCollider>().enabled = true;
        instruction.FirstInstruction();
    }

}
