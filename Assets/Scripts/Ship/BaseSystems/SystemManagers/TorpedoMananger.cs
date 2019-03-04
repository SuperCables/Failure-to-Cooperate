using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorpedoMananger : BaseSystemManager
{

    public TorpedoTube[] tubes;
    public TorpedoArray[] torpedoArray;


    public override void Start()
    {
        base.Start();
        GetComponentInParent<Entity>().FullRebuild += Rebuild;
        Rebuild();
    }

    void Rebuild()
    {
        tubes = GetComponentsInChildren<TorpedoTube>();
        torpedoArray = GetComponentsInChildren<TorpedoArray>();
    }

    public override void Update()
    {
        base.Update();
    }
}
