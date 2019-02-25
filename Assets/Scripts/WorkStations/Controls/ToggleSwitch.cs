using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleSwitch : MonoBehaviour, IClickable
{
    public Material matOn;
    public Material matOff;

    public Transform handle;

    public MeshRenderer hinge;
    public MeshRenderer lever;

    [HideInInspector]
    public int index = 0;
    [HideInInspector]
    public bool changedState = false;
    int lastIndex = 0;

    float currentAngle = 0;
    float smoothDampSpeed;

    void Start()
    {

    }

    void Update()
    {
        if (lastIndex != index)
        {
            lastIndex = index;
            if (matOn != null && matOff != null)
            {
                Material mat = (index > 0) ? matOn : matOff;
                hinge.material = mat;
                lever.material = mat;
            }
        }

        float tarAngle = -20 + index * 40;
        currentAngle = Mathf.SmoothDampAngle(currentAngle, tarAngle, ref smoothDampSpeed, 0.1f);
        handle.transform.localRotation = Quaternion.Euler(currentAngle, 0, 0);
    }

    void IClickable.LeftClick(Vector3 pos)
    {
        index = 1 - index;
        changedState = true;
    }

    void IClickable.RightClick(Vector3 pos)
    {
        index = 1 - index;
        changedState = true;
    }

}
