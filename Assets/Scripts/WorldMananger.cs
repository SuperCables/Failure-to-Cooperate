using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class WorldMananger : MonoBehaviour
{

    [Header("Assignment")]
    public Entity shipBase;
    public VesselHull testHull;
    public Engine engineTemplate;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SpawnTest()
    {
        Entity vessel = Instantiate(shipBase);
        VesselHull hull = Instantiate(testHull);
        hull.transform.parent = vessel.transform;
        vessel.transform.position = Vector3.forward * 200 + UnityEngine.Random.insideUnitSphere * 50;
        NetworkServer.Spawn(vessel.gameObject);
        NetworkServer.Spawn(hull.gameObject);

        //add reactor

        EngineMount[] EngineMounts = hull.GetComponentsInChildren<EngineMount>();
        foreach (EngineMount v in EngineMounts)
        {
            Engine engine = Instantiate(engineTemplate);
            engine.transform.SetParent(v.transform, false);
            NetworkServer.Spawn(engine.gameObject);

        }
        vessel.InvokeFullRebuild();
    }

}
