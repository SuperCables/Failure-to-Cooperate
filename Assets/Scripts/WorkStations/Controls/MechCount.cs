using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechCount : MonoBehaviour
{
    public float count;
    public float smoothTime = 0.7f;

    public Transform[] rotors;

    float display = 0;
    float smoothDampSpeed = 0;

    void Start()
    {
        
    }

    void OnEnable()
    {
        display = count;
    }

    void Update()
    {
        if (count < 0) { count = 0; }
        float diff = Mathf.Abs(display - count);

        float maxCountSpeed = 250 + (diff / 1.7f);

        display = Mathf.SmoothDamp(display, count, ref smoothDampSpeed, smoothTime, maxCountSpeed);

        Refresh();
    }

    void Refresh()
    {
        float prev = 0; //for storing to last value to see if the next needs rollover
        for (int i = 0; i < rotors.Length; i++) //loop through the rotors
        {
            Transform disk = rotors[i];
            float digit = 0;
            if (i == 0) //if is the 'white' first disk
            {
                digit = display % 10; //set directly to first digit + fraction
            }
            else//the later ones
            {
                digit = GetDigit(display, i); //set to intiger digit
                if (prev > 9) { digit += prev - 9; } //if the prevous wheel is rolling over, advance this wheel (9-10 ->maps to-> 0-1)
            }

            disk.localRotation = Quaternion.Euler(36 * digit, 0, 90);
            prev = digit;
        }
    }

    float GetDigit(float number, int digit)
    {
        return Mathf.Floor((number / Mathf.Pow(10, digit)) % 10);
    }
}
