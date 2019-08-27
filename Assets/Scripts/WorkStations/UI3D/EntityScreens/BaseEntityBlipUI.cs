using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BaseEntityBlipUI : MonoBehaviour{

    public bool selected;
    [Range(0, 2)]
    public int targetedStat = 0; //0=none, 1=icon1, 2=icon2
    [Space(10)]
    public Entity repEntity; //what does this entry represent
    public BaseEntityScreenUI screen;

    [Header("Assignment")]
    public RectTransform rootTransform; //icon or frame
    [Space(10)]
    public Image unitIcon; //hull / type icon
    public TextMeshProUGUI title; //title text
    [Space(10)]
    public GameObject selectedIcon; //selected icons, become visible when selected.
    public GameObject targetedIcon1; //primary target
    public GameObject targetedIcon2; //secondary target
    [Space(10)]
    public GameObject statsBars; //curved bars on the side if blip. bars if list entry. Works same either way
    public Image shieldBar;
    public Image hullBar;
    [Space(10)]
    public GameObject statsIcons; //not sure how to implement these yet...
    public Image statLeft;
    public Image statMid;
    public Image statRight;

    public virtual void Start()
    {
        
        //set the basic text.
        if (title != null) { title.text = repEntity.Title; }

    }

    public virtual void Update()
    {
        SetVisible(selectedIcon, selected);
    }

    public void SetVisible(GameObject thing, bool visible)
    {
        if (thing.activeInHierarchy != visible)
        {
            thing.SetActive(visible);
        }
    }

}


