using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerConnection : NetworkBehaviour
{
    public Entity playerEntity; //what ship is this player abord?

    void Start()
    {
        transform.SetParent(G.global.connectionHolder.transform);
        if (isLocalPlayer)
        {
            G.global.localConnection = this;
        }
        //playerEntity = GameObject.FindGameObjectWithTag("Player").GetComponent<Entity>(); //TODO replace with ship selecter!
    }


    void Update()
    {

    }

    //tell our ship to change somthing.
    [Command]
    public void CmdSetShipHeading(float direction)
    {
        playerEntity.vessel.movement.inputAngle = direction;
    }

    [Command]
    public void CmdSetShipThrust(float Thrust)
    {
        playerEntity.vessel.movement.inputThrottle = Thrust;
    }

    [Command]
    public void CmdSetShipDive(float angle)
    {
        playerEntity.vessel.movement.inputDive = angle;
    }

    [Command]
    public void CmdSummonEvil()
    {
        G.worldMananger.SpawnTest();
    }

}
