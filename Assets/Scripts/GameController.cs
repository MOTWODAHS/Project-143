using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Animator uppart_animator;
    public Animator downpart_animator;

    public GameObject active_balloon;

    public GameObject[] balloon;


    public void Pack_Fade_Out()
    {
        uppart_animator.SetTrigger("Tear");
        downpart_animator.SetTrigger("Tear");
    }

    public void Set_Balloon_Collider()
    {
        foreach(GameObject bal in balloon)
        {
            bal.GetComponent<CapsuleCollider>().enabled = true;
        }
    }
}
