using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAlly : Movement
{
    [Header("References")]
    public GameObject attractionIndicator;
    public GameObject[] resourceIndicator;
    public GameObject myBase;

    int currentResource = 0;

    public bool GainResource()
    {
        if (currentResource < resourceIndicator.Length-1)
        {
            ++currentResource;
            foreach (var it in resourceIndicator)
                it.SetActive(false);
            resourceIndicator[currentResource].SetActive(true);
            return true;
        }
        return false;
    }
    public bool GiveResource()
    {
        foreach (var it in resourceIndicator)
            it.SetActive(false);
        if (currentResource > 0)
        {
            --currentResource;
            if (currentResource > 0);
                resourceIndicator[currentResource].SetActive(true);

            return true;
        }
        return false;
    }

    new void Start()
    {
        base.Start(); 
        foreach (var it in resourceIndicator)
            it.SetActive(false);
    }

    new private void FixedUpdate()
    {
        /*if(currentResource > (resourceIndicator.Length - 1) *0.5f)
        {
            Vector3 dir = (myBase.transform.position - transform.position).normalized * 1.0f;
            ApplyDir(new Vector2(dir.x, dir.z), 0 ); 
        }else*/
        if (timerChangeDir.IsReadyRestart())
        {
            ApplyDir(Random.insideUnitCircle);
            timerChangeDir.cd = Random.Range(minDirChange, maxDirChange);
        }

        MoveToDir(new Vector3(lastDir.x, 0, lastDir.y));
        
        attractionIndicator.SetActive(attracted > 1.0f);
        attracted *= 0.9f;
    }
}
