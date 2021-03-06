﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class WeaponBank : MonoBehaviour
{
    public float aimArc = 60; //swivel range
    public float maxRange; //range of longest weapon

    public Entity FireAt;

    public Weapon[] guns;
    WeaponManager WeaponManager;

    void Start()
    {
        GetComponentInParent<Distributer>().FullRebuild += Rebuild;
        Rebuild();
    }

    void Rebuild()
    {
        WeaponManager = GetComponentInParent<WeaponManager>();
        guns = GetComponentsInChildren<Weapon>();

        maxRange = 0;
        foreach (Weapon v in guns)
        {
            if (v.range > maxRange)
            {
                maxRange = v.range;
            }
        }
    }

    void Update()
    {
        if (NetworkServer.active)
        {
            FireAt = null;
            bool noTarget = true;

            if (WeaponManager.Target1 != null)
            {
                if (AimAt(WeaponManager.Target1)) { noTarget = false; }
            }

            if (noTarget && WeaponManager.Target2 != null)
            {
                if (AimAt(WeaponManager.Target2)) { noTarget = false; }
            }

            if (noTarget)
            {
                //AimAt(); //aim at closest or somthing
            }
        } //end is server
    }

    bool AimAt(Entity target)
    {//this is only called by the server, so this will never run on the client
        
        Vector3 pos = transform.position;
        Vector3 targetPos = target.transform.position;

        //TODO: Lead target here!
        Vector3 leadPoint = targetPos;

        Quaternion aimAngle = Quaternion.Inverse(transform.rotation) * Quaternion.LookRotation(leadPoint - pos, Vector3.up);
        //print(aimAngle.eulerAngles);
        
        if (Quaternion.Angle(Quaternion.identity, aimAngle) < aimArc/2) //if in arc
        {
            FireAt = target;
            return true;
        }
        else
        {
            FireAt = null;
            return false;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.matrix = transform.localToWorldMatrix;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(1f, 0.1f, 0.1f));

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = Color.red;
        //Vector3 direction = transform.TransformDirection(Vector3.forward) * 1;
        //Gizmos.DrawRay(transform.position, direction);
        Gizmos.DrawFrustum(Vector3.zero, aimArc, 10, 0.05f, 1);
        
    }
}
