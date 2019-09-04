using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetGlobal : NetworkBehaviour
{
    public bool server;


    void Start()
    {
        server = isServer;
    }

    void Update()
    {
        
    }
}
