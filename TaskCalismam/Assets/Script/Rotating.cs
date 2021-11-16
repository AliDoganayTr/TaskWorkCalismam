using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotating : MonoBehaviour
{
    public bool RotateLock;
    Animator kontrol;

    void Start()
    {
        kontrol = GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Ball")
        {
            kontrol.SetBool("Lock", true);
        }
    }
    void Update()
    {
        if (RotateLock==true) //Mouse Sol tuþuna basýnca cisim dönüyor.
        {
            transform.Rotate(0, 0, 0.3f);
        }
    }

    private void OnMouseDown()
    {
        RotateLock = true;
    }
    private void OnMouseUp()
    {
        RotateLock = false;
    }

}
