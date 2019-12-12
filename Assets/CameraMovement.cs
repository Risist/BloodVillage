using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("What To Follow")]
    public List<Transform> followTargets;

    [Header("Follow params")]
    public Vector3 desiredOffset;
    [Range(0.0f, 1.0f), Tooltip("How fast will camera move to middle point")]
    public float followRatio = 1.0f;

    Vector3 GetMiddlePoint()
    {
        Vector3 sum = Vector3.zero;
        bool any = false;
        foreach (var it in followTargets)
            if (it)
            {
                sum += it.position;
                any = true;
            }
        if (!any)
            return transform.position - desiredOffset;

        return sum / followTargets.Count;
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, GetMiddlePoint() + desiredOffset, followRatio);
    }
}
