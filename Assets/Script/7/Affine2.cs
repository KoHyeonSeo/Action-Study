using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Affine2 : MonoBehaviour
{
    public Transform P1;
    public Transform P2;
    public Transform P3;

    public Transform P;

    [Range(0f,1f)]
    public float a;

    [Range(0f, 1f)]
    public float b;

    Material material;
    private void Start()
    {
        material = P.GetComponent<MeshRenderer>().material;
    }
    private void Update()
    {
        //P = aP1 + bP2 + (1 - a - b)P3
        P.position = a * P1.position + b * P2.position + (1 - a - b) * P3.position;

        material.color = new Vector4(a, b, (1 - a - b), 1);
    }
}
