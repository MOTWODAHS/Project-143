using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class TitleController : MonoBehaviour
{
    public GameObject title;

    private TMP_Text text;

    public GameObject pack;

    public InstructionController instruction;
    void Start()
    {
        text = title.GetComponent<TMP_Text>();
        this.gameObject.GetComponent<Text>().DOText("Who in your life has helped you smile?",8f).SetEase(Ease.Linear).OnComplete(StartGame);
    }

    void Update()
    {
        text.text = this.gameObject.GetComponent<Text>().text;
    }

    void StartGame()
    {
        pack.GetComponent<BoxCollider>().enabled = true;
        instruction.FirstInstruction();
    }

}
