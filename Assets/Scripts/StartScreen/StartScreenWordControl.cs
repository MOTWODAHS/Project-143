using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class StartScreenWordControl : TapableObject
{
    void Start()
    {
        this.gameObject.GetComponent<SpriteRenderer>().DOColor(new Color (0,0,0,1f),5f).SetEase(Ease.Linear);
        StartCoroutine(OpenCollider());
    }

    IEnumerator OpenCollider()
    {
        yield return new WaitForSeconds(5f);
        this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }

    public override void OnTap()
    {
        print("changeToMenu");
        SceneManager.LoadScene("Menu");
    }
}
