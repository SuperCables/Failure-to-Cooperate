﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEntityScreenUI : MonoBehaviour
{
    public bool showSelf = false;
    [Header("Self Assign")]
    public Entity player;
    public WeaponManager weaponManager; //for highlighting priority targets, not for fireing arcs

    public List<BaseEntityBlipUI> blips = new List<BaseEntityBlipUI>();

    public GlobalStation globalStation;

    [HideInInspector]
    public Vector2 mousePos;

    [Header("Assignment")]
    public Transform entrysTransform;
    public BaseEntityBlipUI entryTemplate;


    public virtual void Start()
    {
        player = Game.global?.entity;
        globalStation = GetComponentInParent<GlobalStation>();
        Game.global.UnitAdded += AddBlip;
        Game.global.UnitRemoved += RemoveBlip;
        Game.global.UnitSelected += SelectUnit;
        Refresh();
    }

    void OnEnable() //every time this station is enabled
    {
        Refresh(); //refresh to make sure we have all the correct blips
    }

    public virtual void Update()
    {
        player = Game.global?.entity;
        if (player != null)
        {
            ReadInput();
            UpdateIcons();
        }
    }

    void ReadInput()
    {
        Ray ray = globalStation.StationCam.ScreenPointToRay(Input.mousePosition);
        Plane interfacePlane = new Plane(-entrysTransform.forward, entrysTransform.position); //test mouse pos
        float rayDistance;

        if (interfacePlane.Raycast(ray, out rayDistance)) //if we hit
        {
            Vector3 mouseHitPoint = ray.GetPoint(rayDistance); //find mouse pos
            mousePos = (Vector2)entrysTransform.InverseTransformPoint(mouseHitPoint); //and localize it
            float mDir = 90 - Game.Vector2ToDegree(mousePos);
            float mDis = mousePos.magnitude / 540; //constand needs variable
            if (mDis < 1)
            {
                MouseOver(mousePos, mDir, mDis);
            }
        }
    }

    protected virtual void MouseOver(Vector2 mousePos, float mDir, float mDis)
    {
        //This shouldn't be called
    }

    protected void UpdateIcons()
    {
        bool dirty = false;

        weaponManager = Game.global?.weaponManager; //easy acsess to targeting info

        foreach (BaseEntityBlipUI v in blips)
        {
            if (v.repEntity == player && showSelf == false) { dirty = true; }
            //Update Blip Propertys
            int targetingIcon = 0; //start with no targeted icon
            if (weaponManager?.Target2 == v.repEntity) { targetingIcon = 2; }
            if (weaponManager?.Target1 == v.repEntity) { targetingIcon = 1; }
            SetVisible(v.targetedIcon1, targetingIcon == 1);
            SetVisible(v.targetedIcon2, targetingIcon == 2);

        }
        if (dirty) { Refresh(); }
    }

    protected void AddBlip(Entity unit)
    {
        if (unit != player || showSelf)
        {
            BaseEntityBlipUI go = Instantiate(entryTemplate);
            go.repEntity = unit;
            go.transform.SetParent(entrysTransform, false);
            go.screen = this;
            blips.Add(go);
        }
    }

    protected void RemoveBlip(Entity unit)
    {
        foreach (BaseEntityBlipUI v in blips) //find assocateed blip
        {
            if (v.repEntity == unit)
            {
                blips.Remove(v); //remove it from list
                Destroy(v.repEntity); //destroy it
            }
        }
    }

    protected void Refresh()
    {
        player = Game.global?.entity;
        blips.Clear(); //clean blips list
        foreach (Transform v in entrysTransform) //clean actual blips
        {
            Destroy(v.gameObject);
        }

        foreach (Entity v in Game.global.allUnits) //for each item to add
        {
            AddBlip(v);
        }
    }

    public void SelectUnit(Entity unit) //if the entity was selected elseware, update our side.
    {
        foreach (BaseEntityBlipUI v in blips)
        {
            if (v.repEntity == unit) //find the blip that belongs to that unit
            {
                v.selected = true;
            }
            else //unselect all others
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
