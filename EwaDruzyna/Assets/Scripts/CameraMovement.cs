using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public float damping = 1;
    public Vector2 offset = new Vector2(2, 1);

    public Vector3 velocity = Vector3.zero;
    public Vector2 boxExtent;
    Vector2 center = Vector2.zero;
  
    private void Start()
    {
        center = target.position;
    }
    private bool isInBoundingBox()
    {
        //print((center.x - boxExtent.x) < transform.position.x);
        if (center.x + boxExtent.x > transform.position.x && center.x - boxExtent.x < transform.position.x && center.y + boxExtent.y > transform.position.y && center.y - boxExtent.y < transform.position.y)
        {
            return true;
        }
        return false;

    }
    private void Update()
    {
        center = target.position;
        Vector3 desiredPosition = target.position + new Vector3(offset.x, offset.y, -10);

        if (isInBoundingBox())
        {
        }
        else
        {
            Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, damping);
            transform.position = smoothedPosition;
        }
    }
}
