using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBoardController : MonoBehaviour
{

    public int interactionCode = 0;
    public string inputString;

    [SerializeField]
    private int wordLimit = 20;

    void Start()
    {
        switch(interactionCode)
        {
            case 1:
                wordLimit = 14;
                break;
            case 2:
                wordLimit = 7;
                break;
            case 3:
                wordLimit = 12;
                break;
            case 4:
                wordLimit = 14;
                break;
            default:
                wordLimit = 20;
                break;
        }
    }
    public void AddInput(string rev_string)
    {
        if(inputString.Length < wordLimit)
        {
            if(string.Equals(rev_string,"Backspace"))
            {
                if(inputString.Length > 0)
                {
                    inputString = inputString.Substring(0,inputString.Length - 1);
                }
            }
            else
            {
                inputString += rev_string;
            }
        }
        else
        {
            if(string.Equals(rev_string,"Backspace"))
            {
                if(inputString.Length > 0)
                {
                    inputString = inputString.Substring(0,inputString.Length - 1);
                }
            }
        }
    }
}
