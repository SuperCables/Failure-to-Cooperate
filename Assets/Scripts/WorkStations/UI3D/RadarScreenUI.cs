using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;
//using Mirror;

public class RadarScreenUI : MonoBehaviour
{

    public Entity player;
    public WeaponManager weaponManager; //for highlighting priority targets, not for fireing arcs
    public float ringWidth = 2;
    public bool clickToDrive;
    public List<RadarBlipUI> blips = new List<RadarBlipUI>();

    [HideInInspector]
    public float scale = 0; //scale factor
    [HideInInspector]
    public Vector2 mousePos;

    GlobalStation globalStation;
    float tarScale = 0; //smooth damp to target
    float scrollPos = 0; //mouse pos
    float curScaleSpeed; //damping var
    int[] ringsRad = { 10, 20, 30, 40, 50, 100, 200, 300, 400, 500, 1000, 2000, 3000, 4000, 5000 };

    UICircle[] rings;



    [Header("Assignment")]
    
    public ControlKnob scaleKnob;
    public RadarBlipUI blipTemplate;
    public UICircle ringTemplate;
    public Transform trUnits;
    public Transform trRings;

    void Start()
    {
        globalStation = GetComponentInParent<GlobalStation>();
        Game.global.UnitAdded += AddBlip;
        Game.global.UnitRemoved += RemoveBlip;
        Game.global.UnitSelected += SelectUnit;
        if (scaleKnob != null) { scaleKnob.Changed += ScaleKnobRotated; }
        

        rings = new UICircle[ringsRad.Length]; //allocate rings
        for (int i = 0; i < ringsRad.Length; i++) //create the rings
        {
            UICircle go = Instantiate(ringTemplate, trRings);
            rings[i] = go;
        }

        tarScale = 1;

        //NetworkIdentity.AssignClientAuthority();
    }

    void OnEnable() //every time this station is enabled
    {
        print("Radar Enabled, Updating");
        Refresh(); //refresh to make sure we have all the correct blips
    }

    void Update()
    {
        player = Game.global?.entity;
        if (player != null)
        {
            UpdateInput();

            UpdateBlips();

            UpdateRings();
        }
    }

    void UpdateInput()
    {

        Ray ray = globalStation.StationCam.ScreenPointToRay(Input.mousePosition);
        Plane interfacePlane = new Plane(-trUnits.forward, trUnits.position); //test mouse pos
        float rayDistance;

        if (interfacePlane.Raycast(ray, out rayDistance)) //if we hit
        {
            Vector3 mouseHitPoint = ray.GetPoint(rayDistance); //find mouse pos
            mousePos = (Vector2)trUnits.InverseTransformPoint(mouseHitPoint); //and localize it
            float mDir = 90 - Game.Vector2ToDegree(mousePos);
            float mDis = mousePos.magnitude / 540; //constand needs variable
            bool mouseOver = (mDis < 1);
            //print(mDis);

            if (mouseOver) //if we moused over the radar
            {
                scrollPos -= Input.mouseScrollDelta.y;
                scrollPos = Mathf.Clamp(scrollPos, 0, 100);
                tarScale = (1 / Mathf.Pow(2f, scrollPos / 10)) * 8;

                

                if (Input.GetMouseButton(1) && clickToDrive)
                {
                    //float dedMag = (Mathf.Clamp(mDis, 0.2f, 0.7f) - .2f) * 2;
                    Game.global?.localConnection?.CmdSetShipHeading(mDir);
                    //Game.global.localConnection.CmdSetShipThrust(dedMag);
                }
            }
        }

        scale = Mathf.SmoothDamp(scale, tarScale, ref curScaleSpeed, 0.1f, 250, Time.deltaTime);
    }

    void UpdateBlips()
    {
        
        //Vector2 basePos = Game.V3toV2(Game.global.localConnection.ship.transform.position);
        Vector2 basePos = Game.V3toV2(player.transform.position);
        //float baseAngle = Player.transform.rotation.eulerAngles.y;
        
        weaponManager = Game.global?.weaponManager;

        foreach (RadarBlipUI v in blips)
        {
            Transform t = v.repEntity.transform;
            RectTransform r = v.rootTransform;
            float angle = t.rotation.eulerAngles.y;
            Vector2 pos = Game.V3toV2(v.repEntity.transform.position);

            pos = pos - basePos;
            //self.body.velocity = (Quaternion.Euler (0, angle, 0) * Game.V2toV3(velocity));

            r.localPosition = pos * scale;
            v.unitIcon.transform.localRotation = Quaternion.Euler(0, 0, -angle + 90);

            int targetingIcon = 0; //start with no icon
            if (weaponManager?.Target2 == v.repEntity) { targetingIcon = 2; }
            if (weaponManager?.Target1 == v.repEntity) { targetingIcon = 1; }
            SetVisible(v.targetedIcon1, targetingIcon == 1);
            SetVisible(v.targetedIcon2, targetingIcon == 2);
        }
    }

    void UpdateRings()
    {
        for (int i = 0; i < ringsRad.Length; i++) //for each ring
        {
            float ringScale = scale * ringsRad[i] * 2; //calculate real size
            if (ringScale > 64 && ringScale < 1080) //line in view
            {
                rings[i].gameObject.SetActive(true); //show it
                rings[i].rectTransform.sizeDelta = Vector2.one * ringScale; //size it
                rings[i].Thickness = ringWidth; //why is this here?
            }
            else //not in view
            {
                rings[i].gameObject.SetActive(false); //hide it
            }
        }
    }

    void ScaleKnobRotated(int value) //when the scale knob is rotated
    {
        scrollPos = value * 10; //set the scale target
        tarScale = (1 / Mathf.Pow(2f, scrollPos / 10)) * 8; //and appply
    }

    void AddBlip(Entity unit) {
		RadarBlipUI go = Instantiate (blipTemplate);
		go.repEntity = unit;
		go.transform.SetParent(trUnits, false);
        go.radar = this;
		blips.Add (go);
	}

	void RemoveBlip(Entity unit) {
        //find assocateed blip
        //remove it from list
        //destroy it
	}

    void Refresh()
    {
        blips.Clear();
        foreach (Transform v in trUnits)
        {
            Destroy(v.gameObject);
        }

        foreach (Entity v in Game.global.allUnits)
        {
            AddBlip(v);
        }
    }

    public void SelectUnit(Entity unit)
    {
        foreach (RadarBlipUI v in blips)
        {
            if (v.repEntity == unit)
            {
                v.selected = true;
            }
            else
            {
                v.selected = false;
            }
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
