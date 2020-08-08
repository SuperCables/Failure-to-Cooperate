using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactorManager : MonoBehaviour //this systemManager doesn't inheret from BaseSystem because it's not a consumer
{
    public Reactor[] reactors;

    [Space(10)]
    public float production;
    public float maxproduction;

    void Start()
    {
        GetComponentInParent<Distributer>().FullRebuild += Rebuild;
        Rebuild();
    }

    void Rebuild()
    {
        reactors = GetComponentsInChildren<Reactor>();
    }

    void Update()
    {
        production = 5;
        maxproduction = 5;
        foreach (Reactor v in reactors)
        {
            production += v.powerOutput;
            maxproduction += v.wattage;
        }
    }
}
