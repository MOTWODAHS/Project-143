using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Blueprint : MonoBehaviour
{
    public GameObject altBlueprint;

    private void OnDisable()
    {
        
    }

    public void ProceedToEnd()
    {
        Camera.main.cullingMask &= ~(1 << LayerMask.NameToLayer("Default"));
        altBlueprint.SetActive(true);
        Camera.main.DOOrthoSize(8.15f, 5f);
    }
}
