using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Starting : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartSequence());
    }

    IEnumerator StartSequence()
    {
        float startTime = Time.time;
        yield return new WaitForSeconds(2f);

        GetComponent<RectTransform>().DOAnchorPosY(-70, 3f);
    }
}
