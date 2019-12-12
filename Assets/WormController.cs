using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormController : MonoBehaviour
{
    public float movementSpeed;
    public float commandRadius;
    public Transform commandIndicator;

    Vector3 lastRayPoint;
    Vector3 lastMovementPoint;
    private void Update()
    {
        if(Input.GetButton("Fire2"))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, LayerMask.GetMask("ground")) )
            {
                commandIndicator.position = hit.point;
                var colls = Physics.OverlapSphere(hit.point, commandRadius);
                for(int i = 0; i < colls.Length; ++i)
                {
                    var movement = colls[i].GetComponent<MovementAlly>();
                    if(movement)
                    {
                        lastMovementPoint = movement.transform.position;
                        Vector3 diff= hit.point - movement.transform.position;
                        movement.ApplyDir(new Vector2(diff.x, diff.z).normalized*5, 0.5f);
                        Debug.DrawRay(movement.transform.position, diff, Color.green);
                    }
                }
            }

        }
    }
    private void OnDrawGizmos()
    {
    }

    void LateUpdate()
    {
        transform.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized*movementSpeed;

    }
}
