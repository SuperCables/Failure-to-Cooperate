﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityListUI : BaseEntityScreenUI
{

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
        player = G.global?.entity;
        if (player != null)
        {

        }
    }

}
