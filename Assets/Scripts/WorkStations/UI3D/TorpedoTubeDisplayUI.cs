﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TorpedoTubeDisplayUI : MonoBehaviour //a single tube
{

    public TorpedoTube torpedoTube;

    TorpedoTube repEntity;

    public bool selected;

    public Color clipSelectedColor;
    public Color clipReloadColor;

    Color clipDefColor;


    [Header("Template")]
    public TorpedoClipSlotUI ClipSlotTemplate;
    [Header("Assignment")]
    public RectTransform ArrayIcon;
    public TextMeshProUGUI ArrayCount;
    [Space(10)]
    public Image LoadedIcon;
    public Image LoadingProgress;
    [Space(10)]
    public RectTransform ClipSlotParent;
    public Image ClipBack;
    [Space(10)]
    public GameObject ReloadingBox;
    public TextMeshProUGUI ReloadingCount;
    [Header("Self Assign")]
    public int index = -1;
    TorpedoControlUI torpedoControlUI;
    public TorpedoClipSlotUI[] clipSlots;
    public TorpedoMananger torpedoMananger;
    public TorpedoArray torpedoArray;



    void Start()
    {
        torpedoControlUI = GetComponentInParent<TorpedoControlUI>();
        torpedoMananger = Game.global?.torpedoMananger; //set new refs
        clipDefColor = ClipBack.color;
    }

    void NewTube()
    {
        torpedoMananger = Game.global?.torpedoMananger; //set new refs
        repEntity = torpedoTube;
        torpedoArray = torpedoTube.torpedoArray;

        int clipSize = torpedoTube.clip.Length; //init
        clipSlots = new TorpedoClipSlotUI[clipSize];

        //clean old
        foreach (Transform child in ClipSlotParent) //for each child
        {
            GameObject.Destroy(child.gameObject);
        }

        //spawn new
        for (int i = 0; i < clipSize; i++)
        {
            TorpedoClipSlotUI go = Instantiate(ClipSlotTemplate);
            go.transform.SetParent(ClipSlotParent, false);
            go.index = i;
            clipSlots[i] = go;
        }
    }

    void Update()
    {
        if (repEntity != torpedoTube)
        {
            NewTube();
        }

        if (torpedoTube != null)
        {
            if (selected)
            {
                ClipBack.color = clipSelectedColor;
                foreach (TorpedoClipSlotUI v in clipSlots)
                {
                    if (torpedoControlUI.loadOut.Count > v.index)
                    {
                        SetIcon(v.Icon, torpedoControlUI.loadOut[v.index]);
                    }
                    else
                    {
                        SetIcon(v.Icon, TorpedoType.none);
                    }
                }
            }
            else
            {
                ClipBack.color = clipDefColor;
                foreach (TorpedoClipSlotUI v in clipSlots)
                {
                    SetIcon(v.Icon, torpedoTube.clip[v.index]);
                }
            }
            

            if (torpedoTube.loadTimeRemaining > 0)
            {
                float fill = 1 - (torpedoTube.loadTimeRemaining / torpedoTube.loadTime);
                SetIcon(LoadedIcon, torpedoTube.loading);
                LoadingProgress.fillAmount = fill;

            }
            else
            {
                SetIcon(LoadedIcon, torpedoTube.loaded);
                LoadingProgress.fillAmount = 1;
            }

            if (torpedoTube.reloadTimeRemaining > 0)
            {
                SetVisible(ReloadingBox.gameObject, (torpedoTube.reloadTimeRemaining % 1 > 0.4f));    //);

                ReloadingCount.text = ((int)torpedoTube.reloadTimeRemaining).ToString();
            }
            else
            {
                SetVisible(ReloadingBox, false);
            }
        }

    }

    void SetIcon(Image LoadedIcon, TorpedoType tp)
    {
        Sprite icon = Game.global.GetTorpedoIcon(tp);
        LoadedIcon.sprite = icon;
        SetVisible(LoadedIcon.gameObject, (icon != null));
    }

    void SetVisible(GameObject thing, bool visible)
    {
        if (thing.activeInHierarchy != visible)
        {
            thing.SetActive(visible);
        }
    }
}
