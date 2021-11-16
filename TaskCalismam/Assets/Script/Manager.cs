using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


public class Manager : MonoBehaviour
{
    public Camera MainCamera;
    private Transform goTransform;
    private LineRenderer lineRenderer;
    Rigidbody Rb;
    private Ray ray;
    private RaycastHit hit;
    private Vector3 inDirection;
    public int nReflections = 5;
    public int Points;
    private int nPoints;
    public Vector3[] WayPoint;
    public Vector3[] WayPoint›ndex;
    public bool TimeLock,BallMove,block=true;
    int nextPoint = 0;
    bool speedlock;
    public float speed,zaman;
    public int Start;
    public Canvas Panel;
    public GameObject B_Start, B_NLevel, B_TAgain;
    public GameObject GameScript;

    void Awake()
    {
        Rb = GetComponent<Rigidbody>();
        goTransform = this.GetComponent<Transform>();
        lineRenderer = this.GetComponent<LineRenderer>();
        
    }

    public void GameStart()
    {
        Start = 1;
        Panel.enabled = false;
    }
    public void NextLevel()
    {
        Start = 1;
        Panel.enabled = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void TryAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);   
    }

    void Update()
    {
        zaman = zaman*GameScript.GetComponent<Game>().deger;
        if (Start>0)
        {

        LineSetting();
        BallMoveSetting();
        //Debug.Log(speed);
        if(speedlock==true && speed>-0.01f)
        {
            speed -= Time.deltaTime * 0.12f;
        }
        if(speedlock==false)
        {
            speed = 0.12f;
        }
            //Debug.Log(speed);

        }

        if(TimeLock==true)
        {
            zaman += Time.deltaTime;
            Debug.Log(zaman);
        }

        if(zaman>=2.2f)
        {
            TimeLock = false;
            zaman = 0;
            Panel.enabled = true;
            B_Start.SetActive(false);
            B_NLevel.SetActive(false);
            B_TAgain.SetActive(true);
            Start = 0;
        }
    }

    void LineSetting()
    {
        RaycastHit Contact;
        Ray Light = MainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(Light, out Contact))
        {
            if(Contact.point.z<=-2.4f)
            {
                Vector3 MousePosition = new Vector3(Contact.point.x, Contact.point.y + 0.5f, Contact.point.z);
                transform.LookAt(MousePosition);
                if(Input.GetMouseButtonUp(0))
                {
                    BallMove = true;
                    block = false;
                }
            }
            
        }

        if(block==true)
           
        {
            //yans˝ma kontrol¸
            nReflections = Mathf.Clamp(nReflections, 1, nReflections);
            //˝˛˝n olu˛turma
            ray = new Ray(goTransform.position, goTransform.forward);
            //Sadece sahne sekmesinde gˆz¸kecek bir ray olu˛turma 
            Debug.DrawRay(goTransform.position, goTransform.forward * 100, Color.magenta);

            //noktalar˝ yans˝malara e˛itler  
            nPoints = nReflections;
            //line rendererlerin noktalar˝n˝ ayarlar 
            lineRenderer.SetVertexCount(nPoints);
            //ilk line rendererin pozisyonunu ayarlar
            lineRenderer.SetPosition(0, goTransform.position);
            Points = 0;
            //Ray ne kadar yans˝yacak for dˆng¸s¸ ile onun kontrol¸ yap˝l˝r
            for (int i = 0; i <= nReflections; i++)
            {
                //eer ilk yans˝maysa 
                if (i == 0)
                {
                    //˝˛˝n˝n bir ˛eye vurup vurmad˝˝n˝ kontrol et
                    if (Physics.Raycast(ray.origin, ray.direction, out hit, 100))//cast the ray 100 units at the specified direction  
                    {
                        inDirection = Vector3.Reflect(ray.direction, hit.normal);
                        ray = new Ray(hit.point, inDirection);
                        Debug.DrawRay(hit.point, hit.normal * 3, Color.blue);
                        //Sadece sahne sekmesinde gˆz¸kecek bir ray olu˛turma
                        Debug.DrawRay(hit.point, inDirection * 100, Color.magenta);
                        //vurduu objenin ad˝n˝ yazd˝r                
                        //eer 1.kez yans˝rsa
                        if (nReflections == 1)
                        {
                            //linerenderer yeni kˆ˛e ayarla
                            lineRenderer.SetVertexCount(++nPoints);
                        }
                        WayPoint[i] = hit.point;
                        Points = 1;
                        lineRenderer.SetPosition(i + 1, hit.point);
                    }
                }
                else // son yans˝ma
                {
                    //«arp˝˛may˝ kontrol et
                    if (Physics.Raycast(ray.origin, ray.direction, out hit, 10))
                    //˝s˝n olu˛tur 100 birim
                    {
                        //yans˝man˝n yˆn¸n¸ ayarla
                        inDirection = Vector3.Reflect(inDirection, hit.normal);
                        ray = new Ray(hit.point, inDirection);
                        Debug.DrawRay(hit.point, hit.normal * 3, Color.blue);
                        Debug.DrawRay(hit.point, inDirection * 100, Color.magenta);
                        //Áarpt˝˝ nesnenin ad˝n˝ yazd˝r                 
                        //line renderer kˆ˛esini ayarlama 
                        lineRenderer.SetVertexCount(++nPoints);
                        //line renderer tepe konumunu ayarlama 
                        WayPoint[i] = hit.point;
                        Points++;
                        lineRenderer.SetPosition(i + 1, hit.point);
                    }
                }
                WayPoint›ndex = WayPoint;
            }
        }

        
    }
    void BallMoveSetting()
    {
        //point dei˛keni kˆ˛e say˝lar˝n˝ belirler olu˛turulan vektˆr matrislerindeki deerlere gˆre topun s˝rayla ula˛mas˝na salar
        if (BallMove == true)
        {
            
            TimeLock = true;                     
            lineRenderer.enabled = false;
            transform.position = Vector3.MoveTowards(transform.position, WayPoint›ndex[nextPoint], speed);
            speedlock = true;
            if (transform.position == WayPoint›ndex[nextPoint])
            {
                speedlock = false;
                nextPoint++;
                if (nextPoint == Points)
                {
                    BallMove = false;
                    nextPoint = 0;
                    Points = 0;
                }
            }

        }
    }
     
}
