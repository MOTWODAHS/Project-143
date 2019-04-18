using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneTimer : MonoBehaviour
{
    public float jumptime = 120f;
    void Update()
    {
        jumptime -= Time.deltaTime;
        if(jumptime <= 0f)
        {
            SceneManager.LoadScene("HoldScene");
        }
    }
}
