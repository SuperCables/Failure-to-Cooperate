using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDrawGizmos()
    {
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(new Vector3(0, 1, 0), new Vector3(0.1f, 0.02f, 0.1f));


        //Gizmos.color = Color.red;
        //Vector3 direction = transform.TransformDirection(Vector3.forward) * 1;
        //Gizmos.DrawRay(transform.position, direction);
        //Gizmos.DrawFrustum(Vector3.zero, 1, 10, 0.05f, 1);

    }

    void OnDrawGizmosSelected()
    {
        //Gizmos.matrix = transform.localToWorldMatrix;

        //Gizmos.color = Color.yellow;
        //Gizmos.DrawWireCube(Vector3.zero, new Vector3(1f, 0.1f, 0.1f));
    }
}
