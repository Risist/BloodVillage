using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : Damagable
{

    public float movementSpeed;
    public float lerpVal;
    public Timer timerChangeDir;
    public float lerpDir;
    public float minDirChange;
    public float maxDirChange;

    Rigidbody rb;

    [System.NonSerialized] public float attracted = 0;

    protected void Start()
    {
        rb = GetComponent<Rigidbody>();
        lastInput = Vector3.forward;

        lastDir = Random.insideUnitCircle;
        movementTime = Random.value * 50;

    }
    Vector3 lastInput;
    float movementTime = 0;

    protected Vector2 lastDir;

    public void ApplyDir(Vector2 dir, float attractionModif = 0)
    {
        lastDir = Vector2.Lerp(lastDir, dir, lerpDir);
        attracted += attractionModif;
    }

    protected void FixedUpdate()
    {
        if(timerChangeDir.IsReadyRestart())
        {
            ApplyDir(Random.insideUnitCircle);
            timerChangeDir.cd = Random.Range(minDirChange, maxDirChange);
        }

        MoveToDir(new Vector3(lastDir.x, 0 , lastDir.y));

        attracted *= 0.9f;
    }

    protected void MoveToDir(Vector3 dir)
    {
        dir.y = 0;
        if (dir.sqrMagnitude > 0.125f * 0.125f)
        {
            lastInput = dir;
            movementTime += Time.fixedDeltaTime * 5.0f;

            float sin = Mathf.Sin(movementTime);
            rb.AddForce(dir.normalized * movementSpeed * sin * sin);

        }
        float cos = Mathf.Cos(movementTime);
        float sign = Mathf.Sign(cos);
        float angle = Quaternion.LookRotation(lastInput, Vector3.up).eulerAngles.y + 180 + sign * cos * cos * 100;
        transform.eulerAngles = new Vector3(0, Mathf.LerpAngle(rb.rotation.eulerAngles.y, angle, lerpVal), 0);


    }
}
