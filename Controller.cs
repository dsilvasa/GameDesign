using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Controller : NetworkBehaviour {
	//movement
	[SyncVar]
	public GameObject lobbyOwner;
	public bool Init = false;
	public GameObject square;

	public Rigidbody myRig;
	public GameObject cam;
	public float speed = 2;
	public Vector3 dir = new Vector3();
	public float speedH = 2.0f;
	public float speedV = 2.0f;
	private float yaw = 0.0f;
	private float yee = 0.0f;
	public bool canmove = true;
	//private float pitch = 0.0f;
	private float Distance;
	public Vector3 lastPostion;
	//stamina
	private float regain = 5;
	public float constant;
	[SyncVar]
	public float mp = 100;
	[SyncVar]
	public float maxmp = 100;
	// attack
	[SyncVar]
	public float Attackcool;
	[SyncVar]
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



	[SyncVar]
	public int health = 20;
	[SyncVar]
	public int maxHealth = 20;

	void Start () {

		cam = GameObject.Find ("Main Camera");
		myRig = this.gameObject.GetComponent<Rigidbody>();
		dir = Vector3.zero;
		NetworkLobbyManager temp = GameObject.Find("Network").GetComponent<NetworkLobbyManager>();
		if (isLocalPlayer)
		{
			foreach (NetworkLobbyPlayer I in temp.lobbySlots)
			{
				if (I != null && I.GetComponent<NetworkIdentity>().playerControllerId == this.playerControllerId)
				{
					CmdsetOwner(I.gameObject);
					break;
				}
			}
		}
		//Set coordinates to be random
		if (isServer)
		{
			Vector3 pos = new Vector3(Random.Range(-2, 2), Random.Range(-2, 2), 0);
			this.transform.position = pos;
		}
	}

	[Command]
	public void CmdHandleMove(float x, float y)
	{
		myRig.velocity = new Vector3(x, y, 0) * speed;
	}
	void OnGUI() {
		if (isLocalPlayer) {
//		

			GUI.Label(new Rect(1,1,200,200), "Stamina "+ mp.ToString() +"/"+maxmp);
			GUI.Label(new Rect(300,1,200,200), "Health "+ health.ToString() +"/"+maxHealth);
			GUI.Label(new Rect(400,1,200,200), "Attack Cool Down "+ Attackcool.ToString() +" Seconds");



		}
	}

	// Update is called once per frame
	void Update()
	{
		if (lobbyOwner != null && !Init)
		{
			Init = true;
			Debug.Log("The char class is " + lobbyOwner.GetComponent<MyOwnLobbyPlayer>().charClass);
			switch (lobbyOwner.GetComponent<MyOwnLobbyPlayer>().charClass)
			{
			//As it turns out this is the only way to change the mesh.  (Didn't actually know this).   
			//Most of you will be using sprites.  
			case "Square":
				this.gameObject.GetComponent<MeshFilter>().mesh = square.GetComponent<MeshFilter>().sharedMesh;
				break;


			default:
			break; }
			switch (lobbyOwner.GetComponent<MyOwnLobbyPlayer>().team){
			case "Red":
				this.Team = 'R';
				break;
			case "Blue":
				this.Team = 'B';
				break;
			}


			//I could do the same with owner.team
			Debug.Log(lobbyOwner.GetComponent<MyOwnLobbyPlayer>().team);
		}

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



		if (pl2.Team == team && Attackcool ==0 && mp>15)

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
					canattack = false;
					Attackcool += 15;
					mp -= 15;
					attack(damage, pla2); // have it set to is sever
					pla2.attack(pla2.damage, this);
				}
			}
		}

		if (isClient)
		{
			if (coll.gameObject.tag == "Player")
			{
				if (canattack == true) {	
					canattack = false;
					Attackcool += 15;
					mp -= 15;
				}
			}
		}
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
				cam.transform.rotation = Quaternion.Euler (0,myRig.rotation.eulerAngles.y, 0);//this.myRig.rotation;
				cam.transform.position = new Vector3 (this.myRig.position.x, this.myRig.position.y, this.myRig.position.z);
				CmdSetDirection (xmove, ymove, yaw);
				Cmdstamina (myRig.position, lastPostion);
				lastPostion = myRig.position;

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
		if (Attackcool > 0) {
			Attackcool -= Time.deltaTime;
		} 
		else {
			canattack = true;
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

		myRig.transform.eulerAngles = new Vector3(0.0f,yaw, 0.0f);

	}

	[Command]
	public void Cmdstamina(Vector3 x, Vector3 y)
	{

		Distance = Vector3.Distance (x, y);
		Debug.Log (Distance);
		int round = Mathf.CeilToInt (Distance);
		mp = mp - Mathf.CeilToInt(round / (float)1.5);
		if (mp <= 0) {
			canmove = false;
		} else {
			canmove = true;
		}
		if (x == y && mp < maxmp) {
			mp += regain - 1 * (mp / maxmp) * constant;
			if (mp > maxmp) 
			{
				mp = maxmp;
			}
		}
	}

}
