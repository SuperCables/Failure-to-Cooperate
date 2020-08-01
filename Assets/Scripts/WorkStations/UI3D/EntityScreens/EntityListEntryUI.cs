using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EntityListEntryUI : BaseEntityBlipUI
{

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
    }

    public void Clicked()
    {
        G.global.SelectUnit(repEntity);
    }
}
