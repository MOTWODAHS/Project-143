using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AddName : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SendName()
    {
        Debug.Log("sendName");
        GameObject o = GameObject.FindGameObjectWithTag("panel");
        o.GetComponentInChildren<TextMeshPro>().SetText(this.GetComponentInChildren<Text>().text);
    }
}
