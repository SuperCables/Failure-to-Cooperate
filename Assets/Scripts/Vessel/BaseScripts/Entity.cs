using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Mirror;

public enum EntityType
{
    Undefined,
    Ship,
    Base,
    Gate,
    Cargo,
    Astroid,
    Debree,
    Unknown
}

[RequireComponent(typeof(Rigidbody))]
public class Entity : NetworkBehaviour
{
    public event Action FullRebuild; //for get component and such
    //public event Action FullRecalc; //for calculating constants like engine wattage

    [SyncVar]
    public string Title = "Namelessss123";
    [SyncVar]
    public EntityType entityType;
    [SyncVar]
    public bool alive = true;

    //[HideInInspector]
    public Rigidbody body;
    //[HideInInspector]
    public Collider[] colliders;
    //[HideInInspector]
    public Vessel vessel;
    //[HideInInspector]
    public VesselHull hull;

    float FullRebuildTimer = -1;

    void Awake()
    {
        Rebuild();
        Title = "Ship " + UnityEngine.Random.Range(10, 99);
    }

    void Start()
    {
        G.global.AddUnit(this);
        FullRebuild += Rebuild;
        FullRebuild?.Invoke();
        body.isKinematic = !isServer; //all cliants are kinimatic
    }

    void Update()
    {
        if (FullRebuildTimer > 0)
        {
            FullRebuildTimer -= Time.deltaTime;
            if (FullRebuildTimer <= 0) { FullRebuild?.Invoke(); }
        }
    }

    void Rebuild()
    {
        body = GetComponent<Rigidbody>();
        colliders = GetComponentsInChildren<Collider>();
        vessel = GetComponent<Vessel>();
        hull = GetComponent<VesselHull>();
        print("Rebuild ship: " + Title);
    }

    public void InvokeFullRebuild()
    {
        FullRebuild?.Invoke();
    }

    public void InvokeFullRebuildDelay()
    {
        FullRebuildTimer = 3;
    }

    void FixedUpdate () {

	}

    void OnDestroy()
    {
        G.global.RemoveUnit(this);
    }

}
