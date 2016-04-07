using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {
    public Rigidbody myRig;
    public float speed = 2;
    public bool canattack;
    public bool hasflag;
    public int health;
    public char Team;
    public int damage;
    private bool hasitem;
    private bool Defeence;
    private int Distance;
    // Use this for initialization

    

    [SyncVar]
    public int health = 5;
    [SyncVar]
    public int maxHealth = 5;

	void Start () {
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


    }
   private void attack()
    {

    }
    private static bool  Canattack() {
        return
    }
    private int takedamge() {
        return 
            }

 private void item loaditem()
    {
        return
    }

}
