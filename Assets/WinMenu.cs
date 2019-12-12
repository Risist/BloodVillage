using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinMenu : MonoBehaviour
{
    public GameObject objActivate;

    private void OnDestroy()
    {
        if(objActivate)
            objActivate.SetActive(true);
    }


}
