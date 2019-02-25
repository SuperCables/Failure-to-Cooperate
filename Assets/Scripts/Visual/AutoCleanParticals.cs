using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoCleanParticals : MonoBehaviour
{

    ParticleSystem ps;
    void Start()
    {
        ps = GetComponentInChildren<ParticleSystem>();
    }

    void Update()
    {
        if (ps != null)
        {
            if (!ps.IsAlive())
            {
                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
