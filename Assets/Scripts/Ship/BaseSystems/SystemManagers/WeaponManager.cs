using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TargetPrioritys {Closest, Weakest }

public class WeaponManager : BaseSystemManager
{

    [Header("Self Assign")]
    public BaseWeapon[] weaponMounts;
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
        weaponMounts = GetComponentsInChildren<BaseWeapon>();
        weaponArray = GetComponentsInChildren<WeaponArray>();

        float thisWattage = 0;
        foreach (BaseWeapon v in weaponMounts)
        {
            if (v != null)
            {
                thisWattage += v.wattage;// * v.usability;
            }
        }
        maxWattage = thisWattage;
    }

    public override void Update()
    {
        base.Update();
    }
}
