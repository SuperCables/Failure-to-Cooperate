using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineMananger : BaseSystemManager
{
    
    public EngineMount[] engines;

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
        engines = GetComponentsInChildren<EngineMount>();
        float thisWattage = 0;
        float thisMaxThrust = 0;
        foreach (EngineMount v in engines)
        {
            if (v.engine != null)
            {
                thisWattage += v.engine.wattage;
                thisMaxThrust += v.engine.thrust;
            }
        }
        maxWattage = thisWattage;
        maxThrust = thisMaxThrust;
    }

    public override void Update()
    {
        base.Update();
        float thisThrust = 0;

        foreach (EngineMount v in engines)
        {
            if (v.engine != null)
            {
                thisThrust += v.engine.thrust * v.usability;
            }
        }
        thrust = thisThrust;
    }
}
