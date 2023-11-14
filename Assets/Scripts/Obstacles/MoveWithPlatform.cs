using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithPlatform : MonoBehaviour
{

    void OnCollisionEnter(Collision collision)
    {
        Rigidbody otherRigidbody = collision.collider.GetComponent<Rigidbody>();

        if (otherRigidbody != null)
        {
            // Parent the other Rigidbody to the moving platform
            otherRigidbody.transform.parent = transform;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        Rigidbody otherRigidbody = collision.collider.GetComponent<Rigidbody>();

        if (otherRigidbody != null)
        {
            // Unparent the other Rigidbody from the moving platform
            otherRigidbody.transform.parent = null;
        }
    }

}
