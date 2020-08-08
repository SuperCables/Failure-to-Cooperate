using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[RequireComponent(typeof(CoreBank))]
public class Battery : NetworkBehaviour
{
    [SyncVar]
    public float perfectMaxPower = 50;
    [HideInInspector]
    public float maxPower = 0;
    CoreBank cores;


    void Start()
    {
        cores = GetComponent<CoreBank>();
    }

    void Update()
    {
        maxPower = perfectMaxPower * cores.usability;
    }
}
