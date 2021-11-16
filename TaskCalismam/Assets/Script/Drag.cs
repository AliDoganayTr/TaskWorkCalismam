using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{
    public GameObject location1, location2;
    public int Rotate=0;
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
    private void OnMouseDrag()
    {
        if (Rotate==1)
        {
            transform.position = Vector3.MoveTowards(transform.position, location2.transform.position, 0.01f);
        }
        if (Rotate==2)
        {
            transform.position = Vector3.MoveTowards(transform.position, location1.transform.position, 0.01f);
        }
    }

    private void OnMouseDown()
    {
        if(Rotate==2)
        { Rotate = 0; }
        Rotate++;
    }

}
