using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TorpedoTubeDisplayUI : MonoBehaviour //a single tube
{

    public TorpedoTube torpedoTube;

    TorpedoTube repEntity;

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
    [Space(10)]
    public GameObject ReloadingRoot;
    public TextMeshProUGUI ReloadingText;
    public Image ReloadingBar;
    [Header("Self Assign")]
    public TorpedoMananger torpedoMananger;
    public TorpedoClipSlotUI[] clipSlots;

    void Start()
    {
        
    }

    void NewTube()
    {
        torpedoMananger = Game.global?.torpedoMananger; //set new refs
        repEntity = torpedoTube;

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

        foreach (TorpedoClipSlotUI v in clipSlots)
        {
            TorpedoType torpedoType = torpedoTube.clip[v.index];
            v.Icon.sprite = Game.global.TorpedosSprites[(int)torpedoType];
        }

        if (torpedoTube.loadTimeRemaining > 0)
        {
            float fill = 1-(torpedoTube.loadTimeRemaining / torpedoTube.loadTime);
            TorpedoType type = torpedoTube.loading;
            LoadedIcon.sprite = Game.global.TorpedosSprites[(int)type];
            LoadingProgress.fillAmount = fill;

        }
        else
        {
            TorpedoType type = torpedoTube.loaded;
            LoadedIcon.sprite = Game.global.TorpedosSprites[(int)type];
            LoadingProgress.fillAmount = 1;
        }

        if (torpedoTube.locked)
        {
            SetVisible(ReloadingRoot, true);
            SetVisible(ReloadingText.gameObject, (Time.time % 2 > 0.8f));
            //TODO: find out if rurrently reloading, and do progress bar
        }
        else
        {
            SetVisible(ReloadingRoot, false);
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
