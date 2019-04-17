using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class TrolleyStart : TapableObject
{
    public TrolleyController Trolley;
    public GameObject[] ScreenWord;
    public GameObject ClickHand;
    public bool flag = true;
    public GameObject jumpButton;

    void Start()
    {
        if(flag)
        {
            OnTap();
        }
    }

    public override void OnTap()
    {
        Trolley.flag = true;
        ClickHand.SetActive(false);
        Destroy(this.gameObject.GetComponent<BoxCollider2D>());
        StartCoroutine(JumpNextScene());

        Sequence seq = DOTween.Sequence();

        if(flag)
        {
            foreach(GameObject word in ScreenWord)
            {
                seq.Append(word.GetComponent<SpriteRenderer>().DOFade(1f,5f));
            }
        }
        else
        {
            foreach(GameObject word in ScreenWord)
            {
                seq.Append(word.GetComponent<SpriteRenderer>().DOFade(1f,3f));
            }
        }
    }

    IEnumerator JumpNextScene()
    {
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
