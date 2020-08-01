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
    public TorpedoMananger torpedoMananger;
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
        torpedoMananger = GetComponentInChildren<TorpedoMananger>();
        distributer = GetComponentInChildren<Distributer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Morph (VesselData data)
    {
        //read mesh
        //read colliders
        //read shape

        foreach (EngineRoomData v in data.engineRooms)
        {
            HullRoom room = Instantiate(G.worldMananger.engineRoomTemplate);
            //room.transform.SetParent();
        }
    }

}

[System.Serializable]
public class VesselData
{
    public string title;
    public float cost;

    //add mesh
    //add colliders
    //add voxels (for damage)

    public GenericRoomData[] genericRooms;
    public EngineRoomData[] engineRooms;
    public ShieldRoomData[] shieldRooms;
    public WeaponBankData[] weapons;

}

[System.Serializable]
public class GenericRoomData
{
    public string title;
    public Vector2 pos;
    public Vector2 size;
    public Vector3 mountPos;
}

[System.Serializable]
public class ShieldRoomData
{
    public string title;
    public Vector2 pos;
    public Vector2 size;
    public Vector3 mountPos;
}

[System.Serializable]
public class EngineRoomData
{
    public string title;
    public Vector2 pos;
    public Vector2 size;
    public Vector3 mountPos;
}

[System.Serializable]
public class WeaponBankData
{
    public string title;
    public Vector3 pos;
    public Vector3 rot;
    [RangeAttribute(0, 360)]
    public float aimArc;
    public WeaponRoomData[] weaponRooms;

}

[System.Serializable]
public class WeaponRoomData
{
    //public string title;
    public Vector2 pos;
    public Vector2 size;
    public Vector3 mountPos;
}