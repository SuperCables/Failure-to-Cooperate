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

public class Entity : NetworkBehaviour
{
    
    [SyncVar]
    public string Title = "Namelessss123";
    [SyncVar]
    public EntityType entityType;
    [SyncVar]
    public bool alive = true;

    //All are optional and could be null!
    //[HideInInspector]
    public ShipMovement movement;
    //[HideInInspector]
    public Rigidbody body;
    //[HideInInspector]
    public Collider[] colliders;
    //[HideInInspector]
    public Distributer vessel;
    //[HideInInspector]
    public Health health;

    void Awake()
    {
        Title = "Ship " + UnityEngine.Random.Range(10, 99);
    }

    void Start()
    {
        G.global.AddUnit(this);

        body = GetComponent<Rigidbody>();
        movement = GetComponent<ShipMovement>();
        colliders = GetComponentsInChildren<Collider>();
        vessel = GetComponentInChildren<Distributer>();
        health = GetComponentInChildren<Health>();

        if (body != null)
        {
            body.isKinematic = !isServer; //all cliants are kinimatic
        }
    }

    void Update()
    {

    }

    void FixedUpdate () {

	}

    void OnDestroy()
    {
        G.global.RemoveUnit(this);
    }

}
