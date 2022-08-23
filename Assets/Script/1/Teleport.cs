using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public float teleportPosition = 10;
    private PlayerMove playerMove;
    private void Start()
    {
        playerMove = GetComponent<PlayerMove>();
    }
    private void Update()
    {
        //Vector3 mousePos = Input.mousePosition;
        //mousePos.z = teleportPosition;
        //Vector3 screenToWorld = Camera.main.ScreenToWorldPoint(mousePos);
        //playerMove.Dir = screenToWorld - transform.position;
        //Debug.DrawRay(transform.position, playerMove.Dir, Color.red);
        //if (Input.GetButtonDown("Fire2"))
        //{
        //    transform.position = screenToWorld;
        //}
    }
}
