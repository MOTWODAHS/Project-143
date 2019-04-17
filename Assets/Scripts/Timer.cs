using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public GameControllerv2 gameController;
    public float jumptime = 60f;

    [SerializeField]
    private int process = 0;
    void Update()
    {
        jumptime -= Time.deltaTime;
        
        switch(process)
        {
            case 0:
                if(gameController.isStep1Finished)
                {
                    jumptime = 60f;
                    process++;
                }
                break;
            case 1:
                if(gameController.isStep2Finished)
                {
                    jumptime = 60f;
                    process++;
                }
                break;
            case 2:
                if(gameController.isStep3Finished)
                {
                    jumptime = 60f;
                    process++;
                }
                break;
            case 3:
                if(gameController.isStep4Finished)
                {
                    jumptime = 120f;
                    process++;
                }
                break;
            case 4:
                if(gameController.isEdited)
                {
                    jumptime = 120f;
                    process++;
                }
                break;
            case 5:
                if(gameController.isLanuched)
                {
                    jumptime = 60f;
                    process++;
                }
                break;
            default:
                break;

        }

        if(jumptime <= 0f)
        {
            SceneManager.LoadScene("HoldScene");
        }
    }
}
