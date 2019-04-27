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
    public GameObject menuButton;

    public AudioSource audioSource;

    public AudioClip trolleyChime;

    public BalloonSpawner balloonSpawner;

    void Start()
    {
        if(flag)
        {
            OnTap();
        }
    }

    public override void OnTap()
    {
        StartCoroutine(TrolleyGo());
    }

    IEnumerator JumpNextScene()
    {
        if(flag)
        {
            menuButton.SetActive(true);
            yield return new WaitForSeconds(9);
            jumpButton.SetActive(true);
        }
        else
        {
            balloonSpawner.createRandomBalloon();
            yield return new WaitForSeconds(14);
            jumpButton.SetActive(true);
        }
    }

    public void OnDragEnd()
    {
        StartCoroutine(TrolleyGo());
    }

    IEnumerator TrolleyGo()
    {
        ClickHand.SetActive(false);
        Destroy(this.gameObject.GetComponent<BoxCollider2D>());
        audioSource.PlayOneShot(trolleyChime);
        yield return new WaitForSeconds(1.5f);
        Trolley.flag = true;
        StartCoroutine(JumpNextScene());

        Sequence seq = DOTween.Sequence();

        if(flag)
        {
            foreach(GameObject word in ScreenWord)
            {
                seq.Append(word.GetComponent<SpriteRenderer>().DOFade(1f,7f));
            }
        }
        else
        {
            ScreenWord[0].GetComponent<SpriteRenderer>().DOFade(1f,7f);
            ScreenWord[1].GetComponent<SpriteRenderer>().DOFade(1f,7f);
            yield return new WaitForSeconds(7f);
            ScreenWord[2].GetComponent<SpriteRenderer>().DOFade(1f,7f);
            ScreenWord[3].GetComponent<SpriteRenderer>().DOFade(1f,7f);
        }
    }
}
