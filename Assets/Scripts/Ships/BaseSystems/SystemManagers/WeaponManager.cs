using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TargetPrioritys {Closest, Weakest }

public class WeaponManager : BaseConsumerManager
{

    [Header("Self Assign")]
    public Weapon[] weaponMounts;
    public WeaponBank[] weaponBanks;
    [Space(10)]
    public Entity Target1;
    public Entity Target2;

    public override void Start()
    {
        base.Start();
        GetComponentInParent<Distributer>().FullRebuild += Rebuild;
        Rebuild();
    }

    public override void Rebuild()
    {
        weaponMounts = GetComponentsInChildren<Weapon>();
        weaponBanks = GetComponentsInChildren<WeaponBank>();

        float thisWattage = 0;
        foreach (Weapon v in weaponMounts)
        {
            if (v != null)
            {
                thisWattage += v.wattage;// * v.usability;
            }
        }
        maxWattage = thisWattage;
        base.Rebuild();
    }

    public override void Update()
    {
        base.Update();
    }
}
