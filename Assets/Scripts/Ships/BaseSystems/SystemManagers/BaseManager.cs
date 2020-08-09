using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class BaseManager : NetworkBehaviour //assemble to location
{
    public GameObject parent;
    [SyncVar]
    public uint parentID;
    bool searchForParent = true;

    public override void OnStartClient()
    {
        TryFindParent();
    }

    public virtual void Start()
    {
        
    }

    public virtual void Update()
    {
        if (searchForParent)
        {
            TryFindParent();
        }
    }

    public void TryFindParent()
    {
        if (NetworkIdentity.spawned.TryGetValue(parentID, out NetworkIdentity identity))
        {
            parent = identity.gameObject;
            searchForParent = false;
            SetParent();
        }
    }

    public void SetParent()
    {
        transform.SetParent(parent.GetComponent<Distributer>().rooms, false);
        //print("Assembled");
    }
}
