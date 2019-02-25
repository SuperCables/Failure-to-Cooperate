using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CoreBank))]
public class Reactor : MonoBehaviour
{
    public float wattage = 2; //power produced per second
    public float powerOutput = 0;


    CoreBank cores;

    void Start()
    {
        cores = GetComponent<CoreBank>();
    }

    void Update()
    {
        powerOutput = wattage * cores.usability;
    }
}
