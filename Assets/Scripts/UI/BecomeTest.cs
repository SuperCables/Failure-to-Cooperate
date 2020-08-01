using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BecomeTest : MonoBehaviour
{
    //sets player as first ship

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Become()
    {
        PlayerConnection con = G.global?.localConnection;
        if (con != null)
        {
            con.playerEntity = G.global.allUnits[0];
        }
    }


}
