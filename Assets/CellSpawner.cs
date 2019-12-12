using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellSpawner : MonoBehaviour
{
    public GameObject prefab;
    public float randomRotation = 5.0f;
    public float positionOffset = 0.5f;
    public float cd;
    public float randomCd;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnCell()); 
    }

    IEnumerator SpawnCell()
    {
        while(true)
        {
            var wait = new WaitForSeconds(cd + Random.value*randomCd);
            Quaternion rot = transform.rotation * Quaternion.Euler(0, Random.Range(-randomRotation, randomRotation),0);
            Vector3 pos = transform.position + Random.insideUnitSphere*positionOffset;
            Instantiate(prefab, pos, rot);
            yield return wait;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 0.5f);
        Gizmos.DrawSphere(transform.position + transform.forward*0.25f, 0.25f);
        Gizmos.DrawSphere(transform.position + transform.forward * 0.5f, 0.25f);
        Gizmos.DrawSphere(transform.position + transform.forward * 0.75f, 0.25f);
    }
}
