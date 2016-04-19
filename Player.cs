using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Player : NetworkBehaviour
{
    public Rigidbody myRig;
    public GameObject P2;
    public float speed = 2;
    public bool canattack;
    public bool hasflag;
    // public int health;
    public char Team;
    public int damage;
    private bool hasitem;
    private bool Defeence;
    private int Distance;
    public bool turn;
    public float mp = 100;
    public float maxmp = 100;

    public float Attackcool;
    public Vector3 lastPostion;
    private float itemcool;
    private float regain = 5;
    public float constant;
    // Use this for initialization



    [SyncVar]
    public int health = 5;
    [SyncVar]
    public int maxHealth = 5;

    void Start()
    {
        myRig = this.gameObject.GetComponent<Rigidbody>();
    }

    [Command]
    public void CmdHandleMove(float x, float y)
    {
        myRig.velocity = new Vector3(x, y, 0) * speed;
    }

    [Command]


    // Update is called once per frame
    void Update()
    {

        if (myRig.position == lastPostion && canattack == false)
        {

            mp += regain - 1 * (mp / maxmp) * constant;
        }
        lastPostion = myRig.position;
        Attackcool += 0.5f;

    }



    public void attack(int damage, Player pl2)
    {

        pl2.health -= damage;


    }
    private static bool Canattack(Player pl2, char team, bool canattack, float Attackcool, float mp)
    {



        if (pl2.Team == team && Attackcool > 2 && mp > 15)

        {

            canattack = true;
        }
        return canattack;
    }
    // private int takedamge(int hp,Player pl2) {

    /// return 
    //   }

    //private void item loaditem()
    // {
    //   return
    //} 

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (isServer)
        {
            if (coll.gameObject.tag == "player")
            {
                Player pla2 = coll.gameObject.GetComponent<Player>();
                if (turn == true)
                {
                    canattack = Canattack(pla2, Team, canattack, Attackcool, mp);
                    if (canattack == true)
                    {
                        Attackcool -= 5;
                        mp -= 15;
                        attack(damage, pla2); // have it set to is sever
                        canattack = false;
                        pla2.attack(pla2.damage, this);

                    }
                }

            }
        }
    }

}