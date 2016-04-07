using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Controller : NetworkBehaviour {
    public Rigidbody myRig;
    public float speed = 2;
    // Use this for initialization

    public GameObject bulletPrefab;
    public float shootTimer = 1.0f;
    public bool canShoot = true;


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
    public void CmdShoot(float fire)
    {
        //Run on the server
        if (fire > 0 && canShoot)
        {
            canShoot = false;
            shootTimer = 1.0f;
            GameObject temp = (GameObject)GameObject.Instantiate(bulletPrefab, this.transform.position + this.transform.right, Quaternion.identity);
            NetworkServer.Spawn(temp);
            temp.GetComponent<Rigidbody>().velocity = new Vector3(3, 0, 0);

        }
    }

	// Update is called once per frame
	void Update () {
        float x = 0;
        float y = 0;
        if (isLocalPlayer)
        {
            x = Input.GetAxisRaw("Horizontal");
            y = Input.GetAxisRaw("Vertical");
            float fire = Input.GetAxisRaw("Fire1");

            if (isServer)
            {
                myRig.velocity = new Vector3(x, y, 0) * speed;
                if (fire > 0 && canShoot)
                {
                    canShoot = false;
                    shootTimer = 1.0f;
                    GameObject temp = (GameObject)GameObject.Instantiate(bulletPrefab, this.transform.position + this.transform.right, Quaternion.identity);
                    NetworkServer.Spawn(temp);
                    temp.GetComponent<Rigidbody>().velocity = new Vector3(3, 0,0);
                }
            }
            else
            {
                CmdHandleMove(x, y);
                CmdShoot(fire);
            }
        }//Ends my input

        if(!canShoot && isServer)
        {
            shootTimer -= Time.deltaTime;
            if(shootTimer<=0)
            {
                canShoot = true;
                shootTimer = 1.0f;
            }
        }
	}//End of Update

    public void OnGUI()
    {
        if(isLocalPlayer)
        {
            GUI.Label(new Rect(20, 20, 200, 20), "HP = " + health);
        }
    }

    public void OnTriggerEnter(Collider C)
    {
        if(isServer && C.tag == "Bullet")
        {
            health -= 1;
            NetworkServer.Destroy(C.gameObject);
        }
    }

    if(isServer)
        {
        myrig.velocity=new Vector3(,Matrix4x4,YieldInstruction,0)*
    if(Mathf.abs(x)>mathf.Abs(y))
        {
        if(x>0)
        {
        this.gameObject.Getcompenets<animator>().setInterger("dir",0)
        else if(Matrix4x4.

}
