using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BalloonSpawner : MonoBehaviour
{
    public GameObject[] balloons;

    public GameObject spawnPoint;

    public NetworkingController networkingController;

    private int randomNum;
    public void createRandomBalloon()
    {
        randomNum = Random.Range(0,3);
        GameObject balloon = Instantiate(balloons[randomNum]);
        balloon.transform.position = spawnPoint.transform.position;
        balloon.transform.DOMoveY(7f,11f).SetEase(Ease.Linear);
        StartCoroutine(SendBalloons(randomNum));
    }

    IEnumerator SendBalloons(int num)
    {
        yield return new WaitForSeconds(11f);
        networkingController.SendAction(1,num,"WELCOME");
    }
}
