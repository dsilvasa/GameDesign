using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Networking;


public class flag : NetworkBehaviour {
    [SyncVar]
    bool flaginb;
    [SyncVar]
    GameObject flag2;
    [SyncVar]
    bool OnPlayer;
    [SyncVar]
    bool spawntrue;
    [SyncVar]
    float timer = 10f;
    [SyncVar]
    Vector3 postion;
    [SyncVar]
    GameObject flagz;
    [SyncVar]
    Player Player;
    [SyncVar]
    float cont = 0f;
    [SyncVar]
    bool drop;
    [SyncVar]
    Base Base2;

    // Use this for initialization
    void Start()
    {

        if (isServer)
        {
            spawn();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (drop == true)
        {
            cont += .5f;
            if (cont == 5f)
            {
                //Destroy(this);
                this.spawn();
                drop = false;
            }
        }
    }
    void spawn()
    {
        UnityEngine.Random ran = new UnityEngine.Random();
        int red = UnityEngine.Random.Range(0, 4);
        if (red == 0)
        {
            postion = new Vector3(56087f,-1.04f);
            Instantiate(flagz, postion, Quaternion.identity);
        }
        else if (red == 1)
        {
            postion = new Vector3(260f,-0.99f);
            Instantiate(flagz, postion, Quaternion.identity);

        }
        else if (red == 2)
        {
            postion = new Vector3(497.69f,-0.81f);
            Instantiate(flagz, postion, Quaternion.identity);
        }
        else if (red == 3)
        {
            postion = new Vector3(447.52f,-1.2f);
            Instantiate(flagz, postion, Quaternion.identity);
        }
        else if (red == 4)
        {
            postion = new Vector3(89.038f,-.88f);
            Instantiate(flagz, postion, Quaternion.identity);
        }
      


    }




    bool drop2( bool Onplayer, bool drop)
    {
        drop = true;
        OnPlayer = false;
        return drop;
    }
    void onBase()
    {

    }
    void OnCollisonEnter(Collision c)
    {
        if (isServer)
        {
            if (c.gameObject.tag == "player")
            {
                Player pla2 = c.gameObject.GetComponent<Player>();
                flag2.transform.parent = pla2.P2.transform;
                OnPlayer = true;
            }
            else if (c.gameObject.tag == "base")
            {
                flaginb = true;
                Base pla2 = c.gameObject.GetComponent<Base>();
                flag2.transform.parent = Base2.B2.transform;
                Base2.inBase();
            }
        }
    
        //on colistion check to see if player if player set on player to true then make flag a subcatagrio of the player
    }
}