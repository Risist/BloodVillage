using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttentionPath : MonoBehaviour
{
    static public AttentionPath instance;
    private void Start()
    {
        instance = this;
    }
    public List<Transform> markers;
    public float attentionDistance = 5.0f;
    
    public Vector3 GetDirectionTo(Vector3 position, int id)
    {
        return markers[id].position - position;
    }
    public Vector3 GetBestDirection(Vector3 position)
    {
        for(int i = 0; i < markers.Count; ++i)
        {
            var dir = GetDirectionTo(position, i);
            if (dir.sqrMagnitude < attentionDistance * attentionDistance)
                return dir;
        }

        return GetDirectionTo(position, 0);
    }
    public int GetBestId(Vector3 position)
    {
        for (int i = 0; i < markers.Count; ++i)
        {
            var dir = GetDirectionTo(position, i);
            if (dir.sqrMagnitude < attentionDistance * attentionDistance)
                return i;
        }

        return -1;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;

        for (int i = 0; i < markers.Count; ++i)
        {
            float factor = (float)i / (markers.Count-1);
            Gizmos.color = Color.Lerp(Color.green, Color.red, factor);
            Gizmos.DrawSphere(markers[i].position, 0.5f);
            Gizmos.DrawWireSphere(markers[i].position, attentionDistance);
            
        }
    }
}
