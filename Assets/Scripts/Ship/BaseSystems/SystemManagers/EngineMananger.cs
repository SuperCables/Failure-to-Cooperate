using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineMananger : BaseSystemManager
{
    
    public Engine[] engines;

    [Space(10)]
    public float thrust; //sum of all engine thrust * average of all cores usability
    public float maxThrust; //sum of all engine thrust

    public override void Start()
    {
        base.Start();
        GetComponentInParent<Entity>().FullRebuild += Rebuild;
        Rebuild();
    }

    void Rebuild()
    {
        engines = GetComponentsInChildren<Engine>();
        float thisWattage = 0;
        float thisMaxThrust = 0;
        foreach (Engine v in engines)
        {
            if (v != null)
            {
                thisWattage += v.wattage;
                thisMaxThrust += v.thrust;
            }
        }
        maxWattage = thisWattage;
        maxThrust = thisMaxThrust;
    }

    public override void Update()
    {
        base.Update();
        float thisThrust = 0;

        foreach (Engine v in engines)
        {
            if (v != null)
            {
                thisThrust += v.thrust * v.usability;
            }
        }
        thrust = thisThrust;
    }
}
