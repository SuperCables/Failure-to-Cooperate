using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CompulsiveAssembly : NetworkBehaviour
{

    public GameObject target;
    
    [SyncVar]
    public uint targetID;
    [SyncVar(hook = nameof(SetPosition))]
    public Vector3 pos;
    [SyncVar(hook = nameof(SetRotation))]
    public Quaternion rot;

    public uint currentID;

    

    void Start()
    {
        //print("hi");
        syncInterval = 0;
    }


    void Update()
    {
        if (targetID != currentID)
        {
            TryFindParent();
        }
    }

    public void TryFindParent()
    {
        if (NetworkIdentity.spawned.TryGetValue(targetID, out NetworkIdentity identity))
        {
            target = identity.gameObject;
            currentID = identity.netId;
            SetParent();
        }
    }

    public void SetParent()
    {
        transform.SetParent(target.transform, false);
        //print("Assembled");
    }

    public void SetPosition(Vector3 value)
    {
        transform.localPosition = value;
        pos = value;
    }

    public void SetRotation(Quaternion value)
    {
        transform.localRotation = value;
        rot = value;
    }

    
}
