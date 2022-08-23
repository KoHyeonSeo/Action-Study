using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    float rx;
    float ry;

    // Update is called once per frame
    void Update()
    {
        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");
        rx += my;
        ry += mx;

        rx = Mathf.Clamp(rx, -80, 80);
        transform.eulerAngles = new Vector3(-rx, ry, 0);


    }
}
