using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityListUI : BaseEntityScreenUI
{

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
        player = Game.global?.entity;
        if (player != null)
        {
            //UpdateInput();

            UpdateBlips();
        }
    }

    void UpdateBlips()
    {
        weaponManager = Game.global?.weaponManager;

        foreach (EntityListEntryUI v in blips)
        {
            //Update Blip Propertys
            int targetingIcon = 0; //start with no icon
            if (weaponManager?.Target2 == v.repEntity) { targetingIcon = 2; }
            if (weaponManager?.Target1 == v.repEntity) { targetingIcon = 1; }
            SetVisible(v.targetedIcon1, targetingIcon == 1);
            SetVisible(v.targetedIcon2, targetingIcon == 2);
        }
    }

}
