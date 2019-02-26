using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class WeaponArray : NetworkBehaviour
{
    public float aimArc = 60; //swivel range
    public float maxRange;

    public Entity FireAt;


    public WeaponMount[] Mounts; 
    WeaponManager WeaponManager;

    void Start()
    {
        GetComponentInParent<Entity>().FullRebuild += Rebuild;
        Rebuild();
    }

    void Rebuild()
    {
        WeaponManager = GetComponentInParent<WeaponManager>();
        Mounts = GetComponentsInChildren<WeaponMount>();
    }

    void Update()
    {
        if (isServer)
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

        maxRange = 0;
        foreach (WeaponMount v in Mounts)
        {
            if (v?.gun?.range > maxRange)
            {
                maxRange = v.gun.range;
            }
        }

    }

    bool AimAt(Entity target)
    {//this is only called by the server, so this will never run on the client
        
        Vector3 pos = transform.position;
        Vector3 targetPos = target.transform.position;

        //Lead target here!
        Vector3 leadPoint = targetPos;

        Quaternion aimAngle = Quaternion.Inverse(transform.rotation) * Quaternion.LookRotation(leadPoint - pos, Vector3.up);
        //print(aimAngle.eulerAngles);
        
        if (Quaternion.Angle(Quaternion.identity, aimAngle) < aimArc) //if in arc
        {
            //transform.localRotation = Quaternion.Euler(0, aimAngle, 0);
            FireAt = target;
            return true;
        }
        else
        {
            FireAt = null;
            //aimAngle = 0;
            return false;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 direction = transform.TransformDirection(Vector3.forward) * 1;
        Gizmos.DrawRay(transform.position, direction);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube (transform.position, new Vector3(0.1f, 0.1f, 1));
    }
}
