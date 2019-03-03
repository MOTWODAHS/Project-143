using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceedButton : MonoBehaviour
{
    private static GameObject[] masks;
    private static int stage = 0;
    private static GameObject hideButton;
    public GameObject[] instanceMasks;
    public GameObject hideButtonInstance;

    void Start()
    {
        masks = instanceMasks;
        hideButton = hideButtonInstance;
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
        hideButton.SetActive(true);
        masks[stage].SetActive(false);
        stage++;
    }
}
