using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[RequireComponent(typeof(CoreBank))]
public class BaseSystem : NetworkBehaviour
{
    //[HideInInspector]
    public CoreBank cores;
    public float usability = 1;

    public virtual void Start()
    {
        cores = GetComponent<CoreBank>();
    }

    public virtual void Update()
    {
        usability = cores.usability;
    }
}
