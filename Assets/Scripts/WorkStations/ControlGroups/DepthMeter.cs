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
        float depth = (float)Game.global?.entity.transform.position.y;
        counter.count = Mathf.Abs(depth);
    }
}
