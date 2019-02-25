using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vessel : MonoBehaviour
{


    //[HideInInspector]

    //[Header("Component Refrences")]
    
    //[HideInInspector]
    public ShipMovement movement;
    //[HideInInspector]
    public EngineMananger engineManager;
    //[HideInInspector]
    public WeaponManager weaponManager;
    //[HideInInspector]
    public Distributer distributer;
    //[HideInInspector]
    public Entity entity;


    // Start is called before the first frame update
    void Start()
    {
        GetComponentInParent<Entity>().FullRebuild += Rebuild;
        Rebuild();
    }

    void Rebuild()
    {
        entity = GetComponent<Entity>();
        movement = GetComponent<ShipMovement>();
        engineManager = GetComponentInChildren<EngineMananger>();
        weaponManager = GetComponentInChildren<WeaponManager>();
        distributer = GetComponentInChildren<Distributer>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
