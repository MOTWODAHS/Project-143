using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorWindowPiece : PuzzlePiece
{
    private Vector3 position;
    private Bounds thisBound;

    private static Bounds bound;
    private static List<PuzzlePiece> pieces = new List<PuzzlePiece>();
    private const int MAX_CAP = 5;

    protected override void Start()
    {
        base.Start();
        position = GetComponent<Transform>().position;
        thisBound = GetComponent<BoxCollider2D>().bounds;
    }

    private void ResetTransform()
    {
        transform.position = position;
    }

    private int PlacementType() // 1: inwall; 0: intersect; -1: mutual-exclusive
    {
        

        bool inWall = bound.Contains(new Vector2(thisBound.min.x, thisBound.min.y)) && 
            bound.Contains(new Vector2(thisBound.max.x, thisBound.max.y));
        bool intersect = bound.Intersects(thisBound);

        if (inWall && intersect)
        {
            return 1;
        } else if (!inWall && intersect)
        {
            return 0;
        } else
        {
            return -1;
        }
    }

    private bool Overlap()
    {
        foreach(PuzzlePiece piece in pieces)
        {
            if (!piece.Equals(this) && piece.GetComponent<BoxCollider2D>().bounds.Intersects(thisBound))
            {
                return true;
            }
        }
        return false;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        bound = GameObject.FindGameObjectWithTag("wall").GetComponent<PolygonCollider2D>().bounds;
        bound.Encapsulate(GameObject.FindGameObjectWithTag("roof").GetComponent<PolygonCollider2D>().bounds);
    }

    protected override void transformCompletedHandler(object sender, EventArgs e)
    {
        transformer.enabled = false;
        base.ToggleScale();
        thisBound = GetComponent<BoxCollider2D>().bounds;

        int placementType = PlacementType();
        bool overlap = Overlap();
       
        if (overlap)
        {
            ResetTransform();
        }
        if (placementType == 1 && pieces.Count + 1 > MAX_CAP && !pieces.Contains(this))
        {
            ResetTransform();
        }
        else if (placementType == 0)
        {
            if (pieces.Contains(this))
            {
                pieces.Remove(this);
            }
            ResetTransform();
        }
        if (placementType == -1 && pieces.Contains(this) && !overlap)
        {
            pieces.Remove(this);
        }
        else if (placementType == 1 && pieces.Count + 1 <= MAX_CAP && !pieces.Contains(this) && !overlap)
        {
            pieces.Add(this);
        }

        if (pieces.Count > 0)
        {
            ProceedButton.Advance();
        } else
        {
            ProceedButton.HideAdvance();
        }

    }

    protected override void OnTriggerStay2D(Collider2D other)
    {
        return; 
    }

}
