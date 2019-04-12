using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InstructionController : MonoBehaviour
{
    public GameObject[] handUI;

    public void SetHandInstruction(int number)
    {
        switch(number)
        {
            case 0:
                handUI[0].SetActive(true);
                break;
            case 1:
                handUI[1].SetActive(true);
                break;
            case 2:
                handUI[2].SetActive(true);
                handUI[2].transform.DOMoveY(7.87f,1.8f).SetLoops(-1,LoopType.Yoyo).SetEase(Ease.Linear);
                break;
            case 4:
                handUI[3].SetActive(true);
                break;
            default:
                break;
        }
        
    }

    public void DestroyHandInstruction(int number)
    {
        if(number < 3)
            handUI[number].SetActive(false);
        if(number == 4)
            handUI[3].SetActive(false);
    }

    public void FirstInstruction()
    {
        SetHandInstruction(0);
    }

}
