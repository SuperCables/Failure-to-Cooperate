using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorpedoArray : MonoBehaviour
{
    public TorpedoTube[] tubes;

    void Start()
    {

        GetComponentInParent<Entity>().FullRebuild += Rebuild;
        Rebuild();
    }

    void Rebuild()
    {
        tubes = GetComponentsInChildren<TorpedoTube>();
    }

    void Update()
    {
       
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Vector3 direction = transform.TransformDirection(Vector3.forward) * 0.5f;
        Gizmos.DrawRay(transform.position, direction);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(0.5f, 0.05f, 0.01f));
    }
}
