using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{

    public Canvas Panel;
    public GameObject B_Start, B_NLevel, B_TAgain;
    public float deger=1;



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Ball")
        {
            deger = 0;
            Debug.Log("Gool");
            Panel.enabled = true;
            B_Start.SetActive(false);
            B_NLevel.SetActive(true);
            B_TAgain.SetActive(false);           
        }
    }
}
