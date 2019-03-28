using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InstructionController : MonoBehaviour
{
    public GameObject[] handUI;
    public GameObject[] endPoint;

    private SpriteRenderer[] handRender = new SpriteRenderer[5];

    private Tweener[] handMotion = new Tweener[5];

    private Tweener[] handColor = new Tweener[5];

    private Vector3 hand2StartPosition;
    void Start()
    {
        for(int i = 0; i < handRender.Length; i++)
        {
            handRender[i] = handUI[i].GetComponent<SpriteRenderer>();
        }
        hand2StartPosition = handUI[2].transform.position;
    }

    public void SetHandInstruction(int number)
    {
        if(number != 2)
        {
            handMotion[number] = handUI[number].transform.DOMove(endPoint[number].transform.position,2).SetLoops(-1);
            handColor[number] = handRender[number].DOColor(handRender[number].color + new Color (0,0,0,1f),2).SetLoops(-1);
        }
        else
        {
            hand2Up();
        }
        
    }

    public void DestroyHandInstruction(int number)
    {
        handMotion[number].Kill();
        handColor[number].Kill();
        handUI[number].SetActive(false);
    }

    private void hand2Up()
    {
        handColor[2] = handRender[2].DOColor(handRender[2].color + new Color (0,0,0,1f),2);
        handMotion[2] = handUI[2].transform.DOMove(endPoint[2].transform.position,2).OnComplete(hand2Down);
    }

    private void hand2Down()
    {  
        handColor[2] = handRender[2].DOColor(handRender[2].color + new Color (0,0,0,-1f),2);
        handMotion[2] = handUI[2].transform.DOMove(hand2StartPosition,2).OnComplete(hand2Up);       
    }

    public void FirstInstruction()
    {
        SetHandInstruction(0);
    }

}
