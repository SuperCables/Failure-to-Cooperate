using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TargetPrioritys {Closest, Weakest }

public class WeaponManager : BaseSystemManager
{
    public WeaponMount[] weaponMounts;
    public WeaponArray[] weaponArray;
    [Space(10)]
    public Entity Target1;
    public Entity Target2;

    public override void Start()
    {
        base.Start();
        GetComponentInParent<Entity>().FullRebuild += Rebuild;
        Rebuild();
    }

    void Rebuild()
    {
        weaponMounts = GetComponentsInChildren<WeaponMount>();
        weaponArray = GetComponentsInChildren<WeaponArray>();

        float thisWattage = 0;
        foreach (WeaponMount v in weaponMounts)
        {
            if (v.gun != null)
            {
                thisWattage += v.gun.wattage;// * v.usability;
            }
        }
        maxWattage = thisWattage;
    }

    public override void Update()
    {
        base.Update();
    }
}
