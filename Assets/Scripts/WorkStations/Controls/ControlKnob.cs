using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class ControlKnob : MonoBehaviour, IClickable, IScrollable
{
    public event Action<int> Changed;

    public float[] values = { 0, 90, 180, 200 };
    public bool loop;
    public int index = 0;
    [Space(10)]
    public Transform knob;
    public Transform labels;

    float currentAngle = 0;
    float smoothDampSpeed;

    void Start()
    {

    }

    void Update()
    {
        float tarAngle = values[index];
        if (loop)
        {
            currentAngle = Mathf.SmoothDampAngle(currentAngle, tarAngle, ref smoothDampSpeed, 0.1f, 500, Time.deltaTime);
        }
        else
        {
            currentAngle = Mathf.SmoothDamp(currentAngle, tarAngle, ref smoothDampSpeed, 0.1f, 1000, Time.deltaTime);
        }
        knob.transform.localRotation = Quaternion.Euler(0, 0, -currentAngle);
    }

    void Change(int delta)
    {
        index += delta;
        if (index >= values.Length)
        {
            if (loop)
            {
                index = 0;
            }
            else
            {
                index = values.Length - 1;
            }

        }

        if (index < 0)
        {
            if (loop)
            {
                index = values.Length - 1;
            }
            else
            {
                index = 0;
            }

        }
        Changed?.Invoke(index);
    }

    void IClickable.LeftClick(Vector3 pos)
    {
        Change(1);
    }

    void IClickable.RightClick(Vector3 pos)
    {
        Change(-1);
    }

    void IScrollable.Scroll(int ammount)
    {
        Change(ammount);
    }

#if UNITY_EDITOR
    [ContextMenu("Set Label Angles")]
    public void SetLabelAngles()
    {

        Undo.RegisterCompleteObjectUndo(transform.gameObject, "Set Knob Label Angles");
        for (int i = 0; i < values.Length; i++)
        {
            Transform lbl = labels.GetChild(i);
            if (lbl != null)
            {
                lbl.localRotation = Quaternion.Euler(0, 0, -values[i]);
                lbl.GetChild(0).localRotation = Quaternion.Euler(180, 180, values[i] - 180);
            }
        }
    }
#endif


}
