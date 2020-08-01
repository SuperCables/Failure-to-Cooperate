using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawnEvil : MonoBehaviour
{
    public PushButton button;
    void Start()
    {
        button.Pressed += Summon;
    }

    void Update()
    {
        
    }

    public void Summon()
    {
        G.global?.localConnection?.CmdSummonEvil();
    }
}
