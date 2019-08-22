using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityListUI : MonoBehaviour
{
    public Entity player;
    public WeaponManager weaponManager; //for highlighting priority targets, not for fireing arcs

    public List<EntityListEntryUI> blips = new List<EntityListEntryUI>();

    public Vector2 mousePos;
    GlobalStation globalStation;

    [Header("Assignment")]
    public EntityListEntryUI blipTemplate;
    public Transform trEntrys;

    void Start()
    {
        globalStation = GetComponentInParent<GlobalStation>();
        Game.global.UnitAdded += AddBlip;
        Game.global.UnitRemoved += RemoveBlip;
        Game.global.UnitSelected += SelectUnit;
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
            UpdateBlips();
        }
    }

    void UpdateBlips()
    {
        weaponManager = Game.global?.weaponManager;

        bool dirty = false; //if set to true, we need to refresh
        foreach (EntityListEntryUI v in blips)
        {
            if (v.repEntity == player) { dirty = true; } //No need to list us
            
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
            EntityListEntryUI go = Instantiate(blipTemplate);
            go.repEntity = unit;
            go.transform.SetParent(trEntrys, false);
            go.screen = this;
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
        foreach (Transform v in trEntrys) //clean actual blips
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
        foreach (EntityListEntryUI v in blips)
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
