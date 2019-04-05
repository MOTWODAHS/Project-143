using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClowdSpawner : MonoBehaviour
{
    public float speed;
    private Vector2 temp;
    public float range;
    void Update()
    {
        temp = transform.position;
        temp.x += speed;
        transform.position = temp;
        if(transform.position.x > range)
        {
            temp.x = -range;
            transform.position = temp;
        }
    }
}
