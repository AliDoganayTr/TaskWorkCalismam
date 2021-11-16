using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    Animator kontrol;
    void Start()
    {
        kontrol = GetComponent<Animator>();
    }

   
    void Update()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag== "Ball")
        {
            kontrol.SetBool("Lock", true);
        }
    }
}
