﻿using System.Collections;
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
            yield return new WaitForSeconds(18);
            jumpButton.SetActive(true);
            menuButton.SetActive(true);
        }
        else
        {
            yield return new WaitForSeconds(16);
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
            foreach(GameObject word in ScreenWord)
            {
                seq.Append(word.GetComponent<SpriteRenderer>().DOFade(1f,4f));
            }
        }
    }
}
