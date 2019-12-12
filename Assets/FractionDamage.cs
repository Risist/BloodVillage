using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FractionDamage : MonoBehaviour
{
    public int fractionId;


    private void OnTriggerStay(Collider other)
    {
        var movement = other.GetComponent<Damagable>();
        if(movement )
        {
            if(fractionId != movement.fractionId)
                movement.Die();
        }
    }
    private void OnCollisionStay(Collision other)
    {
        var movement = other.gameObject.GetComponent<Damagable>();
        if (movement)
        {
            if (fractionId != movement.fractionId)
                movement.Die();
        }
    }
}
