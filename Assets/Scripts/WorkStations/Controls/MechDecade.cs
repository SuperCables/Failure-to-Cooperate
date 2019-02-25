using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechDecade : MonoBehaviour
{
    public float count;

    public Transform[] rotors;
    public PushButton[] upButtons;
    public PushButton[] downButtons;

    float[] angles;

    void Start()
    {
        angles = new float[rotors.Length];
    }

    void Update()
    {
        UpdateInput();

        Refresh();
    }

    void UpdateInput()
    {
        int worth = 1;
        foreach (PushButton v in upButtons)
        {
            if (v.changedState)
            {
                v.changedState = false;
                count += worth;
            }
            worth *= 10;
        }
        worth = 1;
        foreach (PushButton v in downButtons)
        {
            if (v.changedState)
            {
                v.changedState = false;
                count -= worth;
            }
            worth *= 10;
        }
        if (count < 0) { count = 0; }
    }

    void Refresh()
    {

        //float prev = 0; //for storing to last value to see if the next needs rollover
        for (int i = 0; i < rotors.Length; i++) //loop through the rotors
        {
            Transform disk = rotors[i];
            float angle = angles[i];
            float target = 36 * GetDigit(count, i);

            float spinRate = 150;
            if (Mathf.DeltaAngle(angle, target) > 64)
            {
                spinRate = 360;
            }
            angle = Mathf.MoveTowardsAngle(angle, target, spinRate * Time.deltaTime);

            disk.localRotation = Quaternion.Euler(angle, 0, 90);
            angles[i] = angle;
        }
    }

    float GetDigit(float number, int digit)
    {
        return Mathf.Floor((number / Mathf.Pow(10, digit)) % 10);
    }
}
