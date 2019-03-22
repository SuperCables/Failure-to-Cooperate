using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthMeter : MonoBehaviour
{
    MechCount counter;

    void Start()
    {
        counter = GetComponent<MechCount>();
    }

    void Update()
    {
        Transform trans = Game.global?.entity?.transform;
        if (trans != null) { counter.count = Mathf.Abs(trans.position.y); } 
        
    }
}
