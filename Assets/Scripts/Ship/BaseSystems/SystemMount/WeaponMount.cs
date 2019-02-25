using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMount : BaseSystemMount
{
    public WeaponArray Rack;
    public BaseWeapon gun;

    public override void Start()
    {
        base.Start();
        GetComponentInParent<Entity>().FullRebuild += Rebuild;
        Rebuild();
    }

    void Rebuild()
    {
        Rack = GetComponentInParent<WeaponArray>();
        gun = GetComponentInChildren<BaseWeapon>();
    }

    public override void Update()
    {
        base.Update();
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
