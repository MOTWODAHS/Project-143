using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class StartScreenWordControl : MonoBehaviour
{
    void Start()
    {
        this.gameObject.GetComponent<SpriteRenderer>().DOColor(new Color (0,0,0,1f),5f).SetEase(Ease.Linear);
    }
}
