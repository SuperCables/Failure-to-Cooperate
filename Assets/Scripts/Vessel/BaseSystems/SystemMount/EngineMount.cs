using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineMount : MonoBehaviour
{
    public Engine engine;

    void Start()
    {
        GetComponentInParent<Entity>().FullRebuild += Rebuild;
        Rebuild();
    }

    void Rebuild()
    {
        engine = GetComponentInChildren<Engine>();
    }

    void Update()
    {

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
