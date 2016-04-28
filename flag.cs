using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Networking;


public class flag : NetworkBehaviour
{
    [SyncVar]
    bool flaginb;
    

    [SyncVar]
    bool OnController;
    [SyncVar]
    bool spawntrue;
    [SyncVar]
    float timer = 10f;
    [SyncVar]
    Vector3 postion;
    [SyncVar]
    public  GameObject flagz;
    [SyncVar]
    float cont = 0f;
    [SyncVar]
    bool drop;
    Base Base2;
    bool spaz;
    public GameObject o1;
    public GameObject o0;
    public GameObject o2;
    public GameObject o3;
    public GameObject o4;
    public GameObject o5;
    int count;
    // Use this for initialization

    void Start()
    {

    }
    
    // Update is called once per frame
    void Update()
    {  if (isServer)
        {
        if (drop == true)
        {
            cont += .5f;
            if (cont == 5f)
            {
                    spawnfl spawn = new spawnfl();
                    spawn.spawn();
                Destroy(this);
                
                drop = false;
            }
        }
      
            if (OnController == true)
            { 
                this.transform.localPosition = new Vector3(0, 1, 0);
                // networktransform
            }
        }
    }


   


    public  bool drop2()
    {
        drop = true;
        OnController = false;
        

        return drop;
    }
    //   void onBase()
    // {

    // }
    public void OnTriggerEnter(Collider c)
    {
        Debug.Log("piss");
        if (isServer)
        {
            Debug.Log("it hit the ball on the sevver");

            if (c.CompareTag("bB"))
            {
                if (OnController == false) { 
                Vector3 pla = new Vector3(0f, 4f, 0f);
                Controller pla2 = c.gameObject.GetComponent<Controller>();
                //Vector3 tyk = pla2.transform.position + pla;

   
                
                
                
                
               
                //flagz.transform.parent = pla2.p2.transform;
                //flagz.transform.position = tyk;
                flagz.transform.parent = pla2.p2.transform;
                flagz.transform.localPosition = Vector3.zero;
                OnController = true;
                }
               
            }
            else if (c.gameObject.tag == "base")
            {
                flaginb = true;
                Base pla2 = c.gameObject.GetComponent<Base>();
                flagz.transform.parent = Base2.B2.transform;

                Base2.inBase();
            }
        }

        //on colistion check to see if Controller if Controller set on Controller to true then make flag a subcatagrio of the Controller
    }
}