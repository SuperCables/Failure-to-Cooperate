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

        InGame.global.cameraMananger.MainCam.enabled = true;
        InGame.global.cameraMananger.RadarCam.enabled = false;
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
            InGame.global.cameraMananger.MainCam.enabled = true;
            InGame.global.cameraMananger.RadarCam.enabled = false;
        }
        else
        {
            InGame.global.cameraMananger.MainCam.enabled = false;
            InGame.global.cameraMananger.RadarCam.enabled = true;
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
