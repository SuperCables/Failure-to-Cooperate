using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CoreBank))]
public class BallastPump : MonoBehaviour
{
    public float maxFlowRate = 5;
    public float wattage = 2;

    [HideInInspector]
    public float flowRate;

    [HideInInspector]
    public CoreBank cores;

    void Start()
    {
        cores = GetComponent<CoreBank>();
    }

    void Update()
    {
        flowRate = maxFlowRate * cores.usability;
    }
}
