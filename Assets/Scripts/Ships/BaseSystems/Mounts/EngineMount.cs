using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineMount : BaseMount
{
    public Engine engine;

    public override void Start()
    {
        base.Start();
        GetComponentInParent<Distributer>().FullRebuild += Rebuild;
        Rebuild();
    }

    void Rebuild()
    {
        engine = GetComponentInChildren<Engine>();
    }

    public override void Update()
    {
        base.Update();
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
