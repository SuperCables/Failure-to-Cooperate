using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class BaseWeapon : NetworkBehaviour
{
    [Header("Load")]
    public float wattage = 5;
    public float thermalLoad = 7; //heat per shot or heat per second

    //public float shieldDamage = 1; //damage to various things (per shot or second)
    //public float hullDamage = 1;
    //public float thermalDamage = 1;
    //public float systemDamage = 1;
    //public float stunDamage = 1;

    [Header("Aiming")]
    public float range = 250;

    [HideInInspector]
    public Entity MountedVessel;
    [HideInInspector]
    public WeaponManager WeaponManager;
    [HideInInspector]
    public WeaponMount Mount;
    [HideInInspector]

    [SyncVar]
    Quaternion aimAngle;

    public virtual void Start()
    {
        GetComponentInParent<Entity>().FullRebuild += Rebuild;
        Rebuild();
    }

    void Rebuild()
    {
        MountedVessel = GetComponentInParent<Entity>();
        WeaponManager = GetComponentInParent<WeaponManager>();
        Mount = GetComponentInParent<WeaponMount>();
    }

    public virtual void Update()
    {
        transform.localRotation = aimAngle;

        if (isServer)
        {
            bool noTarget = true;
            if (Mount?.Rack?.FireAt != null)
            {
                if (AimAt(Mount.Rack.FireAt)) { noTarget = false; }
            }
            if (noTarget)
            {
                //AimAt(); //aim at closest or somthing
            }
        } //end is server
    }

    bool AimAt(Entity target)
    {//this is only called by the server, so this will never run on the client
        //Quaternion currentAngle = Mount.Rack.transform.rotation;
        //Quaternion mountAngle = Mount.Rack.transform.localRotation;

        Vector3 pos = Mount.transform.position;
        Vector3 targetPos = target.transform.position;

        //Lead target here!
        Vector3 leadPoint = targetPos;

        if ((pos - leadPoint).sqrMagnitude < (range * range))
        {
            aimAngle = Quaternion.Inverse(Mount.Rack.transform.rotation) * Quaternion.LookRotation(leadPoint - pos, Vector3.up);
            Fire(target);
            return true;
        }else{
            //aimAngle = Quaternion.identity;
            return false;
        }
    }


    public virtual void Fire(Entity target)
    {
        print("Attempted to fire BaseWeapon :(");
        print("Fire From derived classes only!");
    }

}

