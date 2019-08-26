using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScopeUI : BaseEntityScreenUI
{

    //TODO: find these automaticly!
    public WeaponBank weaponBank;

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
            UpdateInput();

            UpdateBlips();
        }
    }

    protected override void MouseOver(Vector2 mousePos, float mDir, float mDis)
    {

    }

    void UpdateInput()
    {
        Ray ray = globalStation.StationCam.ScreenPointToRay(Input.mousePosition);
        Plane interfacePlane = new Plane(-entrysTransform.forward, entrysTransform.position); //test mouse pos
        float rayDistance;

        if (interfacePlane.Raycast(ray, out rayDistance)) //if we hit
        {
            Vector3 mouseHitPoint = ray.GetPoint(rayDistance); //find mouse pos
            mousePos = (Vector2)entrysTransform.InverseTransformPoint(mouseHitPoint); //and localize it
            float mDir = 90 - Game.Vector2ToDegree(mousePos);
            float mDis = mousePos.magnitude / 540; //constant needs variable
            bool mouseOver = (mDis < 1);
            //print(mDis);
        }
    }
    
    void UpdateBlips()
    {
        weaponManager = Game.global?.weaponManager;

        foreach (WeaponScopeBlipUI v in blips)
        {
            Vector3 pos = weaponBank.transform.position;
            Vector3 targetPos = v.repEntity.transform.position;

            //TODO: Lead target here!
            Vector3 leadPoint = targetPos;

            Quaternion aimAngle = Quaternion.Inverse(weaponBank.transform.rotation) * Quaternion.LookRotation(leadPoint - pos, Vector3.up);
            Vector3 aimEuler = aimAngle.eulerAngles;

            
            float fracX = Mathf.DeltaAngle(0, aimEuler.y) / (weaponBank.aimArc / 2); //yes the X and Y are swaped
            float fracY = Mathf.DeltaAngle(0, aimEuler.x) / (weaponBank.aimArc / 2); //don't change it

            float absX = Mathf.Abs(fracX);
            float absY = Mathf.Abs(fracY);

            //this is wrong!
            //float aimX = ((absX < 1) ? absX : (absX / (absX + 1))*2 ) * 64 * Mathf.Sign(fracX);
            //float aimY = ((absY < 1) ? absY : (absY / (absY + 1))*2 ) * 64 * Mathf.Sign(fracY);

            float aimX = fracX * 64;
            float aimY = fracY * 64;

            //print(fracX + "  " + aimX);
            v.rootTransform.localPosition = new Vector3(-aimX, -aimY, 0);

            //Update Blip Propertys
            int targetingIcon = 0; //start with no icon
            if (weaponManager?.Target2 == v.repEntity) { targetingIcon = 2; }
            if (weaponManager?.Target1 == v.repEntity) { targetingIcon = 1; }
            SetVisible(v.targetedIcon1, targetingIcon == 1);
            SetVisible(v.targetedIcon2, targetingIcon == 2);

        }
    }

}
