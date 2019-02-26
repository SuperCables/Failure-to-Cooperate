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
    [Header("Player")]
    public Entity selectedUnit;
    public PlayerConnection localConnection;
    [Header("Refrences to ship parts")]
    public Entity entity;
    public Vessel vessel;
    public ShipMovement moveing;
    public VesselHull hull;
    public WeaponManager weaponManager;
    [Header("UI")]
    public GameObject connectionHolder;
    public Canvas canvas;
    public Transform tempStuff;
    public Camera playerCamera;

	void Start () {
        RefreshCurrentVesselLinks();

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

}
