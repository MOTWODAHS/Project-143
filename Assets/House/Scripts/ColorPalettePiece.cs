﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ColorPalettePiece : PuzzlePiece
{
    private Vector3 position;
    private Bounds thisBound;
    public Color color;

    protected override void Start()
    {
        base.Start();
        position = GetComponent<Transform>().position;
        thisBound = GetComponent<CircleCollider2D>().bounds;
    }

    private void ResetTransform()
    {
        transform.position = position;
    }

    protected override void transformCompletedHandler(object sender, EventArgs e)
    {
        transformer.enabled = false;
        base.ToggleScale();
        GetComponent<CircleCollider2D>().enabled = false;
        
        GameObject.FindGameObjectWithTag("roof").GetComponent<PolygonCollider2D>().enabled = false;
        GameObject.FindGameObjectWithTag("wall").GetComponent<PolygonCollider2D>().enabled = false;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero,0);
        if (hit.collider != null && hit.collider.GetComponent<FillInColor>() != null)
        {
            Debug.Log(hit.collider.gameObject.name);
            hit.collider.GetComponent<FillInColor>().FillColor(color);
        }
        else
        {
            GameObject.FindGameObjectWithTag("roof").GetComponent<PolygonCollider2D>().enabled = true;
            GameObject.FindGameObjectWithTag("wall").GetComponent<PolygonCollider2D>().enabled = true;

            RaycastHit2D secondhit = Physics2D.Raycast(transform.position, Vector2.zero, 9);

            if (secondhit.collider != null && secondhit.collider.GetComponent<FillInColor>() != null)
            {
                Debug.Log(secondhit.collider.gameObject.name);
                secondhit.collider.GetComponent<FillInColor>().FillColor(color);
            }
        }
        GameObject.FindGameObjectWithTag("roof").GetComponent<PolygonCollider2D>().enabled = true;
        GameObject.FindGameObjectWithTag("wall").GetComponent<PolygonCollider2D>().enabled = true;
        GetComponent<CircleCollider2D>().enabled = true;

        ResetTransform();
    }
}
