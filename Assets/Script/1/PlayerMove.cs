using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    /*
    public float teleportPosition = 10;
    public GameObject Portal;
    public float speed = 5;
    private bool createPortal = false;
    public bool usePortal { get; set; }
    public Vector3 Dir { get; set; }
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 dir = Vector3.right * h + Vector3.forward * v;
        transform.position += dir * speed * Time.deltaTime;



        Vector3 mousePos = Input.mousePosition;
        mousePos.z = teleportPosition;
        Vector3 screenToWorld = Camera.main.ScreenToWorldPoint(mousePos);
        Dir = screenToWorld - transform.position;
        Debug.DrawRay(transform.position, Dir, Color.red);

        if (Input.GetButtonDown("Fire2") && !createPortal)
        {
            GameObject P = Instantiate(Portal);
            P.transform.position = transform.position;
            createPortal = true;
        }
        if (Input.GetButtonDown("Fire1"))
        {
            usePortal = true;
            createPortal = false;
        }
    }
    */

    [SerializeField] private GameObject orbFactory;
    [SerializeField] private float powerUpTime = 0.5f;
    private float curTime = 0;
    private bool isFire = false;
    GameObject orb;
    public float speed = 5;
    private void Update()
    {

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 dir = Vector3.right * h + Vector3.forward * v;
        transform.position += dir * speed * Time.deltaTime;

        if (Input.GetButton("Fire1") && !isFire)
        {
            isFire = true;
            orb = Instantiate(orbFactory);
            orb.transform.position = transform.position + Vector3.forward * 2;
        }
        else if (isFire && !Input.GetButton("Fire1"))
        {
            orb.GetComponent<Orb>().isGo = true;
            isFire = false;
        }
        else if( isFire && Input.GetButton("Fire1"))
        {
            if (orb.transform.localScale.x < 1)
            {
                orb.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f) * 3 * Time.deltaTime;
                orb.GetComponent<Orb>().Power = orb.transform.localScale.x * 100f;
            }
        }
    }
}
