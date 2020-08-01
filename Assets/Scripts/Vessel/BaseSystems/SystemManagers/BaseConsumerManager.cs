using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseConsumerManager : MonoBehaviour //more of a consumer mananger, doesn't apply to reactors and batterys
{

    public float allocation = 1; //engeneers throttle
    public float energy = 0; //buffer
    public float maxEnergy = 5;

    [HideInInspector]
    public float demand;
    public float demandSecond;

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
