using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenSwitcherUI : MonoBehaviour
{
    public GameObject[] Stations;
    Dropdown dropDown;

    void Start()
    {

        dropDown = GetComponent<Dropdown>();

        foreach (GameObject v in Stations)
        {
            v.SetActive(false);
        }

        G.global.cameraMananger.MainCam.enabled = true;
        G.global.cameraMananger.RadarCam.enabled = false;
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
            G.global.cameraMananger.MainCam.enabled = true;
            G.global.cameraMananger.RadarCam.enabled = false;
        }
        else
        {
            G.global.cameraMananger.MainCam.enabled = false;
            G.global.cameraMananger.RadarCam.enabled = true;
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
