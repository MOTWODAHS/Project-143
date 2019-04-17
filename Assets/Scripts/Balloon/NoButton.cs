using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoButton : TapableObject3D
{
    public GameObject itself;
    public override void OnTap()
    {
        itself.SetActive(false);
    }
}
