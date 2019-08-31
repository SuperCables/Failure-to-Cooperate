using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Distributer : NetworkBehaviour
{
    ReactorManager reactorManager;
    BatteryMananger batteryMananger;

    public BaseSystemManager[] systems;

    [SyncVar]
    public float batteryPower = 1000;
    [SyncVar]
    public float maxBatteryPower = 1000;

    void Start()
    {
        GetComponentInParent<Entity>().FullRebuild += Rebuild;
        Rebuild();
    }

    void Rebuild()
    {
        reactorManager = GetComponentInChildren<ReactorManager>();
        batteryMananger = GetComponentInChildren<BatteryMananger>();
    }
    
    void Update()
    {
        batteryPower += reactorManager.production * Time.deltaTime;

        maxBatteryPower = batteryMananger.maxPower + 50;
        if (batteryPower > maxBatteryPower)
        {
            batteryPower = maxBatteryPower;
        }

        if (batteryPower > 0)
        {
            DistributePower();
        } 
    }

    void DistributePower()
    {
        float powerDemand = 0; //how much power do we want to use this frame?
        //Add Power Demands
        foreach (BaseSystemManager v in systems) //for each system
        {
            if (v != null) //if it exist
            {
                //draw the min of how much we need and how much we can get
                float demand = Mathf.Min(v.maxEnergy - v.energy, (v.maxWattage * Time.deltaTime) * v.allocation); 

                if (demand > 0) //if we need somthing
                {
                    v.demand = demand; //set its demand
                    powerDemand += demand; //track total usage
                }
                else
                {
                    v.demand = 0; //we cant give back power
                }

                if (v.energy > v.maxEnergy) //if overpowered
                {
                    v.energy = v.maxEnergy; //lose the extra power
                }
            }

        }


        if (powerDemand > 0)
        {
            float satisfaction = Mathf.Min(batteryPower / powerDemand, 1); //track how well we can satisfy all the equipment

            foreach (BaseSystemManager v in systems)
            {
                if (v != null) //if the system exist
                {
                    v.energy += v.demand * satisfaction; //give it the power we can
                }
            }

            batteryPower -= powerDemand * satisfaction; //consume the power
        }
    }

}
