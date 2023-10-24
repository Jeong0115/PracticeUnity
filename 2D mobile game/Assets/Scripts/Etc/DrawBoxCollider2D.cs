using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawBoxCollider2D : MonoBehaviour
{
    void Update()
    {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        if (collider != null)
        {
            Vector3 topLeft = collider.bounds.min;
            Vector3 topRight = new Vector3(collider.bounds.max.x, collider.bounds.min.y, collider.bounds.min.z);
            Vector3 bottomRight = collider.bounds.max;
            Vector3 bottomLeft = new Vector3(collider.bounds.min.x, collider.bounds.max.y, collider.bounds.max.z);

            Debug.DrawLine(topLeft, topRight, Color.red);
            Debug.DrawLine(topRight, bottomRight, Color.red);
            Debug.DrawLine(bottomRight, bottomLeft, Color.red);
            Debug.DrawLine(bottomLeft, topLeft, Color.red);
        }
    }
}
