using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public float damping = 1;
    public Vector2 offset = new Vector2(2, 1);

    private Vector3 velocity = Vector3.zero;

    private void Update()
    {
        Vector3 desiredPosition = target.position + new Vector3(offset.x, offset.y, 0);
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, damping);
        transform.position = smoothedPosition;
    }
}
