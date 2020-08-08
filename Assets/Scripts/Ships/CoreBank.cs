using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CoreBank : NetworkBehaviour
{
    public int coreCount = 1;

    [HideInInspector]
    public float usability = 1; //average of the cores

    SyncListFloat coresUsability;
    SyncListFloat coresIntegrity;

    void Start()
    {
        if (NetworkServer.active)
        {
            for (int i = 0; i < coreCount; i++)
            {
                coresUsability.Add(1);
                coresIntegrity.Add(100);
            }
        }
    }

    void Update()
    {
        //if (isServer)
        //{
            usability = 0;
            foreach (float v in coresUsability)
            {
                usability += v;
            }
            usability /= coreCount;
        //}
    }
}
