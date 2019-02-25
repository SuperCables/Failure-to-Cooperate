using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthControl : MonoBehaviour
{
    public MechCount depth;
    public MechDecade setDepth;
    public ShipMovement moveing;
    float lastDepth = 0;

    void Start()
    {
        
    }

    void Update()
    {
        depth.count = -moveing.transform.position.y;
        if (lastDepth != setDepth.count)
        {
            lastDepth = setDepth.count;
            Game.global?.localConnection?.CmdSetShipDepth(setDepth.count);
        }
    }
}
