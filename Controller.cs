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
	public bool canmove = true;
	//private float pitch = 0.0f;
	private float Distance;
	public Vector3 lastPostion;
	//stamina
	private float regain = 5;
	public float constant;
	[SyncVar]
	public double mp = 100;
	[SyncVar]
	public double maxmp = 100;
	// attack
	public float Attackcool;
	public bool canattack = true;
	public int damage = 5;
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
	public Texture2D emptyTex;
	public Texture2D fullTex;


	[SyncVar]
	public int health = 20;
	[SyncVar]
	public int maxHealth = 20;

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
	void OnGUI() {
		if (isLocalPlayer) {
			//draw the background:
			GUI.BeginGroup (new Rect (1, 1, 10* maxHealth, 10));
			GUI.Box (new Rect (0, 0, 100, 100), emptyTex);

			//draw the filled-in part:
			GUI.BeginGroup (new Rect (0, 0, 10 * health, 10));
			GUI.Box (new Rect (0, 0, 100, 100), fullTex);
			GUI.EndGroup ();
			GUI.EndGroup ();

			GUI.BeginGroup (new Rect (1, 1, 10* maxmp, 10));
			GUI.Box (new Rect (0, 0, 100, 100), emptyTex);

			//draw the filled-in part:
			GUI.BeginGroup (new Rect (0, 0, 10 * mp, 10));
			GUI.Box (new Rect (0, 0, 100, 100), fullTex);
			GUI.EndGroup ();
			GUI.EndGroup ();


		}
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
		if (this.health == 0)
		{
			//flag red = this.transform.GetComponent<flag>;
			if (this.transform.GetChildCount() > 0)
			{
				this.transform.GetChild(0).gameObject.GetComponent<flag>().drop2();
			}
			this.transform.DetachChildren();
			Destroy(this);
		}

	}



	public void attack(int damage,Controller pl2)
	{

		pl2.health -= damage;


	}
	private static bool  Canattack(Controller pl2,char team,bool canattack,float Attackcool, double mp) {



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

	void OnCollisionEnter(Collision coll)
	{ 		
		Debug.Log ("you hit me");
		

		if (isServer)
		{
			if (coll.gameObject.tag == "Player")
			{
				Controller pla2 = coll.gameObject.GetComponent<Controller>();
				canattack = Canattack(pla2, Team,canattack,Attackcool,mp);
				if (canattack == true)
				{
					Attackcool += 10;
					mp -= 15;
					attack(damage, pla2); // have it set to is sever
					canattack = false;
					pla2.attack(pla2.damage, this);
				}
			}
		}

//		if (isClient)
//		{
//			if (coll.gameObject.tag == "Player")
//			{
//				Controller pla2 = coll.gameObject.GetComponent<Controller>();
//				canattack = Canattack(pla2, Team,canattack,Attackcool,mp);
//				if (canattack == true)
//				{
//					Attackcool += 10;
//					mp -= 15;
//					attack(damage, pla2); // have it set to is sever
//					canattack = false;
//					pla2.attack(pla2.damage, this);
//				}
//			}
//		}
	}


	void ClientUpdate()
	{
		Cursor.lockState = CursorLockMode.Locked;

		// update hud
		if (isLocalPlayer) 
		{
			if (mp > 0) {
				float xmove = Input.GetAxisRaw ("Horizontal");
				float ymove = Input.GetAxisRaw ("Vertical");
				yaw += speedH * Input.GetAxis ("Mouse X");
				cam.transform.rotation = this.myRig.rotation;
				cam.transform.position = new Vector3 (this.myRig.position.x, this.myRig.position.y, this.myRig.position.z);
				CmdSetDirection (xmove, ymove, yaw);
				Cmdstamina (myRig.position, lastPostion);
				lastPostion = myRig.position;
				if (Attackcool > 0) {
					Attackcool -= 0.5f;
				} 
				else {
					canattack = true;
				}
			} 
			else {
				lastPostion = myRig.position;
				Cmdstamina (myRig.position, lastPostion);
			}
				

		}
	}
	[Server]
	void ServerUpdate(){
		if (mp > 0) {
			myRig.velocity = dir * 10;
		}
		else {
			myRig.velocity = dir * 0;
			Cmdstamina (myRig.position, lastPostion);

		}
	}
	[Command]
	public void CmdSetDirection(float x, float z, float yaw)
	{
		if (z != 0 || x != 0) {
			dir = transform.forward * z + transform.right * x + transform.up;
		} else {
			dir = transform.forward * z + transform.right * x + transform.up * 0;
		}

		myRig.transform.eulerAngles = new Vector3(0.0f, yaw, 0.0f);

	}

	[Command]
	public void Cmdstamina(Vector3 x, Vector3 y)
	{

		Distance = Vector3.Distance (x, y);
		Debug.Log (Distance);
		int round = Mathf.CeilToInt (Distance);

		mp = mp - round/1.5;
		if (mp <= 0) {
			canmove = false;
		} else {
			canmove = true;
		}
		if (x == y && mp < maxmp) {
			mp += regain - 1 * (mp / maxmp) * constant;
			if (mp > maxmp) {
				mp = maxmp;
			}
		}
	}

}
