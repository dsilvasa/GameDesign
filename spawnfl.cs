using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class spawnfl : NetworkBehaviour
{
    Vector3 postion;
    public GameObject o1;
    public GameObject o0;
    public GameObject o2;
    public GameObject o3;
    public GameObject o4;
    public GameObject o5;
    public GameObject flagz;
    // Use this for initialization
    void Start()
    {

    
        Debug.Log("hello world");
        if (isServer)
        {

            spawn();
        }

    }

    // Update is called once per frame
    void Update () {
	
	}
    public void spawn()
    {
        Vector3 redz;
        redz = new Vector3(0f, 4f, 0f);
        UnityEngine.Random ran = new UnityEngine.Random();
        // int red = UnityEngine.Random.Range(0, 4);
        int red = 0;
        if (red == 0)
        {
            Debug.Log("pissjfmkdlmc0");
            postion = o0.transform.position + redz; ;
            GameObject temp = (GameObject)Instantiate(flagz, postion, Quaternion.identity);
            NetworkServer.Spawn(temp);
        }
        else if (red == 1)
        {
            Debug.Log("fuckjfmkdlmc0");
            postion = o1.transform.position + redz; ;
            GameObject temp = (GameObject)Instantiate(flagz, postion, Quaternion.identity);
            NetworkServer.Spawn(temp);

        }
        else if (red == 2)
        {
            Debug.Log("fucrkjfmkdlmc0");
            postion = o2.transform.position + redz; ;
            GameObject temp = (GameObject)Instantiate(flagz, postion, Quaternion.identity);
            NetworkServer.Spawn(temp);
        }
        else if (red == 3)
        {
            Debug.Log("fuckejfmkdlmc0");
            postion = o3.transform.position + redz;
            GameObject temp = (GameObject)Instantiate(flagz, postion, Quaternion.identity);
            NetworkServer.Spawn(temp);
        }
        else if (red == 4)
        {
            Debug.Log("fuckjrfmkdlmc03");
            postion = o4.transform.position + redz; ;
            GameObject temp = (GameObject)Instantiate(flagz, postion, Quaternion.identity);
            NetworkServer.Spawn(temp);
        }



    }
}
