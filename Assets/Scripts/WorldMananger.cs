using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class WorldMananger : MonoBehaviour
{

    [Header("Assignment")]
    public Entity shipBase;
    [Space(10)]
    public HullRoom genericRoomTemplate;
    public HullRoom engineRoomTemplate;
    public HullRoom shieldRoomTemplate;
    //public HullRoom weaponRoomTemplate;
    [Space(10)]
    public Engine engineTemplate;
    //public Shield engineTemplate;
    public Weapon weaponTemplate;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SpawnTest()
    {
        Entity vessel = Instantiate(shipBase);
        vessel.transform.position = Vector3.forward * 200 + Random.insideUnitSphere * 50;
        NetworkServer.Spawn(vessel.gameObject);

        //add reactor

        VesselData hullData = G.definitions.hulls[0];

        //Add Engines
        EngineRoomData[] engineRoomsData = hullData.engineRooms;
        //EngineRoomData engineRoomData = engineRoomsData[0];

        EngineMananger engineMananger = vessel.GetComponentInChildren<EngineMananger>();
        print(engineMananger);

        foreach (EngineRoomData v in engineRoomsData)
        {
            HullRoom engine = Instantiate(engineRoomTemplate, engineMananger.transform);
            //NetworkServer.Spawn(engine.gameObject);

        }

        EngineMount[] engineMounts = vessel.GetComponentsInChildren<EngineMount>();
        foreach (EngineMount v in engineMounts)
        {
            Engine engine = Instantiate(engineTemplate);
            engine.transform.SetParent(v.transform, false);
            NetworkServer.Spawn(engine.gameObject);

        }

        //Add Weapons
        WeaponMount[] weaponMounts = vessel.GetComponentsInChildren<WeaponMount>();
        foreach (WeaponMount v in weaponMounts)
        {
            Weapon weapon = Instantiate(weaponTemplate);
            weapon.transform.SetParent(v.transform, false);
            NetworkServer.Spawn(weapon.gameObject);

        }

    }

}
