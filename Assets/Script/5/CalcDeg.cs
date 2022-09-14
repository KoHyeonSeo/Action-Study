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
        //���� ���
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

        float b = Vector3.Distance(controlBar.transform.position, controlBar2Center);
        b = (float)Math.Round(b, 2);
        float c = Vector3.Distance(bar.transform.position, controlBar2Center);
        c = (float)Math.Round(c, 2);

        //�ڻ��� ��Ģ�� ����
        float cosTheta = (Mathf.Pow(a, 2) - Mathf.Pow(b, 2) - Mathf.Pow(c, 2)) / -2 * b * c;
        cosTheta = (float)Math.Round(cosTheta, 2);

        //Theta�� ����

        float theta = 1 / Mathf.Cos(cosTheta);
        Debug.Log(Mathf.Cos((float)cosTheta));
        theta = (float)Math.Round(theta, 2);
        if (float.IsNaN(theta))
        {
            theta = 0;
        }

        return (Mathf.Abs(theta * Mathf.Rad2Deg), 0);
    }
    private void Update()
    {
        var (deg, ang) = CalcDegree();
        while (ang < deg)
        {
            controlBar.transform.RotateAround(controlBar.transform.GetChild(0).position,
               new Vector3(1, 0, 0), (float)(Time.deltaTime * deg * 0.1f));
            ang += (float)(Time.deltaTime * deg * 0.1f);
        }

    }
}
