using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class BaseWeapon : BaseSystem
{
    [Header("Aiming")]
    public float range = 250;

    //self assign
    [HideInInspector]
    public Vessel MountedVessel;
    [HideInInspector]
    public WeaponManager WeaponManager;
    [HideInInspector]
    public WeaponMount Mount;
    [HideInInspector]

    [SyncVar]
    Quaternion aimAngle;

    public override void Start()
    {
        base.Start();
        GetComponentInParent<Entity>().FullRebuild += Rebuild;
        Rebuild();
    }

    void Rebuild()
    {
        MountedVessel = GetComponentInParent<Vessel>();
        WeaponManager = GetComponentInParent<WeaponManager>();
        Mount = GetComponentInParent<WeaponMount>();
    }

    public override void Update()
    {
        base.Update();
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
            HoldTrigger(target);
            return true;
        }else{
            //aimAngle = Quaternion.identity;
            return false;
        }
    }


    public virtual void HoldTrigger(Entity target)
    {
        print("Attempted to fire BaseWeapon :(");
        print("Fire From derived classes only!");
    }

}

[System.Serializable]
public struct WeaponData
{
    [Header("Basic Info")]
    public string title;
    public int cost;
    public float wattage;
    public float thermalLoad;

    [Header("Aiming")]
    public float range;

    [Header("Ammo")]
    public int clipSize;

    [Header("Fireing")]
    public float cycleTime; //time between shots

    public float shellSpeed; //shell speed (m/s)

    float accuracy; //degrees, one standard devation. 95% won't be off by more than 2X this.

    int burstCount; //shells per shot

}

[System.Serializable]
public struct DamageData
{
    [Header("DamageType")]
    public float shieldDamage; //damage to various things (per shot or second)
    public float hullDamage;
    public float thermalDamage;
    public float systemDamage;
    public float stunDamage;

    [Header("Breach")]
    public float hullBreachMin; //min armor for guarentee to penitrate
    public float hullBreachMax; //max armor for chance to penitrate
    public float systemHitChance; //chance for penitrate to strike the desired system
}