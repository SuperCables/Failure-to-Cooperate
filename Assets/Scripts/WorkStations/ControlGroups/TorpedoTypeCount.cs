using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TorpedoTypeCount : MonoBehaviour
{



    [Space(10)]
    public TorpedoType torpedoType;

    [Header("Assignment")]
    public MechCount Count;
    public PushButton LoadButton;
    public SpriteRenderer Icon;
    [Header("Self Assign")]
    public TorpedoControlUI torpedoControlUI;

    void Start()
    {
        Icon.sprite = Game.global.GetTorpedoIcon(torpedoType);
        torpedoControlUI = GetComponentInParent<TorpedoControlUI>();
        LoadButton.Pressed += Clicked;
    }

    void Update()
    {
        
    }

    void Clicked()
    {
        torpedoControlUI.AppendToLoadOut(torpedoType);
    }
}
