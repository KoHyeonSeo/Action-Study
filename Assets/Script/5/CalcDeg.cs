using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CalcDeg : MonoBehaviour
{
    [SerializeField] private GameObject bar;
    [SerializeField] private GameObject controlBar;
    private (double deg, float angle) CalcDegree()
    {
        //공식 계산
        double a = Vector3.Distance(bar.transform.position, controlBar.transform.position);
        a = Math.Round(a, 2);
        Vector3 barDist = bar.transform.position;
        Vector3 bar2Center = controlBar.transform.GetChild(0).position;
        barDist.x = 0; barDist.z = 0;
        bar2Center.x = 0; bar2Center.z = 0;

        Vector3 controlBarDist = controlBar.transform.position;
        Vector3 controlBar2Center = controlBar.transform.GetChild(0).position;
        controlBarDist.x = 0; controlBarDist.y = 0;
        controlBar2Center.x = 0; controlBar2Center.y = 0;

        double b = Vector3.Distance(controlBar.transform.position, controlBar2Center);
        b = Math.Round(b, 2);
        double c = Vector3.Distance(bar.transform.position, controlBar2Center);
        c = Math.Round(c, 2);

        //코사인 법칙에 의해
        double cosTheta = (Mathf.Pow((float)a, 2) - Mathf.Pow((float)b, 2) - Mathf.Pow((float)c, 2)) / -2 * b * c;
        cosTheta = Math.Round(cosTheta, 2);

        //Theta값 산출
        double theta = Mathf.Acos((float)cosTheta);
        theta = Math.Round(theta, 2);
        if (double.IsNaN(theta))
        {
            theta = 0;
        }

        return (theta * Mathf.Rad2Deg, 0);
    }
    private void Update()
    {
        var (deg, ang) = CalcDegree();
        while (ang < deg)
        {
            controlBar.transform.RotateAround(controlBar.transform.GetChild(0).position,
               new Vector3(1, 0, 0), (float)(Time.deltaTime * deg));
            ang += (float)(Time.deltaTime * deg);
        }
    }
}
