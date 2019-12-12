using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject prefab;
    public float cd;
    public float randomCd;
    public float cdWaveScale;

    int waveId = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnCell());
    }

    IEnumerator SpawnCell()
    {
        while (true)
        {
            ++waveId;
            var obj = Instantiate(prefab, transform.position, transform.rotation);
            float val = Random.value;
            if (val > 0.9)
                obj.GetComponentInChildren<IntestinesSpawner>().qualitySpawn = 2;
            else if (val > 0.65)
                obj.GetComponentInChildren<IntestinesSpawner>().qualitySpawn = 1;
            else
                obj.GetComponentInChildren<IntestinesSpawner>().qualitySpawn = 0;

            var wait = new WaitForSeconds(cd + Random.value * randomCd + waveId*cdWaveScale);
            yield return wait;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.5f);
        Gizmos.DrawSphere(transform.position + transform.forward * 0.25f, 0.25f);
        Gizmos.DrawSphere(transform.position + transform.forward * 0.5f, 0.25f);
        Gizmos.DrawSphere(transform.position + transform.forward * 0.75f, 0.25f);
    }
}
