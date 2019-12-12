using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    public GameObject next;
    public Transform cameraLocation;
    public float lerpFactor;
    private void Start()
    {
        Time.timeScale = 0;
    }
    public void StartGame()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
        if (next)
            next.SetActive(true);
    }

    private void LateUpdate()
    {
        if (!transform)
            return;
        var cam = Camera.main.transform;
        cam.position = Vector3.Lerp(cam.position, cameraLocation.position, lerpFactor);
    }
}
