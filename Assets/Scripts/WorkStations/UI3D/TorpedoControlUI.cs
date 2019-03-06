using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorpedoControlUI : MonoBehaviour
{

    public List<TorpedoType> loadOut = new List<TorpedoType>(); //the desired clip to be loaded

    [Header("Assignment")]
    public TorpedoTubeControlRow[] ControlRows;
    public TorpedoTypeCount[] CountRows;

    [Header("Self Assigning")]
    public TorpedoTubeDisplayUI[] displayRows;

    void Start()
    {
        for (int i = 0; i < displayRows.Length; i++) //align display rows and control rows
        {
            ControlRows[i].index = i;
            ControlRows[i].display = displayRows[i];
        }
    }

    void Update()
    {
        
    }

    public void AppendToLoadOut(TorpedoType tp)
    {
        loadOut.Add(tp);
    }
}
