﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;

public class TurretArcUI : MonoBehaviour
{
    public RadarScreenUI radar;
    //public BaseWeapon repWeapon;
    public WeaponArray repArray;

    public float length = 50;
    [Range(0, 360)]
    public float angle = 45;
    [Range(0,1)]
    public float curvePos = 0.9f;

    public Vector2 correctionOffset = new Vector2(0.5f,0.5f);

    public UILineRenderer arms;
    public UILineRenderer curve;

    void Start()
    {
        
    }

    void Update()
    {
        length = repArray.maxRange * radar.scale;
        angle = repArray.aimArc;
        Refresh();
    }

    public void Refresh()
    {
        if (angle < 0) { angle = 0; }
        float halfAngle = angle / 2;
        Vector2 left = Game.DegreeToVector2(-halfAngle, length);
        Vector2 right = Game.DegreeToVector2(halfAngle, length);

        Vector2[] armPoints = {left + correctionOffset, Vector2.zero + correctionOffset, Vector2.zero + correctionOffset, right + correctionOffset };
        int pointCount = (int)(8+(angle / 10));
        Vector2[] arcPoints = new Vector2[pointCount];

        for (int i = 0; i < pointCount; i++)
        {
            arcPoints[i] = Game.DegreeToVector2(Mathf.Lerp(-halfAngle, halfAngle, i/ (pointCount - 1f)), length * curvePos) + correctionOffset;
        }

        arms.Points = armPoints;
        curve.Points = arcPoints;
    }
}
