using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Transition : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private LineRenderer lineRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void TransitionOut()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.DOFade(0f, 1f);
        }

        if (GetComponent<Collider2D>() != null)
        {
            GetComponent<Collider2D>().enabled = false;
        }
    }

    public void TransitionIn()
    {
        spriteRenderer.DOFade(1.0f, 1f);
        if (GetComponent<Collider2D>() != null)
        {
            GetComponent<Collider2D>().enabled = true;
        }
    }

    public IEnumerator lineRendererFadeOut()
    {
        float startTime = Time.time;
        while (Time.time - startTime < 1f)
        {
            lineRenderer.material.SetFloat("_Transparency", 1f - (Time.time - startTime) / 1f);
            yield return null;
        }
        lineRenderer.material.SetFloat("_Transparency", 0f);

        if (GetComponent<Collider2D>() != null)
        {
            GetComponent<Collider2D>().enabled = true;
        }
    }

    public IEnumerator lineRendererFadeIn()
    {
        float startTime = Time.time;
        while (Time.time - startTime < 1f)
        {
            lineRenderer.material.SetFloat("_Transparency", (Time.time - startTime) / 1f);
            yield return null;
        }
        lineRenderer.material.SetFloat("_Transparency", 1f);

        if (GetComponent<Collider2D>() != null)
        {
            GetComponent<Collider2D>().enabled = true;
        }
    }

    public void hide()
    {
        lineRenderer.material.SetFloat("_Transparency", 0f);

        if (GetComponent<Collider2D>() != null)
        {
            GetComponent<Collider2D>().enabled = true;
        }
    }
}

