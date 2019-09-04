using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TargetViewSetting
{
    Custom,
    CurrentShip,
    Target1,
    Target2,
    Selected
}

public class TargetInfoUI : MonoBehaviour
{
    public TargetViewSetting viewSetting;
    public Entity display;

    QuadrentBarUI[] shieldsBars;
    QuadrentBarUI[] armorBars;

    VesselHull hull;
    Entity repEntity;

    [Header("Assignment")]
    public Transform ShieldQuadsHolder;
    public Transform ArmorQuadsHolder;

    public QuadrentBarUI ShieldTemplate;
    public QuadrentBarUI ArmorTemplate;

    public Transform VesselHolder;

    void Start()
    {
        shieldsBars = new QuadrentBarUI[0]; //clean array
        armorBars = new QuadrentBarUI[0];
    }

    void Update()
    {
        CheckNewTarget();
        Refresh();
    }

    void Refresh()
    {
        if (hull != null)
        {
            float angle = repEntity.transform.eulerAngles.y;
            VesselHolder.localRotation = Quaternion.Euler(0, 0, angle - 90);

            for (int i = 0; i < hull.armorSegments; i++)
            {
                armorBars[i].front.fillAmount = hull.armor[i] / hull.maxArmor;
            }

            for (int i = 0; i < hull.shieldsSegments; i++)
            {
                shieldsBars[i].front.fillAmount = hull.shields[i] / hull.maxShields;
            }
        }
    }
    
    void CheckNewTarget()
    {
        if (viewSetting == TargetViewSetting.CurrentShip)
        {
            display = InGame.global?.localConnection?.playerEntity;
        }

        if (viewSetting == TargetViewSetting.Target1)
        {
            display = InGame.global?.localConnection?.playerEntity?.vessel?.weaponManager?.Target1;
        }

        if (viewSetting == TargetViewSetting.Target2)
        {
            display = InGame.global?.localConnection?.playerEntity?.vessel?.weaponManager?.Target2;
        }

        if (display != null)
        {
            if (display != repEntity)
            {
                ChangeTarget(display);
            }
        }
    }

    void ChangeTarget(Entity newTarget)
    {
        repEntity = newTarget;
        display = repEntity;
        hull = newTarget?.hull;
        //clean last round

        foreach (Transform child in ShieldQuadsHolder)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in ArmorQuadsHolder)
        {
            Destroy(child.gameObject);
        }

        if (hull != null)
        {
            //print("Refresh Target Info");
            shieldsBars = new QuadrentBarUI[hull.shieldsSegments];
            armorBars = new QuadrentBarUI[hull.armorSegments];

            for (int i = 0; i < hull.armorSegments; i++)
            {
                float angle = i * 360 / hull.armorSegments;

                QuadrentBarUI go = Instantiate(ArmorTemplate);
                go.transform.SetParent(ArmorQuadsHolder, false);
                go.transform.localRotation = Quaternion.Euler(0, 0, angle - 90);
                armorBars[i] = go;
            }

            for (int i = 0; i < hull.shieldsSegments; i++)
            {
                float angle = i * 360 / hull.shieldsSegments;

                QuadrentBarUI go = Instantiate(ShieldTemplate);
                go.transform.SetParent(ShieldQuadsHolder, false);
                go.transform.localRotation = Quaternion.Euler(0, 0, angle - 90);
                shieldsBars[i] = go;
            }

        }
        else
        {
            shieldsBars = new QuadrentBarUI[0]; //clean array
            armorBars = new QuadrentBarUI[0];
        }
    }
}
