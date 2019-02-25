using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CoreBank))]
public class EngineMount : BaseSystemMount
{

    public float usability = 1; //average of all cores usability

    public Engine engine;

    public override void Start()
    {
        base.Start();
        GetComponentInParent<Entity>().FullRebuild += Rebuild;
        Rebuild();
    }

    void Rebuild()
    {
        engine = GetComponentInChildren<Engine>();
    }

    public override void Update()
    {
        base.Update();
        usability = cores.usability;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 direction = transform.TransformDirection(Vector3.forward) * .2f;
        Gizmos.DrawRay(transform.position, direction);
        
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(.05f,.05f,.05f));
        //Gizmos.DrawSphere(transform.position, .1f);
    }
}
