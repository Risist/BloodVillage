using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CellMovement : MonoBehaviour
{
    public float movementSpeed;
    Rigidbody rb;
    Quaternion initialRotation;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialRotation = rb.rotation;
        Destroy(gameObject, 10);
    }

    void FixedUpdate()
    {
        if (rb.velocity.sqrMagnitude < 0.25f * 0.25f)
        {
            rb.AddForce(transform.forward * movementSpeed);
        }
        else
        {
            rb.AddForce(rb.velocity.normalized * movementSpeed);
            rb.rotation = initialRotation * Quaternion.LookRotation(rb.velocity, Vector3.up);
        }

        Debug.DrawRay(transform.position, rb.velocity*0.5f, Color.red);
    }

    private void OnCollisionStay(Collision collision)
    {
        Vector3 normalSum = Vector3.zero;
        for (int i = 0; i < collision.contactCount; ++i)
            normalSum += collision.GetContact(i).normal;

        rb.AddForce(normalSum/collision.contactCount* 20);
    }
}
