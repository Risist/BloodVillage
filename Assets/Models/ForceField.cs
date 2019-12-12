using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceField : MonoBehaviour
{
    public float force;
    private void OnTriggerStay(Collider other)
    {
        if(other.attachedRigidbody)
        {
            other.attachedRigidbody.AddForce(transform.forward*force);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, 0.5f);

        Gizmos.DrawRay(transform.position, transform.forward * force);
    }
}
