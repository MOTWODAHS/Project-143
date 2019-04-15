using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class TrolleyStart : TapableObject
{
    public TrolleyController Trolley;
    public GameObject EndScreenWord;
    public GameObject ClickHand;
    public bool flag = true;
    public GameObject jumpButton;

    public override void OnTap()
    {
        Trolley.flag = true;
        ClickHand.SetActive(false);
        Destroy(this.gameObject.GetComponent<BoxCollider2D>());
        StartCoroutine(JumpNextScene());
    }

    IEnumerator JumpNextScene()
    {
        EndScreenWord.GetComponent<SpriteRenderer>().DOColor(new Color (0,0,0,1f),4f).SetEase(Ease.Linear);
        if(flag)
        {
            yield return new WaitForSeconds(15);
            jumpButton.SetActive(true);
        }
        else
        {
            yield return new WaitForSeconds(12);
            jumpButton.SetActive(true);
        }
    }
}
