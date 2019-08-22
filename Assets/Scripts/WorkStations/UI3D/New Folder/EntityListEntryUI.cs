using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EntityListEntryUI : MonoBehaviour
{
    public bool selected;
    [Range(0, 2)]
    public int targetedStat = 0; //0=none, 1=icon 1, 2=icon 2
    [Space(10)]
    public Entity repEntity; //what does this icon represent
    public EntityListUI screen;

    [Header("Assignment")]
    public RectTransform rootTransform; //
    [Space(10)]
    public Image unitIcon; //info abbout the object and selection stats
    public GameObject selectedIcon;
    public GameObject targetedIcon1;
    public GameObject targetedIcon2;
    [Space(10)]
    public GameObject statsBars; //stats
    public Image shieldBar;
    public Image hullBar;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
