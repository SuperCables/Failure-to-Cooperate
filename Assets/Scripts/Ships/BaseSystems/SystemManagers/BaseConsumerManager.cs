using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class BaseConsumerManager : NetworkBehaviour //more of a consumer mananger, doesn't apply to reactors and batterys
{
    [SyncVar]
    public float allocation = 1; //engeneers throttle
    [SyncVar]
    public float energy = 0; //buffer
    //[SyncVar]
    public float maxEnergy = 5;

    [HideInInspector]
    public float demand; //per frame
    public float demandSecond; //per second (inspecting only, remove later)

    public float maxWattage; //how much can it consume? (how fast this system will charge)
    //public float preformance = 1; //preformance multiplier

    public float heat; //average heat


    public virtual void Start()
    {
        
    }

    public virtual void Rebuild()
    {
        maxEnergy = maxWattage * 10; //seconds of power
        //maxEnergy = 50;
    }

    public virtual void Update()
    {
        demandSecond = demand / Time.deltaTime;
    }



}
