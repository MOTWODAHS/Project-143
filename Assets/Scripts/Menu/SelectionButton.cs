using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectionButton : TapableObject
{
    public string interaction_Name;
    
    public override void OnTap()
    {
        if(string.Equals(interaction_Name,"Balloon"))
        {
            SceneManager.LoadScene("Balloon");
        }
        else
        {
            print("Coming soon");
        }
    }
}
