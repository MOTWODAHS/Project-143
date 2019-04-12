using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class HoldSceneWordController : MonoBehaviour
{
    private SpriteRenderer sr;
    private Color color;
    void Start()
    {
        sr = this.gameObject.GetComponent<SpriteRenderer>();
        sr.DOColor(new Color (0f,0f,0f,1f),5f).SetEase(Ease.Linear).OnComplete(WordLoop);
    }

    void WordLoop()
    {
        StartCoroutine(LoopAgain());
    }

    IEnumerator LoopAgain()
    {
        yield return new WaitForSeconds(15f);
        sr.color = new Color (0f,0f,0f,0f);
        sr.DOColor(new Color (0f,0f,0f,1f),5f).SetEase(Ease.Linear).OnComplete(WordLoop);
    }
}
