using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchScript.Gestures;
public class TapableObject3D : MonoBehaviour
{
    private TapGesture gesture;

    private void OnEnable()
    {
        gesture = GetComponent<TapGesture>();
        gesture.Tapped += tappedHandler;
    }

    private void OnDisable()
    {
        gesture.Tapped -= tappedHandler;
    }

    private void tappedHandler(object sender, System.EventArgs e)
    {
        Ray ray = Camera.main.ScreenPointToRay(gesture.ScreenPosition);
        RaycastHit hitinfo;

        if (Physics.Raycast(ray, out hitinfo) && hitinfo.transform == transform)
        {
            OnTap();
            print("Tap operation");
        }
    }

    public virtual void OnTap()
    {

    }
}
