using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextInputField : MonoBehaviour
{
    public KeyBoardController controller;
    void Update()
    {
        GetComponent<TextMeshPro>().text = controller.inputString;
    }
}

