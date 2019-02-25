using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenSwitcherUI : MonoBehaviour
{
    public GameObject[] Stations;
    public Camera ShipCam;
    Dropdown dropDown;

    void Start()
    {
        dropDown = GetComponent<Dropdown>();

        foreach (GameObject v in Stations)
        {
            v.SetActive(false);
        }
        
        ShipCam.enabled = true;
    }

    void Update()
    {
        
    }

    public void Change()
    {
        foreach (GameObject v in Stations)
        {
            SetVisible(v, false);
        }

        int choice = dropDown.value;
        if (choice == 0)
        {
            ShipCam.enabled = true;
        }
        else
        {
            ShipCam.enabled = false;
            SetVisible(Stations[choice - 1], true); 
        }
    }

    public void SetVisible(GameObject thing, bool visible)
    {
        if (thing.activeInHierarchy != visible)
        {
            thing.SetActive(visible);
        }
    }

}
