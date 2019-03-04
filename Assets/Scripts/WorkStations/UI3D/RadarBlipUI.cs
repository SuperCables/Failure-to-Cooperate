using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RadarBlipUI : MonoBehaviour { //an object on the radar


    public bool selected;
    [Range(0,2)]
    public int targetedStat = 0; //0=none, 1=icon1, 2=icon2
    [Space(10)]
    public Entity repEntity; //what does this icon represent
    public RadarScreenUI radar;
    
    public List<TurretArcUI> arcs = new List<TurretArcUI>();

    [Header("Assignment")]
    public RectTransform rootTransform; //of the icon
    [Space(10)]
    public TurretArcUI arcTemplate;
    public Transform turretArcs;
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
	public GameObject statsIcons; //damaged icons above the unit
	public Image statLeft;
	public Image statMid;
	public Image statRight;
    
    
    void Start () {
        title.text = repEntity.Title;

        foreach (WeaponArray rack in repEntity?.vessel?.weaponManager?.weaponArray)
        {
            TurretArcUI go = Instantiate(arcTemplate);
            go.transform.SetParent(turretArcs, false);
            float x = rack.transform.localPosition.y;
            float y = rack.transform.localPosition.x;
            go.transform.localPosition = new Vector3(x, y, 0) * 8;
            go.transform.localRotation = Quaternion.Euler(0, 0, rack.transform.localRotation.eulerAngles.y);
            go.repArray = rack;
            go.radar = radar;
            arcs.Add(go);
        }
    }


    void Update()
    {
        UpdateInput();
        
    }

    void UpdateInput()
    {
        Vector2 mPos = (Vector2)transform.localPosition - radar.mousePos;
        //float mDir = 90 - Game.Vector2ToDegree(mPos);
        float mDis = mPos.magnitude / 16; //constant needs variable (32/2)
        bool mouseOver = (mDis < 1); //if the mouse is over the circle

        if (mouseOver)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Game.global.SelectUnit(repEntity);
            }
        }

        SetVisible(statsBars, mouseOver || selected || targetedStat > 0);
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

