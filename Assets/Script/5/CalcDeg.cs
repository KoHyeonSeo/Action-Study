using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CalcDeg : MonoBehaviour
{
    [SerializeField] private GameObject bar;
    [SerializeField] private GameObject controlBar;
    private float degree = 0;
    private float angle = 0;
    private void Start()
    {
        var (deg, ang) = CalcDegree();
        degree = deg;
        angle = ang;
    }
    private (float deg, float angle) CalcDegree()
    {
        //공식 계산
        float a = Vector3.Distance(bar.transform.position, controlBar.transform.position);
        a = (float)Math.Round(a, 2);
        Vector3 barDist = bar.transform.position;
        Vector3 bar2Center = controlBar.transform.GetChild(0).position;
        barDist.x = 0; barDist.z = 0;
        bar2Center.x = 0; bar2Center.z = 0;

        Vector3 controlBarDist = controlBar.transform.position;
        Vector3 controlBar2Center = controlBar.transform.GetChild(0).position;
        controlBarDist.x = 0; controlBarDist.y = 0;
        controlBar2Center.x = 0; controlBar2Center.y = 0;

        float b = Vector3.Distance(controlBarDist, controlBar2Center);
        b = (float)Math.Round(b, 2);
        float c = Vector3.Distance(barDist, bar2Center);
        c = (float)Math.Round(c, 2);

        //코사인 법칙에 의해
        float cosTheta = (Mathf.Pow(a, 2) - Mathf.Pow(b, 2) - Mathf.Pow(c, 2)) / -2 * b * c;
        cosTheta = (float)Math.Round(cosTheta, 2);

        //Theta값 산출
        float theta = 1 / Mathf.Cos(cosTheta);
        float kkk = (Mathf.Cos((float)cosTheta));
        theta = (float)Math.Round(theta, 2);
        if (float.IsNaN(theta))
        {
            theta = 0;
        }
        return (theta * Mathf.Rad2Deg, 0);
    }
    private void Update()
    {
        if (angle < degree)
        {
            controlBar.transform.RotateAround(controlBar.transform.GetChild(0).position,
                new Vector3(1, 0, 0), (Time.deltaTime * degree * 0.1f));
            angle += (Time.deltaTime * degree * 0.1f);
        }
        else
        {
            var (deg, ang) = CalcDegree();
            degree = deg;
            angle = ang;
        }
    }
}
