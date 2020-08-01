using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponScopeBlipUI : BaseEntityBlipUI
{

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
        UpdateInput();
    }

    void UpdateInput()
    {
        Vector2 mPos = (Vector2)transform.localPosition - screen.mousePos;
        //float mDir = 90 - Game.Vector2ToDegree(mPos);
        float mDis = mPos.magnitude / 8; //constant needs variable (16/2)
        bool mouseOver = (mDis < 1); //if the mouse is over the circle

        if (mouseOver)
        {
            if (Input.GetMouseButtonDown(0))
            {
                G.global.SelectUnit(repEntity);
            }
        }

        SetVisible(statsBars, mouseOver || selected || targetedStat>0);

    }

}
