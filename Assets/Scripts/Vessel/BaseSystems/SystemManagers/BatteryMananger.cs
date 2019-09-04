using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryMananger : MonoBehaviour //this systemManager doesn't inheret from BaseSystem because it's not a consumer
{

    public float maxPower = 250;

    public Battery[] batterys;

    void Start()
    {
        GetComponentInParent<Entity>().FullRebuild += Rebuild;
        Rebuild();
    }

    void Rebuild()
    {
        batterys = GetComponentsInChildren<Battery>();
    }

    void Update()
    {
        maxPower = 50;
        foreach (Battery v in batterys)
        {
            maxPower += v.maxPower;
        }
    }
}
