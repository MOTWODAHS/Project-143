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
    private static GameObject blueprint;
    public GameObject[] instanceMasks;
    public GameObject hideButtonInstance;
    public GameObject enterNameInstance;
    public GameObject blueprintInstance;

    void Start()
    {
        masks = instanceMasks;
        hideButton = hideButtonInstance;
        enterName = enterNameInstance;
        blueprint = blueprintInstance;
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
        Debug.Log("Current Stage is " + stage);
        if (stage < 2) hideButton.SetActive(true);
        if (stage < 3)
        {
            masks[stage].SetActive(false);
        } else {
            enterName.transform.DOMoveY(150, 2f);
        }
        if (stage == 4){
            blueprint.GetComponent<Blueprint>().ProceedToEnd();
        }

        stage++;
    }
}
