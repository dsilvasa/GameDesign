using UnityEngine;
using System.Collections;

public class Base : NetworkBehaviour
{
    public GameObject B2;
    int HP = 1000;
    bool vor = true;
    public char Team;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
   

    void takeDamage(Player play, int HP)
    {
        HP -= play.damage;
    }
    void OnCollisonEnter(Collision c)
    {
        Player pla2 = c.gameObject.GetComponent<Player>();
        if (c.gameObject.tag == "player")
        {
            if (vor == true)
            {
                if (pla2.Team != Team)
                {
                    takeDamage(pla2, HP);
                }

            }

        }

        //on colistion check to see if player if player set on player to true then make flag a subcatagrio of the player
    }
}