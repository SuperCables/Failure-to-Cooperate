using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TorpedoTubeControlRow : MonoBehaviour
{
    public TorpedoTube torpedoTube;

    [Header("Assignment")]
    public ToggleButton SelectBtn;
    public PushButton LoadBtn;
    public PushButton FireBtn;
    public ToggleSwitch AutoLoadSwitch;
    public ToggleSwitch LinkedFireSwitch;

    void Start()
    {
        LoadBtn.Pressed += Load;
        FireBtn.Pressed += Fire;
    }

    void Update()
    {
        
    }

    void Load()
    {

    }

    void Fire()
    {

    }

}
