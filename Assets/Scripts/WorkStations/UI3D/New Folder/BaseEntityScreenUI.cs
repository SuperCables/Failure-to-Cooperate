using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEntityScreenUI : MonoBehaviour
{
    public Entity Player;
    public WeaponManager WeaponManager; //for highlighting priority targets, not for fireing arcs

    public List<BaseEntityBlipUI> blips = new List<BaseEntityBlipUI>();

    GlobalStation GlobalStation;

    [Header("Assignment")]
    public BaseEntityBlipUI EntryTemplate;
    public Transform EntrysTransform;

    public virtual void Start()
    {
        GlobalStation = GetComponentInParent<GlobalStation>();
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
        Player = Game.global?.entity;
        if (Player != null)
        {
            UpdateBlips();
        }
    }

    protected void UpdateBlips()
    {
        WeaponManager = Game.global?.weaponManager;

        bool dirty = false; //if set to true, we need to refresh
        foreach (BaseEntityBlipUI v in blips)
        {
            if (v.repEntity == Player) { dirty = true; } //No need to list us

            //Update Blip Propertys
            int targetingIcon = 0; //start with no icon
            if (WeaponManager?.Target2 == v.repEntity) { targetingIcon = 2; }
            if (WeaponManager?.Target1 == v.repEntity) { targetingIcon = 1; }
            SetVisible(v.targetedIcon1, targetingIcon == 1);
            SetVisible(v.targetedIcon2, targetingIcon == 2);

        }

        if (dirty)
        {
            Refresh();
        }

    }

    protected void AddBlip(Entity unit)
    {
        if (unit != Player)
        {
            BaseEntityBlipUI go = Instantiate(EntryTemplate);
            go.repEntity = unit;
            go.transform.SetParent(EntrysTransform, false);
            //go.screen = this;
            blips.Add(go);
        }
    }

    protected void RemoveBlip(Entity unit)
    {
        //find assocateed blip
        //remove it from list
        //destroy it
    }

    protected void Refresh()
    {
        Player = Game.global?.entity;
        blips.Clear(); //clean blips list
        foreach (Transform v in EntrysTransform) //clean actual blips
        {
            Destroy(v.gameObject);
        }

        foreach (Entity v in Game.global.allUnits) //for each item to add
        {
            if (v != Player) //Don't show us
            {
                AddBlip(v);
            }
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
