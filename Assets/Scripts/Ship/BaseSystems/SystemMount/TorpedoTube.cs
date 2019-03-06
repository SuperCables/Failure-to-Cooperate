using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public enum TorpedoType { none, type1, type2, type3, type4, type5 }

public class TorpedoTube : NetworkBehaviour
{
    public float loadTime = 4;
    public int queueLength;

    //NOTE:
    // load / loading -> loading the tube from the clip
    // reload / reloading -> reloading the clip from storage

    public TorpedoType[] clip; //what is loaded
    public TorpedoType[] targetClip; //what are we tring to load
    public TorpedoType loading = TorpedoType.none; //what torpedo is being inserted in to tube
    public TorpedoType loaded = TorpedoType.none; //what is ready to fire

    public bool locked; //storage -> clip (we cant do anything while reloading)
    public float loadTimeRemaining; //clip -> tube (how long till the torpedo is in the tube)

    [Header("Assignment")]

    [Header("Self Assign")]
    public TorpedoArray torpedoArray;

    void Start()
    {
        GetComponentInParent<Entity>().FullRebuild += Rebuild;
        Rebuild();

        //clip = new TorpedoType[queueLength];
        loadTimeRemaining = -1;
    }

    void Rebuild()
    {
        torpedoArray = GetComponentInParent<TorpedoArray>();
    }

    void Update()
    {
        if (loadTimeRemaining > 0) //if still loading
        {
            loadTimeRemaining -= Time.deltaTime; //load
            if (loadTimeRemaining <= 0) //if done loading
            {
                loaded = loading; //finish loading
                loading = TorpedoType.none;
                loadTimeRemaining = -1;
            }
        }
    }

    void Load() //don't call, use the command
    {
        //if not loading, not reloading, and has a shot ready to load, and not loaded
        if ((loadTimeRemaining < 0) && (locked == false) && (clip[0] != TorpedoType.none) && (loaded == TorpedoType.none))
        {
            loading = clip[0]; //begin loading
            clip[0] = TorpedoType.none;
            loadTimeRemaining = loadTime;

            //slide all ammo in the clip down a notch
            for (int i = 1; i < queueLength; i++)
            {
                clip[i - 1] = clip[i];
            }
            clip[queueLength - 1] = TorpedoType.none;
        }
    }

    void Reload(TorpedoType[] ls) //don't call, use the command
    {
        if (!torpedoArray.reloadAgenda.Contains(this))
        {
            print("Add to array que");
            torpedoArray.reloadAgenda.Add(this);
            locked = true;
        }
        else
        {
            print("Already Reloading");
        }
        
        
    }

    void Fire() //don't call, use the command
    {
        if (loaded != TorpedoType.none)
        {
            loaded = TorpedoType.none;
            print("BLAM!");
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Vector3 direction = transform.TransformDirection(Vector3.forward) * .2f;
        Gizmos.DrawRay(transform.position, direction);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(.02f, .02f, .1f));
        //Gizmos.DrawSphere(transform.position, .1f);
    }

    [Command]
    public void CmdReload(TorpedoType[] ls)
    {
        Reload(ls);
    }

    [Command]
    public void CmdLoad()
    {
        Load();
    }

    [Command]
    public void CmdFire()
    {
        Fire();
    }
}
