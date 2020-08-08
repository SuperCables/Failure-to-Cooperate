using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Mirror;

public class Distributer : NetworkBehaviour //will be compulsive assembler!
{
    public Transform rooms; //where to put the rooms

    [Header("Self Assign")]
    //[HideInInspector]
    public Entity entity;
    //[HideInInspector]
    ReactorManager reactorManager;
    //[HideInInspector]
    BatteryMananger batteryMananger;
    //[HideInInspector]
    public EngineMananger engineManager;
    //[HideInInspector]
    public WeaponManager weaponManager;
    //[HideInInspector]
    public TorpedoMananger torpedoMananger;
    [Space(10)]
    public BaseConsumerManager[] consumers;

    [SyncVar]
    public float batteryPower = 1000;
    [SyncVar]
    public float maxBatteryPower = 1000;



    public event Action FullRebuild; //for get component and such
    //public event Action FullRecalc; //for calculating constants like engine wattage
    //[HideInInspector]
    //[Header("Component Refrences")]

    float FullRebuildTimer = -1;

    void Start()
    {
        FullRebuild += Rebuild;
        FullRebuild?.Invoke();
    }

    void Rebuild()
    {
        reactorManager = GetComponentInChildren<ReactorManager>();
        batteryMananger = GetComponentInChildren<BatteryMananger>();

        entity = GetComponentInParent<Entity>();
        engineManager = GetComponentInChildren<EngineMananger>();
        weaponManager = GetComponentInChildren<WeaponManager>();
        torpedoMananger = GetComponentInChildren<TorpedoMananger>();
    }

    void Update()
    {
        batteryPower += reactorManager.production * Time.deltaTime;

        maxBatteryPower = batteryMananger.maxPower;
        if (batteryPower > maxBatteryPower)
        {
            batteryPower = maxBatteryPower;
        }

        if (batteryPower > 0)
        {
            DistributePower();
        }

        if (FullRebuildTimer > 0)
        {
            FullRebuildTimer -= Time.deltaTime;
            if (FullRebuildTimer <= 0) { FullRebuild?.Invoke(); }
        }
    }

    void DistributePower()
    {
        float powerDemand = 0; //how much power do we want to use this frame?
        //Add Power Demands
        foreach (BaseConsumerManager v in consumers) //for each system
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

                if (v.energy > v.maxEnergy + 1) //if overpowered
                {
                    v.energy = v.maxEnergy + 1; //lose the extra power
                }
            }

        }


        if (powerDemand > 0)
        {
            float satisfaction = Mathf.Min(batteryPower / powerDemand, 1); //track how well we can satisfy all the equipment

            foreach (BaseConsumerManager v in consumers)
            {
                if (v != null) //if the system exist
                {
                    v.energy += v.demand * satisfaction; //give it the power we can
                }
            }

            batteryPower -= powerDemand * satisfaction; //consume the power
        }
    }

    public void InvokeFullRebuildDelay()
    {
        FullRebuildTimer = 3;
    }

    //public void Morph (VesselData data)
    //{
    //    //read mesh
    //    //read colliders
    //    //read shape

    //    foreach (EngineRoomData v in data.engineRooms)
    //    {
    //        HullRoom room = Instantiate(G.worldMananger.engineRoomTemplate);
    //        //room.transform.SetParent();
    //    }
    //}
}

