using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Global : MonoBehaviour
{
    public event Action<Entity> UnitAdded;
    public event Action<Entity> UnitRemoved;
    public event Action<Entity> UnitSelected; //from radar, or anything else

    public List<Entity> allUnits = new List<Entity>();
    [Header("Global")]
    public CamScript cameraMananger; 
    [Header("Player")]
    public Entity selectedUnit;
    public Entity hoveredUnit;
    public PlayerConnection localConnection;
    [Header("Refrences to ship parts")]
    public Entity entity;
    public Vessel vessel;
    public ShipMovement moveing;
    public VesselHull hull;
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

    void RefreshCurrentVesselLinks()
    {
        entity = localConnection?.playerEntity;
        vessel = entity?.vessel;
        moveing = vessel?.movement;
        hull = entity?.hull;
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
