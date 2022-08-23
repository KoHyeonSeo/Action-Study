using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    public float Power { get; set; }
    public bool isGo { get; set; }
    private void Start()
    {
        isGo = false;
        Power = 0f;
    }
    private void Update()
    {
        if (isGo)
        {
            transform.position += Vector3.forward * speed * Time.deltaTime;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("Enemy"))
        {
            other.GetComponent<EnemyHealth>().Damage(Power);
        }
        Destroy(gameObject);
    }
}
