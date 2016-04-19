using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Controller : NetworkBehaviour {
	public Rigidbody myRig;
	public GameObject cam;
	public float speed = 2;
	public Vector3 dir = new Vector3();
	public bool canattack;
	public bool hasflag;
	// public int health;
	public char Team;
	public int damage;
	private bool hasitem;
	private bool Defence;
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

	void Start () {
		myRig = this.gameObject.GetComponent<Rigidbody>();
		dir = Vector3.zero;
	}

	[Command]
	public void CmdHandleMove(float x, float y)
	{
		myRig.velocity = new Vector3(x, y, 0) * speed;
	}


	// Update is called once per frame
	void Update()
	{

		if(isServer)
		{ 
			ServerUpdate ();
		}
		if (isLocalPlayer)
		{
			ClientUpdate();
		}

	}



	public void attack(int damage,Controller pl2)
	{

		pl2.health -= damage;


	}
	private static bool  Canattack(Controller pl2,char team,bool canattack,float Attackcool, float mp) {



		if (pl2.Team == team && Attackcool >2 && mp>15)

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
	{ if (isServer)
		{
			if (coll.gameObject.tag == "player")
			{
				Controller pla2 = coll.gameObject.GetComponent<Controller>();
				if (turn == true)
				{
					canattack = Canattack(pla2, Team,canattack,Attackcool,mp);
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


	void ClientUpdate()
	{
		float xmove = Input. GetAxisRaw("Horizontal");
		float ymove = Input.GetAxisRaw("Vertical");
		CmdSetDirection(xmove, ymove);
		cam.transform.position = new Vector3 (this.myRig.position.x, cam.transform.position.y, this.myRig.position.z);
		// update hud
		if(myRig.position== lastPostion && canattack == false )
		{

			mp += regain - 1 * (mp / maxmp) * constant;
		}
		lastPostion = myRig.position;
		Attackcool += 0.5f;

	}
	[Server]
	void ServerUpdate(){

		myRig.velocity = dir*3;

	}
	[Command]
	public void CmdSetDirection(float x, float y)
	{
		dir = new Vector3(x,0,y);

	}



}
