using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;
//using Mirror;

public class RadarScreenUI : BaseEntityScreenUI
{
    public float ringWidth = 2;
    public bool clickToDrive;
    public bool dragable;

    [HideInInspector]
    public float scale = 0; //scale factor
    //[HideInInspector]
    public Vector2 offsetDrag;

    float tarScale = 0; //smooth damp to target
    float scrollPos = 0; //mouse pos
    float curScaleSpeed; //damping var
    int[] ringsRad = { 10, 20, 30, 40, 50, 100, 200, 300, 400, 500, 1000, 2000, 3000, 4000, 5000 };
    float radarZoomRadus;

    UICircle[] rings;

    [Header("Assignment")]
    
    public ControlKnob scaleKnob;
    public UICircle ringTemplate;
    public Transform trRings;

    public override void Start()
    {
        base.Start();
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

    public override void Update()
    {
        base.Update();
        if (player != null)
        {
            radarZoomRadus = (8 * 67.2f) / scale;
            InGame.global.cameraMananger.RadarCam.orthographicSize = radarZoomRadus;

            scale = Mathf.SmoothDamp(scale, tarScale, ref curScaleSpeed, 0.1f, 250, Time.deltaTime);

            UpdateBlips();
            UpdateRings();
        }
    }

    protected override void MouseOver(Vector2 mousePos, float mDir, float mDis)
    {
        scrollPos -= Input.mouseScrollDelta.y;
        scrollPos = Mathf.Clamp(scrollPos, -10, 100);
        tarScale = (1 / Mathf.Pow(2f, scrollPos / 10)) * 8;

        if (Input.GetMouseButton(1) && clickToDrive)
        {
            //float dedMag = (Mathf.Clamp(mDis, 0.2f, 0.7f) - .2f) * 2;
            InGame.global?.localConnection?.CmdSetShipHeading(mDir);
            //Game.global.localConnection.CmdSetShipThrust(dedMag);
        }
    }

    void UpdateBlips()
    {
        
        //Vector2 basePos = Game.V3toV2(Game.global.localConnection.ship.transform.position);
        Vector2 basePos = InGame.V3toV2(player.transform.position) + offsetDrag;
        //float baseAngle = Player.transform.rotation.eulerAngles.y;
        
        weaponManager = InGame.global?.weaponManager;

        foreach (BaseEntityBlipUI v in blips)
        {
            Transform t = v.repEntity.transform;
            RectTransform r = v.rootTransform;
            float angle = t.rotation.eulerAngles.y;
            Vector2 pos = InGame.V3toV2(v.repEntity.transform.position);

            pos = pos - basePos;
            //self.body.velocity = (Quaternion.Euler (0, angle, 0) * Game.V2toV3(velocity));

            r.localPosition = pos * scale;
            v.unitIcon.transform.localRotation = Quaternion.Euler(0, 0, -angle + 90);

        }
    }

    void UpdateRings()
    {
        for (int i = 0; i < ringsRad.Length; i++) //for each ring
        {
            float ringScale = scale * ringsRad[i] * 2; //calculate real size
            if (ringScale > 64 && ringScale - (offsetDrag.magnitude * scale * 2) < 1080) //line in view
            {
                rings[i].gameObject.SetActive(true); //show it
                rings[i].rectTransform.localPosition = -offsetDrag * scale;
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

}
