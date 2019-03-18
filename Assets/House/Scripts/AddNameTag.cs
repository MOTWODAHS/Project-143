using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AddNameTag : MonoBehaviour
{

    public Text text;
    void Update()
    {
        GetComponentInChildren<TextMeshPro>().SetText(text.text);
        GetComponentInChildren<TextMeshPro>().text = text.text;
    }
}
