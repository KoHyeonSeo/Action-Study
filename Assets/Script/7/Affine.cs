using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Affine : MonoBehaviour
{
    public float angle = 90f;
    
    [System.Serializable]
    public struct ChooseAxis
    {
        public bool x;
        public bool y;
        public bool z;
    }

    public ChooseAxis axis;

    private void Update()
    {
        //뭐가 되었든 중심점 회전 
        if (axis.x == true)
        {
            Vector3 pos = transform.localPosition;
            //x축 중심 회전 이동
            Vector3 rot = new Vector3(pos.x,
                pos.y * Mathf.Cos(angle) - pos.z * Mathf.Sin(angle),
                pos.y * Mathf.Sin(angle) + pos.z * Mathf.Cos(angle));
            //transform.LookAt(rot);
            transform.localPosition = rot;
            print(rot);
        }
        if(axis.y == true)
        {
            Vector3 pos = transform.localPosition;
            //y축 중심 회전 이동
            Vector3 rot = new Vector3(pos.z * Mathf.Sin(angle) + pos.x * Mathf.Cos(angle),
                pos.y,
                pos.z * Mathf.Cos(angle) - pos.x * Mathf.Sin(angle));
            //transform.LookAt(rot);
            transform.localPosition = rot;
            print(rot);
        }
        if (axis.z == true)
        {
            Vector3 pos = transform.localPosition;
            //z축에서 회전 이동
            Vector3 rot = new Vector3(pos.x * Mathf.Cos(angle) - pos.y * Mathf.Sin(angle),
                pos.x * Mathf.Sin(angle) + pos.z * Mathf.Cos(angle),
                pos.z);
            //transform.LookAt(rot);
            transform.localPosition = rot;
            print(rot);
        }
    }
}
