using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour
{
    public int fractionId;

    public virtual void Die()
    {
        Debug.Log(":D");
        var rb = GetComponent<Rigidbody>();
        var collider = GetComponent<Collider>();

        collider.enabled = false;
        rb.useGravity = true;
        rb.constraints = RigidbodyConstraints.None;
        rb.drag = 0.0f;
        rb.mass = 50.0f;

        Destroy(gameObject, 3);
        Destroy(this);
    }
}
