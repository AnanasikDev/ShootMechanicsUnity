using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BultScript : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        transform.SetParent(null);
        Destroy(gameObject, lifeTime);
        //rb.velocity = Vector3.forward * speed;
        rb.AddForce(transform.forward * speed);
    }

    void Update()
    {
        //transform.Translate(Vector3.forward * speed);
        //rb.velocity = Vector3.forward * speed;
    }
    void OnCollisionStay(Collision other)
    {
        Destroy(gameObject);
    }
}