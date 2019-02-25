using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallastPumpsMananger : BaseSystemManager
{
    public BallastPump[] pumps;

    [Space(10)]
    public float flow; //sum of all pumps flow * average of all cores usability
    public float maxFlow; //sum of all pumps flow

    public override void Start()
    {
        base.Start();
        pumps = GetComponentsInChildren<BallastPump>();
    }

    public override void Update()
    {
        base.Update();
        float thisFlow = 0;
        float thisMaxFlow = 0;
        float thisWattage = 0;
        foreach (BallastPump v in pumps)
        {
            thisFlow += v.flowRate * 1; //v.usability;
            thisMaxFlow += v.flowRate;
            thisWattage += v.wattage;
        }
        flow = thisFlow;
        maxFlow = thisMaxFlow;
        maxWattage = thisWattage;
    }
}
