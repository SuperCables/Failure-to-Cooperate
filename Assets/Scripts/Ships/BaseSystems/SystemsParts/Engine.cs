using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : BaseSystem
{
    [Header("Engine")]
    public float thrust = 25; //moving force
    public float torque = 5; //turning force


    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
    }

}

[System.Serializable]
public struct EngineData
{
    [Header("Basic Info")]
    public string title;
    public int cost;
    public float wattage;
    public float thermalLoad;

    [Header("Engine")]
    public float thrust; //moving force
    public float torque; //turning force

    public EngineData(string title, int cost, float wattage, float thermalLoad, float thrust, float torque)
    {
        if (thrust <= 0) { thrust = 5; }
        if (title == "") { title = "Engine " + thrust; }

        this.title = title;
        this.cost = cost;
        this.wattage = wattage;
        this.thermalLoad = thermalLoad;
        this.thrust = thrust;
        this.torque = torque;
    }
}