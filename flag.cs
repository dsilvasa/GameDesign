using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Networking;


public class flag : NetworkBehaviour { 
    bool flaginb;
    GameObject flag2;
    bool OnPlayer;
    bool spawntrue;
    float timer = 10f;
    Vector3 postion;
    GameObject flagz;
    Player Player;
    float cont = 0f;
    bool drop;
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
        int red = UnityEngine.Random.Range(0, 6);
        if (red == 0)
        {
            postion = new Vector3();
            Instantiate(flagz, postion, Quaternion.identity);
        }
        else if (red == 1)
        {
            postion = new Vector3();
            Instantiate(flagz, postion, Quaternion.identity);

        }
        else if (red == 2)
        {
            postion = new Vector3();
            Instantiate(flagz, postion, Quaternion.identity);
        }
        else if (red == 3)
        {
            postion = new Vector3();
            Instantiate(flagz, postion, Quaternion.identity);
        }
        else if (red == 4)
        {
            postion = new Vector3();
            Instantiate(flagz, postion, Quaternion.identity);
        }
        else if (red == 5)
        {
            postion = new Vector3();
            Instantiate(flagz, postion, Quaternion.identity);
        }
        else if (red == 6)
        {
            postion = new Vector3();
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