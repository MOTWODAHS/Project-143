using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerlineFixLocation : MonoBehaviour
{
    private const int x_limit = 15;
    private const float y_limit = 4f;
    void FixedUpdate()
    {
        if(transform.position.x < -x_limit )
        {
            transform.position = new Vector3(-x_limit, transform.position.y, transform.position.z);
        }

        if(transform.position.x > x_limit)
        {
            transform.position = new Vector3(x_limit, transform.position.y, transform.position.z);
        }

        if (transform.position.y < -y_limit)
        {
            transform.position = new Vector3(transform.position.x, -y_limit, transform.position.z);
        }

        if (transform.position.y > (y_limit - 2.5f))
        {
            transform.position = new Vector3(transform.position.x, y_limit - 2.5f, transform.position.z);
        }

    }
}
