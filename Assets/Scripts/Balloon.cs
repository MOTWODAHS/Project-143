using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    public int Number = 0;
    public Animator[] balloon;

    public GameController gamecontroller;

    void OnMouseDown()
    {
        if(this.gameObject.GetComponent<Animator>().enabled == true)
        {
            for(int i = 0; i < balloon.Length; i++)
            {
                if(i == Number)
                {
                    this.gameObject.GetComponent<Animator>().SetTrigger("Scale");
                    //Destroy(this.gameObject.GetComponent<CapsuleCollider>());
                    StartCoroutine(DisableAnimator(this.gameObject.GetComponent<Animator>()));
                }
                else
                {
                    balloon[i].gameObject.GetComponent<Animator>().SetTrigger("Fall");
                    //Destroy(balloon[i].gameObject.GetComponent<CapsuleCollider>());
                    StartCoroutine(DisableAnimator(balloon[i].gameObject.GetComponent<Animator>()));
                }
            }
        }
        else
        {
            if(gamecontroller.active_balloon != this.gamecontroller)
            {
                Material mat = this.gameObject.GetComponent<SpriteRenderer>().material;
                this.gameObject.GetComponent<SpriteRenderer>().material = gamecontroller.active_balloon.GetComponent<SpriteRenderer>().material;
                gamecontroller.active_balloon.GetComponent<SpriteRenderer>().material = mat;
            }
        }
    }

    IEnumerator DisableAnimator(Animator anim)
    {
        yield return new WaitForSeconds(2.5f);
        anim.enabled = false;
        gamecontroller.active_balloon = this.gameObject;
    }

}
