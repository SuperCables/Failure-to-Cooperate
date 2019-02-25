using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ToggleButton : MonoBehaviour, IClickable
{
    public event Action<bool> Toggled;

    public float pressMoveAmmount = 0.03f;

    public Material matOn;
    public Material matOff;

    public Transform handle;
    public MeshRenderer lever;

    [HideInInspector]
    public bool state = false;
    bool lastState = false;

    float currentPos = 0;
    float smoothDampSpeed;
    float outPos;

    void Start()
    {
        outPos = handle.transform.localPosition.z;
    }

    void Update()
    {
        if (lastState != state)
        {
            lastState = state;
            if (matOn != null && matOff != null)
            {
                lever.material = (state) ? matOn : matOff;
            }
        }
        float tarPos = outPos - ((state) ? pressMoveAmmount : 0);
        currentPos = Mathf.SmoothDamp(currentPos, tarPos, ref smoothDampSpeed, 0.07f);
        handle.transform.localPosition = currentPos * Vector3.forward;
    }

    void IClickable.LeftClick(Vector3 pos)
    {
        state = !state;
        Toggled?.Invoke(state);
    }

    void IClickable.RightClick(Vector3 pos)
    {
        state = !state;
        Toggled?.Invoke(state);
    }
}
