using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ProceedButton : MonoBehaviour
{
    private static GameObject[] masks;
    private static int stage = 0;
    private static GameObject hideButton;
    private static GameObject enterName;
    public GameObject[] instanceMasks;
    public GameObject hideButtonInstance;
    public GameObject enterNameInstance;

    void Start()
    {
        masks = instanceMasks;
        hideButton = hideButtonInstance;
        enterName = enterNameInstance;
        Debug.Log(masks);
    }
    public static void Advance()
    {
        hideButton.SetActive(false);
    }

    public static void HideAdvance()
    {
        hideButton.SetActive(true);
    }

    public static void Next()
    {
        if (stage < 2) hideButton.SetActive(true);
        if (stage < 2)
        {
            masks[stage].SetActive(false);
        } else {
            enterName.transform.DOMoveY(150, 2f);
        }


        stage++;
    }
}
