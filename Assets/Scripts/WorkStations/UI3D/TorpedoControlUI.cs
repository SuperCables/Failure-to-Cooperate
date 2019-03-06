using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TorpedoControlUI : MonoBehaviour
{
    public TorpedoMananger torpedoMananger;
    TorpedoMananger lastTorpedoMananger;

    public List<TorpedoType> loadOut = new List<TorpedoType>(); //the desired clip to be loaded

    [Header("Template")]
    public TorpedoTubeDisplayUI screenTubeTemplate;
    [Header("Assignment")]
    public PushButton ReloadBtn;
    public Transform Screen;
    public TorpedoTubeControlRow[] ControlRows;
    public TorpedoTypeCount[] CountRows;

    [Header("Self Assigning")]
    public TorpedoTubeDisplayUI[] displayRows;

    void Start()
    {
        NewScreen();
        ReloadBtn.Pressed += Reload;
    }

    void NewScreen()
    {
        foreach (Transform child in Screen)
        {
            Destroy(child.gameObject);
        }

        TorpedoTube[] tubes = torpedoMananger.GetComponentsInChildren<TorpedoTube>(); //get all the tubes
        displayRows = new TorpedoTubeDisplayUI[tubes.Length];

        for (int i = 0; i < tubes.Length; i++) //align display rows and control rows
        {
            TorpedoTubeDisplayUI go = Instantiate(screenTubeTemplate);
            go.transform.SetParent(Screen, false);
            go.index = i;
            go.torpedoTube = tubes[i];
            displayRows[i] = go;
            ControlRows[i].index = i;
            ControlRows[i].display = go;
            ControlRows[i].torpedoTube = tubes[i];
        }
    }

    void Update()
    {
        if (torpedoMananger != lastTorpedoMananger && torpedoMananger.gameObject.activeInHierarchy)
        {
            lastTorpedoMananger = torpedoMananger;
            NewScreen();
        }
    }

    public void AppendToLoadOut(TorpedoType tp)
    {
        loadOut.Add(tp);
    }

    void Reload()
    {
        for (int i = 0; i < displayRows.Length; i++) //align display rows and control rows
        {
            if (ControlRows[i].selected) {
                ControlRows[i].SelectBtn.state = false; //flip off button
                ControlRows[i].selected = false;
                displayRows[i].selected = false;

                TorpedoTube tube = displayRows[i].torpedoTube;
                int clipSize = tube.clip.Length;
                tube.targetClip = new TorpedoType[clipSize];

                int min = Mathf.Min(clipSize, loadOut.Count);
                for (int j = 0; j < min; j++)
                {
                    tube.targetClip[j] = loadOut[j];
                }
                tube.CmdReload(tube.targetClip);
            }
        }
        loadOut.Clear();
    }
    
}
