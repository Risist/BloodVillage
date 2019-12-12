using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatController : MonoBehaviour
{
    public int leftFat;
    private void OnCollisionEnter(Collision collision)
    {
        MovementAlly movement = collision.gameObject.GetComponent<MovementAlly>();
        if(movement && leftFat > 0 && movement.GainResource())
        {
            --leftFat;
        }

        if(leftFat == 0)
        {
            var renderers = GetComponentsInChildren<Renderer>();
            foreach(var it in renderers)
            {
                var rb = it.gameObject.AddComponent<Rigidbody>();
                rb.useGravity = true;
                rb.transform.parent = null;
                rb.AddExplosionForce(400, transform.position, 5);

                Destroy(gameObject,3);
            }

            Destroy(gameObject);
        }
    }
}
