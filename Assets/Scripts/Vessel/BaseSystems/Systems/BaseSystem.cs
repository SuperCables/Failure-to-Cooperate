using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[RequireComponent(typeof(CoreBank))]
public class BaseSystem : NetworkBehaviour
{
    //[HideInInspector]
    public CoreBank Cores;
    public float usability = 1;

    public virtual void Start()
    {
        Cores = GetComponent<CoreBank>();
    }

    public virtual void Update()
    {
        usability = Cores.usability;
    }
}
