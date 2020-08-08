using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorpedoMananger : BaseConsumerManager
{

    public TorpedoTube[] tubes;
    public TorpedoArray[] torpedoArray;


    public override void Start()
    {
        base.Start();
        GetComponentInParent<Distributer>().FullRebuild += Rebuild;
        Rebuild();
    }

    public override void Rebuild()
    {
        tubes = GetComponentsInChildren<TorpedoTube>();
        torpedoArray = GetComponentsInChildren<TorpedoArray>();
    }

    public override void Update()
    {
        base.Update();
    }
}
