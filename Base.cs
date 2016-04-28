using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class Base : NetworkBehaviour
{
    public GameObject B2;
    int HP = 1000;
    bool vor = false;
    public char Team;
    //bool inbase2;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isServer)
        {
            if (vor == true)
            {
                HP -= 5;
            }
        }
    }
    public void inBase()
    {
        vor = true;
    }

    void takeDamage(Controller play, int HP)
    {
        HP -= play.damage;
    }
    void OnCollisonEnter(Collision c)
    {
        Controller pla2 = c.gameObject.GetComponent<Controller>();
        if (c.gameObject.tag == "Controller")
        {
            if (isServer)
            {
                if (vor == true)
                {
                    if (pla2.Team != Team)
                    {
                        takeDamage(pla2, HP);
                    }

                }

            }

            //on colistion check to see if Controller if Controller set on Controller to true then make flag a subcatagrio of the Controller
        }
    }
}