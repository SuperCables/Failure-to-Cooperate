using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScopeUI : MonoBehaviour
{

    public Entity player;
    public WeaponManager weaponManager; //for highlighting priority targets, not for fireing arcs

    public List<WeaponScopeBlipUI> blips = new List<WeaponScopeBlipUI>();

    public Vector2 mousePos;
    GlobalStation globalStation;
    float scale = 0; //smooth damp to target

    [Header("Assignment")]
    public WeaponScopeBlipUI blipTemplate;
    public Transform trUnits;

    //TODO: find these automaticly!
    public WeaponBank weaponBank;

    void Start()
    {
        globalStation = GetComponentInParent<GlobalStation>();
        Game.global.UnitAdded += AddBlip;
        Game.global.UnitRemoved += RemoveBlip;
        Game.global.UnitSelected += SelectUnit;
        scale = 1;
        Refresh();
    }

    void OnEnable() //every time this station is enabled
    {
        print("Scope Enabled, Updating");
        Refresh(); //refresh to make sure we have all the correct blips
    }

    void Update()
    {
        player = Game.global?.entity;
        if (player != null)
        {
            UpdateInput();

            UpdateBlips();
        }
    }

    void UpdateInput()
    {

        Ray ray = globalStation.StationCam.ScreenPointToRay(Input.mousePosition);
        Plane interfacePlane = new Plane(-trUnits.forward, trUnits.position); //test mouse pos
        float rayDistance;

        if (interfacePlane.Raycast(ray, out rayDistance)) //if we hit
        {
            Vector3 mouseHitPoint = ray.GetPoint(rayDistance); //find mouse pos
            mousePos = (Vector2)trUnits.InverseTransformPoint(mouseHitPoint); //and localize it
            float mDir = 90 - Game.Vector2ToDegree(mousePos);
            float mDis = mousePos.magnitude / 540; //constant needs variable
            bool mouseOver = (mDis < 1);
            //print(mDis);

        }
    }
    

    void UpdateBlips()
    {
        weaponManager = Game.global?.weaponManager;

        bool dirty = false; //if set to true, we need to refresh
        foreach (WeaponScopeBlipUI v in blips)
        {
            if (v.repEntity == player) { dirty = true; } //because this scope is in first person, we dont want to see our selves on the projection. (so flag for refresh)

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
        
        if (dirty)
        {
            Refresh();
        }

    }

    void AddBlip(Entity unit)
    {
        if (unit != player)
        {
            WeaponScopeBlipUI go = Instantiate(blipTemplate);
            go.repEntity = unit;
            go.transform.SetParent(trUnits, false);
            go.radar = this;
            blips.Add(go);
        }
    }

    void RemoveBlip(Entity unit)
    {
        //find assocateed blip
        //remove it from list
        //destroy it
    }

    void Refresh()
    {
        player = Game.global?.entity;
        blips.Clear(); //clean blips list
        foreach (Transform v in trUnits) //clean actual blips
        {
            Destroy(v.gameObject);
        }

        foreach (Entity v in Game.global.allUnits) //for each item to add
        {
            if (v != player) //because this scope is in first person, we dont want to see our selves on the projection.
            {
                AddBlip(v);
            }
        }
    }

    public void SelectUnit(Entity unit)
    {
        foreach (WeaponScopeBlipUI v in blips)
        {
            if (v.repEntity == unit)
            {
                v.selected = true;
            }
            else
            {
                v.selected = false;
            }
        }
    }

    public void SetVisible(GameObject thing, bool visible)
    {
        if (thing.activeInHierarchy != visible)
        {
            thing.SetActive(visible);
        }
    }
}
