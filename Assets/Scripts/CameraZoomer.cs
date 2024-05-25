using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomer : MonoBehaviour
{
    private Camera cam;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.mouseScrollDelta.y > 0)
        {
            cam.orthographicSize -= 1;
        }
        else if (Input.mouseScrollDelta.y < 0)
        {
            cam.orthographicSize += 1;
        }
    }
}
