using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TouchScript.Behaviors;
using TouchScript.Gestures.TransformGestures;

public class PuzzlePiece : MonoBehaviour
{
    protected TransformGesture gesture;
    protected Transformer transformer;
    protected Vector3 position;


    private bool enlarged = false;

    protected Vector3 enlargedScale { get; set; }

    protected Vector3 normalScale { get; set; }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        normalScale = transform.localScale;
        enlargedScale = 1.2f * transform.localScale;
        position = GetComponent<Transform>().position;
    }

    private void ResetTransform()
    {
        transform.position = position;
    }

    public Transformer GetTransformer()
    {
        return transformer;
    }

    protected void ToggleScale()
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

    protected virtual void OnEnable()
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

    protected virtual void transformCompletedHandler(object sender, EventArgs e)
    {
        transformer.enabled = false;
        ToggleScale();
    }

    protected virtual void OnTriggerStay2D(Collider2D other)
    {

        if (other.gameObject.name.Equals(this.gameObject.name + "InPlace") && !transformer.enabled)
        {
            ResetTransform();
            if (!other.GetComponentsInChildren<SpriteRenderer>()[0].enabled)
            {
                ProceedButton.Advance();

                foreach (SpriteRenderer spriteRenderer in other.GetComponentsInChildren<SpriteRenderer>())
                {
                    spriteRenderer.enabled = true;
                }
            }
            
        }
    }

}
