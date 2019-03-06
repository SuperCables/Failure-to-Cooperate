using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TorpedoTubeControlRow : MonoBehaviour
{

    public bool selected;

    public TorpedoTube torpedoTube;

    [Header("Assignment")]
    public ToggleButton SelectBtn;
    public PushButton LoadBtn;
    public PushButton FireBtn;
    public ToggleSwitch AutoLoadSwitch;
    public ToggleSwitch LinkedFireSwitch;

    [Header("Self Assign")]
    public int index = -1; //assigned by TorpedoControlUI in Start
    public TorpedoTubeDisplayUI display;
    public TorpedoControlUI torpedoControlUI;

    void Start()
    {
        torpedoControlUI = GetComponentInParent<TorpedoControlUI>();
        LoadBtn.Pressed += Load;
        FireBtn.Pressed += Fire;
        SelectBtn.Toggled += Select;
    }

    void Update()
    {

    }

    void Load()
    {
        if (torpedoTube != null)
        {
            torpedoTube.CmdLoad();
        }
    }

    void Fire()
    {
        if (torpedoTube != null)
        {
            torpedoTube.CmdFire();
        }
    }

    void Select(bool stat)
    {
        selected = stat;
        if (display != null) //if that display exist
        {
            display.selected = selected;
        }
    }

}
