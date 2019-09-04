using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class WorldMananger : MonoBehaviour
{

    [Header("Assignment")]
    public Vessel shipBase;
    public VesselHull testHull;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SpawnTest()
    {
        Vessel v = Instantiate(shipBase);
        VesselHull h = Instantiate(testHull);
        h.transform.parent = v.transform;
        v.transform.position = Vector3.forward * 200 + UnityEngine.Random.insideUnitSphere * 50;
        NetworkServer.Spawn(v.gameObject);
        NetworkServer.Spawn(h.gameObject);
    }

}
