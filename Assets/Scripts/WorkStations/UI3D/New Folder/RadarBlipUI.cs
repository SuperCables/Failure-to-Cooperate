using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RadarBlipUI : BaseEntityBlipUI{ //an object on the radar
    
    public List<TurretArcUI> arcs = new List<TurretArcUI>();

    [Header("Assignment")]
    public TurretArcUI arcTemplate;
    public Transform turretArcs;
    
    void Start () {
        title.text = repEntity.Title;

        foreach (WeaponBank rack in repEntity?.vessel?.weaponManager?.weaponArray)
        {
            TurretArcUI go = Instantiate(arcTemplate);
            go.transform.SetParent(turretArcs, false);
            float x = rack.transform.localPosition.y;
            float y = rack.transform.localPosition.x;
            go.transform.localPosition = new Vector3(x, y, 0) * 8;
            go.transform.localRotation = Quaternion.Euler(0, 0, rack.transform.localRotation.eulerAngles.y);
            go.repBank = rack;
            go.radar = (RadarScreenUI)screen;
            arcs.Add(go);
        }
    }


    void Update()
    {
        UpdateInput();
        
    }

    void UpdateInput()
    {
        Vector2 mPos = (Vector2)transform.localPosition - screen.mousePos;
        //float mDir = 90 - Game.Vector2ToDegree(mPos);
        float mDis = mPos.magnitude / 16; //constant needs variable (32/2)
        bool mouseOver = (mDis < 1); //if the mouse is over the circle

        if (mouseOver)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Game.global.SelectUnit(repEntity);
            }
        }

        SetVisible(statsBars, mouseOver || selected || targetedStat > 0);
        SetVisible(selectedIcon, selected);

    }

}

