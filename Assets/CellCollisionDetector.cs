using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellCollisionDetector : MonoBehaviour
{
    public float collisionForce;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
    }

    private void OnTriggerStay(Collider other)
    {
        /*Debug.Log("Collision");
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward,out hit) )
        {
            float angle = Vector3.SignedAngle(hit.normal, Vector3.forward, Vector3.up);
            rb.rotation = Quaternion.Euler(0,angle,0);
            rb.AddForce(rb.rotation*Vector3.forward*collisionForce);

            Debug.Log("RayHiy : angle = " + angle);
        }*/
    }
}
