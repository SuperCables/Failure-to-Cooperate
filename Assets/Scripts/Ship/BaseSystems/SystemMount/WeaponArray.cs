using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class WeaponArray : NetworkBehaviour
{
    public Vector2 aimArc = new Vector2(60, 40); //H, V swivel range
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
        float mountAngle = transform.rotation.eulerAngles.y;
        Vector2 pos = Game.V3toV2(transform.position);
        Vector2 targetPos = Game.V3toV2(target.transform.position);

        //Lead target here!
        Vector2 leadPoint = targetPos;

        float targetAngle = 270 - Game.Vector2ToDegree(pos - leadPoint);
        float aimAngle = Mathf.DeltaAngle(mountAngle, targetAngle);
        if (Mathf.Abs(aimAngle * 2) < aimArc.x) //if in arc
        {
            //transform.localRotation = Quaternion.Euler(0, aimAngle, 0);
            FireAt = target;
            return true;
        }
        else
        {
            FireAt = null;
            aimAngle = 0;
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
