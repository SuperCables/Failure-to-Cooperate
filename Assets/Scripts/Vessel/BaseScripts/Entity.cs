﻿using System.Collections;
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
    public event Action FullRecalc; //for calculating constants like engine wattage

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

    void Start () {
        FullRebuild += Rebuild;

        FullRebuild?.Invoke();

        //Rebuild();
        body.isKinematic = !isServer; //all cliants are kinimatic
        Title = "Ship " + UnityEngine.Random.Range(10, 99);
        InGame.global.AddUnit(this);

    }

    void Rebuild()
    {
        body = GetComponent<Rigidbody>();
        colliders = GetComponentsInChildren<Collider>();
        vessel = GetComponent<Vessel>();
        hull = GetComponentInChildren<VesselHull>();
    }

	void FixedUpdate () {

	}

    void OnDestroy()
    {
        InGame.global.RemoveUnit(this);
    }

}
