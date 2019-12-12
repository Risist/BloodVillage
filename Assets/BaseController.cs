using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseController : Damagable
{
    public int currentResource;
    [Header("Worm")]
    public int[] wormCost;
    public GameObject[] wormPrefab;
    [Header("Resources")]
    public Text resourceText;

    private void OnCollisionStay(Collision collision)
    {
        MovementAlly movememt = collision.gameObject.GetComponent<MovementAlly>();
        if(movememt && movememt.GiveResource())
        {
            ++currentResource;
            UpdateText();
        }
    }
    public void SpawnWorm(int i)
    {
        if (currentResource >= wormCost[i])
        {
            var root = transform;
            Vector2 offset = Random.insideUnitCircle * (1.5f + Random.value * 1.5f);
            var obj = Instantiate(wormPrefab[i], root.position + new Vector3(offset.x, wormPrefab[i].transform.position.y, offset.y), Random.rotation);
            obj.GetComponentInChildren<IntestinesSpawner>().qualitySpawn = i;
            obj.GetComponent<MovementAlly>().myBase = gameObject;

            currentResource -= wormCost[i];
            UpdateText();
        }
    }

    void UpdateText()
    {
        resourceText.text = currentResource.ToString();
    }
}
