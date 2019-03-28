using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBoardController : MonoBehaviour
{
    public string inputString;
    public void AddInput(string rev_string)
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
}
