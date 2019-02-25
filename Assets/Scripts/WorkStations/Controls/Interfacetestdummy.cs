using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interfacetestdummy : MonoBehaviour
{
    public ControlKnob[] controlKnobs;
    public MechCount count;
    public MechCount countTime;
    public MechCount countTimeInt;

    void Start()
    {
        
    }

    void Update()
    {
        float number = 0;
        float i = 1;
        foreach (ControlKnob v in controlKnobs)
        {
            number += (v.index * i);
            i *= 10;
        }
        count.count = number;

        countTime.count = Time.fixedUnscaledTime;
        countTimeInt.count = Mathf.CeilToInt(Time.fixedUnscaledTime);
    }
}
