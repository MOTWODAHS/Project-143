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
        else if(string.Equals(interaction_Name, "Bird"))
        {
            SceneManager.LoadScene("Bird");
        }
        else if(string.Equals(interaction_Name, "PowerLine"))
        {
            SceneManager.LoadScene("PowerlineScene");
        }
        else if(string.Equals(interaction_Name, "House"))
        {
            SceneManager.LoadScene("House");
        }
        else
        {
            print("Coming soon");
        }
    }
    /* 
    public void OnMouseDown()
    {
        if(string.Equals(interaction_Name,"Balloon"))
        {
            SceneManager.LoadScene("Balloon");
        } 
        else if(string.Equals(interaction_Name, "Bird"))
        {
            SceneManager.LoadScene("Bird");
        }
        else
        {
            print("Coming soon");
        }
    }
    */
}
