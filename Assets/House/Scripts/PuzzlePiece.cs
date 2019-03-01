using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TouchScript.Behaviors;
using TouchScript.Gestures.TransformGestures;

public class PuzzlePiece : MonoBehaviour
{
    private TransformGesture gesture;
    private Transformer transformer;


    private bool enlarged = false;

    protected Vector3 enlargedScale { get; set; }

    protected Vector3 normalScale { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        normalScale = transform.localScale;
        enlargedScale = 1.2f * transform.localScale;
    }

    public Transformer GetTransformer()
    {
        return transformer;
    }

    void ToggleScale()
    {
        if (enlarged)
        {
            transform.localScale = normalScale;
        }
        else
        {
            transform.localScale = enlargedScale;
        }
        enlarged = !enlarged;
    }

    private void OnEnable()
    {
        gesture = GetComponent<TransformGesture>();
        transformer = GetComponent<Transformer>();

        transformer.enabled = false;
        // Subscribe to gesture events
        gesture.TransformStarted += transformStartedHandler;
        gesture.TransformCompleted += transformCompletedHandler;
    }

    private void transformStartedHandler(object sender, EventArgs e)
    {
        transformer.enabled = true;
        ToggleScale();
    }

    protected void transformCompletedHandler(object sender, EventArgs e)
    {
        transformer.enabled = false;
        ToggleScale();
    }

    void OnTriggerStay2D(Collider2D other)
    {

        if (other.gameObject.name.Equals(this.gameObject.name + "InPlace") && !transformer.enabled)
        {
            Debug.Log(other.gameObject.name);
            this.gameObject.SetActive(false);


            foreach(SpriteRenderer spriteRenderer in other.GetComponentsInChildren<SpriteRenderer>())
            {
                spriteRenderer.enabled = true;
            }

            other.GetComponent<Collider2D>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
