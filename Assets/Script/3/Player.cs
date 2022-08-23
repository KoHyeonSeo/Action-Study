using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject cameras;
    public float speed = 5;
    public float jumpPower = 20;
    //필요속성: 점프 가능 횟수
    public int jumpCount = 2;
    Vector3 trans;
    bool isCameraMove = false;
    private float curTime = 0;
    private float dragChange = 1f;
    private bool isOnSlope = false;
    private bool isOnce = false;
    private void Update()
    {
        //Debug.Log($"velocity: {GetComponent<Rigidbody>().velocity}");
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        //2. 방향이 필요
        // -> 내가 바라보는 방향을 기준으로 가고 싶다.
        Vector3 dir = new Vector3(h, 0, v);
        dir = Camera.main.transform.TransformDirection(dir);
        dir.y = 0;

        transform.position += dir * speed * Time.deltaTime;

        if (Input.GetButtonDown("Jump") && jumpCount > 0)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpPower,ForceMode.Impulse);
            jumpCount--;
            GetComponent<Rigidbody>().drag = 0;
        }
        if (isCameraMove)
        {
            cameras.transform.position = transform.position + Vector3.up;
        }
        if (Input.GetButton("SpeedUp") && isOnSlope && !isOnce)
        {
            GetComponent<Rigidbody>().velocity += Vector3.forward * 5;
            cameras.transform.position = new Vector3(
                cameras.transform.position.x,
                (cameras.transform.position + Vector3.down * 0.5f).y,
                cameras.transform.position.z);
            isOnce = true;
        }
        else if (isOnce && (Input.GetButtonUp("SpeedUp") || !isOnSlope))
        {
            cameras.transform.position = new Vector3(
                cameras.transform.position.x,
                (cameras.transform.position + Vector3.up * 0.5f).y,
                cameras.transform.position.z);
            GetComponent<Rigidbody>().velocity -= Vector3.forward * 5;
            isOnce = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        isCameraMove = false;
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Slope"))
        {
            jumpCount = 2;
            StartCoroutine(CameraMoving());
        }
        else if( collision.collider.gameObject.layer == LayerMask.NameToLayer("Platform"))
        {
            isOnSlope = false;
            jumpCount = 2;
            GetComponent<Rigidbody>().drag = 10f;
            StartCoroutine(ChangeDrag());
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Slope"))
        {
            isOnSlope = true;
            GetComponent<Rigidbody>().drag = 0;
        }
        else if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Platform"))
        {
            isOnSlope = false;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        isCameraMove = true;
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Slope"))
        {
            curTime = 0;
        }
    }
    IEnumerator ChangeDrag()
    {
        while (true)
        {
            curTime += Time.deltaTime;
            if (curTime > dragChange)
            {
                curTime = 0;
                GetComponent<Rigidbody>().drag = 0;
                break;
            }
            yield return null;
        }
    }
    IEnumerator CameraMoving()
    {
        trans = cameras.transform.position + Vector3.down * 0.7f;
        while (true)
        {
            if(Mathf.Abs(cameras.transform.position.y - trans.y) < 0.01f)
            {
                break;
            }
            float y = Mathf.Lerp(cameras.transform.position.y, trans.y, 0.05f);
            cameras.transform.position = new Vector3(cameras.transform.position.x, y, cameras.transform.position.z);
            yield return null;
        }
        yield return null;
    }
}
