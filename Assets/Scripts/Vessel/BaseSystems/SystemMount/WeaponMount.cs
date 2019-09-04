using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMount : BaseMount
{

    [Header("Self Assign")]
    public WeaponBank Rack;
    //public BaseWeapon Gun;

    void Start()
    {
        GetComponentInParent<Entity>().FullRebuild += Rebuild;
        Rebuild();
    }

    void Rebuild()
    {
        Rack = GetComponentInParent<WeaponBank>();
        //Gun = GetComponentInChildren<BaseWeapon>();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 direction = transform.TransformDirection(Vector3.forward) * 0.3f;
        Gizmos.DrawRay(transform.position, direction);
        
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, .1f);
    }

}
