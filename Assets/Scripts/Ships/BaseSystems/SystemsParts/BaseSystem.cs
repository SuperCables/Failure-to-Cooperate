using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[RequireComponent(typeof(CoreBank))]
public class BaseSystem : NetworkBehaviour
{
    [Header("Basic Info")]
    public string title = "System";
    public int cost = 1;
    public float wattage = 1;
    public float thermalLoad = 1;

    [Header("Cores (Self Assign)")]
    public CoreBank Cores;
    public float usability = 1;

    public virtual void Start()
    {
        Cores = GetComponent<CoreBank>();
        GetComponentInParent<Distributer>().InvokeFullRebuildDelay();
    }

    public virtual void Update()
    {
        usability = Cores.usability;
    }

}
