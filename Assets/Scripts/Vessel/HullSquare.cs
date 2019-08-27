using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HullSquare : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnDrawGizmos()
    {
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(new Vector3(0, 1, 0), new Vector3(0.1f, 0.02f, 0.1f));
    }
}
