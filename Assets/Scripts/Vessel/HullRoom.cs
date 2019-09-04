using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HullRoom : MonoBehaviour
{
    public float width = 0.1f;
    public float length = 0.1f;


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
        Gizmos.DrawCube(new Vector3(0, 1, 0), new Vector3(width, 0.02f, length));
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector3(0, 0.5f, 0), new Vector3(width, 1f, length));
    }

}
