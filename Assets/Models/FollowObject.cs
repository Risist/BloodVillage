using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FollowObject : MonoBehaviour
{
    public Transform aim;
    public float angleOffset = 0;
    public float distance;
    public float lerpFactor = 1.0f;
    public float requiredAngleDiff;


    // Start is called before the first frame update
    void Start()
    {
        rotation = transform.rotation.y;
    }
    float rotation;

    // Update is called once per frame
    void Update()
    {
        if (!aim)
        {
            Destroy(gameObject);
            GetComponent<FractionDamage>().enabled = false;
            return;
        }
        Vector3 toAim = transform.position - aim.position;
        float targetAngle = aim.eulerAngles.y + angleOffset; 
        
        transform.position = aim.position + Quaternion.Euler(0, Mathf.LerpAngle(rotation, targetAngle, lerpFactor), 0) * Vector3.forward * distance;

        float angleToAim = Vector2.SignedAngle(new Vector2(toAim.x, toAim.z), Vector2.up);
        float angleDiff = Mathf.DeltaAngle(rotation, targetAngle);

        
        if (!(angleDiff > -requiredAngleDiff && angleDiff < requiredAngleDiff))
        {
            rotation = Mathf.LerpAngle(rotation, angleToAim, lerpFactor);
            transform.eulerAngles = new Vector3(0, rotation, 0);
        }
    }
}
