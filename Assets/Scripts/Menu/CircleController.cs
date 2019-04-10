using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CircleController : MonoBehaviour
{
    public GameObject[] CircleArray;

    public float transmitTime = 1f;
    void Start()
    {
        StartCoroutine(CircleLoop());
    }

    IEnumerator CircleLoop()
    {
        this.gameObject.transform.DOScale(new Vector3 (0.3f,0.3f,0.3f), transmitTime/2).SetEase(Ease.Linear).SetLoops(-1,LoopType.Yoyo);
        foreach(GameObject circle in CircleArray)
        {
            circle.SetActive(true);
            circle.transform.DOScale(new Vector3 (0.7f,0.7f,0.7f),transmitTime).SetEase(Ease.Linear).SetLoops(-1);
            circle.GetComponent<SpriteRenderer>().DOColor(new Color (1f,1f,1f,0f),transmitTime).SetEase(Ease.Linear).SetLoops(-1);
            yield return new WaitForSeconds(1f);
        }
    }
}
