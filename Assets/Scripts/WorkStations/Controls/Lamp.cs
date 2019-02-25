using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour
{
    public bool on;
    bool wasOn;

    public MeshRenderer lamp;
    public Material matOn;
    public Material matOff;

    void Start()
    {
        Refresh();
    }

    void Update()
    {
        if (on != wasOn)
        {
            Refresh();
        }
        if (Random.value < 0.01f)
        {
            on = (Random.value > 0.5f);
        }
    }

    void Refresh()
    {
        wasOn = on;
        lamp.material = (on) ? matOn : matOff;
    }
}
