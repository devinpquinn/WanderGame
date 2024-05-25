using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomer : MonoBehaviour
{
    private Camera cam;

    private float increment = 0.5f;

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

        if (Input.GetKeyDown(KeyCode.R))
        {
            cam.transform.position = new Vector3(0, 0, -10);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y + (increment * (cam.orthographicSize / 5)), -10);
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y - (increment * (cam.orthographicSize / 5)), -10);
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            cam.transform.position = new Vector3(cam.transform.position.x - (increment * (cam.orthographicSize / 5)), cam.transform.position.y, -10);
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            cam.transform.position = new Vector3(cam.transform.position.x + (increment * (cam.orthographicSize / 5)), cam.transform.position.y, -10);
        }
    }
}
