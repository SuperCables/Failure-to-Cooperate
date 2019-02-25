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
    float aimAngle;

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
        transform.localRotation = Quaternion.Euler(0, aimAngle, 0);

        if (isServer)
        {
            if (Mount?.Rack?.FireAt != null)
            {
                AimAt(Mount.Rack.FireAt);
            }
        } //end is server
    }

    bool AimAt(Entity target)
    {//this is only called by the server, so this will never run on the client
        float mountAngle = Mount.Rack.transform.rotation.eulerAngles.y;
        Vector2 pos = Game.V3toV2(Mount.transform.position);
        Vector2 targetPos = Game.V3toV2(target.transform.position);

        //Lead target here!
        Vector2 leadPoint = targetPos;

        float targetAngle = 270 - Game.Vector2ToDegree(pos - leadPoint);
        aimAngle = Mathf.DeltaAngle(mountAngle, targetAngle);
        if ((pos - leadPoint).sqrMagnitude < (range * range))
        {
            transform.localRotation = Quaternion.Euler(0, aimAngle, 0);
            Fire(target);
            return true;
        }else{
            aimAngle = 0;
            return false;
        }
    }


    public virtual void Fire(Entity target)
    {
        print("Attempted to fire BaseWeapon :(");
        print("Fire From derived classes only!");
    }

}

