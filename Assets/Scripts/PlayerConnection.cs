﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerConnection : NetworkBehaviour
{
    //[SyncVar]
    public Entity playerEntity; //what ship is this player abord?

    void Start()
    {
        transform.SetParent(Game.global.connectionHolder.transform);
        if (isLocalPlayer)
        {
            Game.global.localConnection = this;

            //ship = GameObject.FindGameObjectWithTag("Player").GetComponent<Vessel>(); //replace with ship selecter!
        }
        playerEntity = GameObject.FindGameObjectWithTag("Player").GetComponent<Entity>(); //TODO replace with ship selecter!
    }


    void Update()
    {

    }

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
    public void CmdSetShipDepth(float Depth)
    {
        //playerEntity.vessel.movement.targetDepth = Depth;
    }

}
