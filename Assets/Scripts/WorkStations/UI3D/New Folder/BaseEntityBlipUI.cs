using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BaseEntityBlipUI : MonoBehaviour
{ //an object on the radar


    public bool selected;
    [Range(0, 2)]
    public int targetedStat = 0; //0=none, 1=icon1, 2=icon2
    [Space(10)]
    public Entity repEntity; //what does this entry represent
    public BaseEntityScreenUI screen;

    public List<TurretArcUI> arcs = new List<TurretArcUI>();

    [Header("Assignment")]
    public RectTransform rootTransform;
    [Space(10)]
    public Image unitIcon; //info abbout the object and selection stats
    public GameObject selectedIcon;
    public GameObject targetedIcon1;
    public GameObject targetedIcon2;
    public TextMeshProUGUI title;
    [Space(10)]
    public GameObject statsBars; //curved bars on the side
    public Image shieldBar;
    public Image hullBar;
    [Space(10)]
    public GameObject statsIcons; //
    public Image statLeft;
    public Image statMid;
    public Image statRight;


    void Start()
    {
        
    }


    void Update()
    {

    }

    void UpdateInput()
    {

    }

    public void SetVisible(GameObject thing, bool visible)
    {
        if (thing.activeInHierarchy != visible)
        {
            thing.SetActive(visible);
        }
    }

}


