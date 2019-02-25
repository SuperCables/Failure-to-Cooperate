using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PushButton : MonoBehaviour, IClickable
{

    public event Action Pressed;

    public float holdTime = 0.4f;
    public float pressMoveAmmount = 0.03f;

    public Material matOn;
    public Material matOff;

    public Transform handle;
    public MeshRenderer lever;

    [HideInInspector]
    public int index = 0;
    [HideInInspector]
    public bool changedState = false;

    float outPos;
    float currentPos = 0;
    float smoothDampSpeed;
    float currentTime = 0;

    void Start()
    {
        outPos = handle.transform.localPosition.z;
    }

    void Update()
    {
        if (currentTime > -3)
        {
            float tarPos = outPos - index * pressMoveAmmount;
            currentPos = Mathf.SmoothDamp(currentPos, tarPos, ref smoothDampSpeed, 0.07f);
            handle.transform.localPosition = currentPos * Vector3.forward;

            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                index = 0;
                if (matOff != null)
                {
                    lever.material = matOff;
                }
            }
        }

       
    }

    void FlipOn()
    {
        index = 1;
        changedState = true;
        Pressed?.Invoke();
        currentTime = holdTime;
        if (matOn != null)
        {
            lever.material = matOn;
        }
    }

    void IClickable.LeftClick(Vector3 pos)
    {
        FlipOn();
    }

    void IClickable.RightClick(Vector3 pos)
    {
        FlipOn();
    }
}
