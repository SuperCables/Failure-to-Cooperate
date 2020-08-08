using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Definitions : MonoBehaviour
{
    public VesselData[] hulls;

    //generic
    public EngineData[] engines;
    //shields
    

    public WeaponData[] ProjectileWeapons;

    void Start()
    {
        
    }

    void Update()
    {
        
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

