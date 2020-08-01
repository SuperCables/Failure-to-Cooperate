using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StationWeapons : MonoBehaviour
{
    WeaponManager weaponManager;

    [Header("Assignment")]
    public PushButton SetT1;
    public PushButton SetT2;

    void Start()
    {
        SetT1.Pressed += SetTarget1;
        SetT2.Pressed += SetTarget2;
    }

    void Update()
    {
        
    }

    Entity selfTargetPlayer()
    {
        if (G.global.selectedUnit == G.global.entity)
        {
            return null;
        }
        else
        {
            return G.global.selectedUnit;
        }
    }

    void SetTarget1()
    {
        //TODO: Use a command to tell the server to change target, not just the client!
        weaponManager = G.global.weaponManager;
        weaponManager.Target1 = selfTargetPlayer();
    }

    void SetTarget2()
    {
        weaponManager = G.global.weaponManager;
        weaponManager.Target2 = selfTargetPlayer();
    }

}
