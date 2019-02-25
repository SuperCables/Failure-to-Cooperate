using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CoreBank))]
public class BaseSystemMount : MonoBehaviour
{
    public float mass = 10;
    public float heat;
    //[HideInInspector]
    public CoreBank cores;

    public virtual void Start()
    {
        cores = GetComponent<CoreBank>();
    }

    public virtual void Update()
    {

    }
}
