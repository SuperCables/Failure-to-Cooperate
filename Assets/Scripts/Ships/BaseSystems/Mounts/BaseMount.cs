using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMount : MonoBehaviour
{

    [Header("Self Assign")]
    public HullRoom room;

    public virtual void Start()
    {
        room = GetComponentInParent<HullRoom>();
    }

    public virtual void Update()
    {

    }
}
