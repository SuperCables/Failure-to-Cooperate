using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Mirror;

public class Global : MonoBehaviour
{
    public event Action<Entity> UnitAdded; //everyone add a blip to the radar
    public event Action<Entity> UnitRemoved; //remove this blip
    public event Action<Entity> UnitSelected; //from radar, or anything else

    public List<Entity> allUnits = new List<Entity>(); //everything selectable
    [Header("Global")]
    public CamScript cameraMananger;
    [Header("Player")]
    public Entity selectedUnit;
    public Entity hoveredUnit;
    public PlayerConnection localConnection;
    [Header("Refrences to ship parts")]
    public Transform trans;
    public Entity entity;
    public Vessel vessel;
    public ShipMovement moveing;
    public Health hull;
    public WeaponManager weaponManager;
    public TorpedoMananger torpedoMananger;
    
    [Header("UI")]
    public GameObject connectionHolder;
    public Canvas canvas;
    public Transform tempStuff;

    [Header("ImageLinks")]
    public Sprite[] TorpedosSprites;

    void Start () {
        cameraMananger = GetComponentInChildren<CamScript>();

        RefreshCurrentVesselLinks();
        RenderSettings.ambientLight = Color.Lerp(Color.black, Color.white, 0.3f);
        RenderSettings.ambientIntensity = 1;
    }
	
	void Update () {
        RefreshCurrentVesselLinks();
    }

    //This updates refrences to player ship components so they can be easily acsessed by the station controls.
    //will be null if no player ship or ship doesn't have that component
    void RefreshCurrentVesselLinks()
    {
        entity = localConnection?.playerEntity;
        if (entity == null) { return; }
        trans = entity?.transform;
        hull = entity?.health;
        vessel = entity?.vessel;
        moveing = vessel?.movement;
        weaponManager = vessel?.weaponManager;
        torpedoMananger = vessel?.torpedoMananger;
    }

	public void AddUnit(Entity unit) {
		allUnits.Add (unit);
        UnitAdded?.Invoke(unit);
    }

	public void RemoveUnit(Entity unit) {
		allUnits.Remove (unit);
		UnitRemoved?.Invoke(unit);
	}

    public void SelectUnit(Entity unit)
    {
        selectedUnit = unit;
        UnitSelected?.Invoke(unit);
    }

    //FIXME Should be moved to different class?
    public Sprite GetTorpedoIcon(TorpedoType tp)
    {
        int index = (int)tp;
        if (index > 0)
        {
            return TorpedosSprites[index-1];
        }
        else
        {
            return null;
        }
    }

}
