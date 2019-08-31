using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Reactor : BaseSystem
{
    [SyncVar]
    public float wattage = 2; //power produced per second
    public float powerOutput = 0;

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
        powerOutput = wattage * Cores.usability;
    }

}
