using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuTimer : MonoBehaviour
{
    public float jumpTime = 120f;
    void Update()
    {
        jumpTime -= Time.deltaTime;
        if(jumpTime <= 0f)
        {
            SceneManager.LoadScene("HoldScene");
        }
    }
}
