using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Controller : NetworkBehaviour {
	//movement
	public Rigidbody myRig;
	public GameObject cam;
	public float speed = 2;
	public Vector3 dir = new Vector3();
	public float speedH = 2.0f;
	public float speedV = 2.0f;
	private float yaw = 0.0f;
	private float pitch = 0.0f;
	private int Distance;
	public Vector3 lastPostion;
	// public int health;
	//stamina
	private float regain = 5;
	public float constant;
	public float mp = 100;
	public float maxmp = 100;
	// attack
	public float Attackcool;
	public bool canattack;
	public int damage;
	private bool Defence;
	//flag/team
	public bool hasflag;
	public char Team;
	public bool turn;
	//item
	private float itemcool;
	private bool hasitem;
	public GameObject p2;
	// Use this for initialization



	[SyncVar]
	public int health = 5;
	[SyncVar]
	public int maxHealth = 5;

	void Start () {
		
		cam = GameObject.Find ("Main Camera");
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
		yaw += speedH * Input.GetAxis("Mouse X");
		pitch -= speedV * Input.GetAxis("Mouse Y");
		myRig.transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
		cam.transform.position = new Vector3 (this.myRig.position.x, this.myRig.position.y, this.myRig.position.z);
		cam.transform.rotation = this.transform.rotation;
		// update hud
		if(myRig.position== lastPostion && canattack == false && mp < maxmp)
		{

			mp += regain - 1 * (mp / maxmp) * constant;
			if (mp > maxmp) {
				mp = maxmp;
			}
		}
		lastPostion = myRig.position;
		Attackcool += 0.5f;

	}
	[Server]
	void ServerUpdate(){

		myRig.velocity = dir*3;

	}
	[Command]
	public void CmdSetDirection(float x, float z)
	{
		dir = transform.forward * z + transform.right * x;

	}



}
