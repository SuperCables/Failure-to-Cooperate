using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TorpedoType { none, asdf, qwerty, zxcv }

public class TorpedoTube : MonoBehaviour
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

    void Start()
    {
        GetComponentInParent<Entity>().FullRebuild += Rebuild;
        Rebuild();

        //clip = new TorpedoType[queueLength];
        loadTimeRemaining = -1;
    }

    void Rebuild()
    {

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

    void Load()
    {
        //if not loading, not reloading, and has a shot ready to load
        if ((loadTimeRemaining < 0) && (locked == false) && (clip[0] != TorpedoType.none))
        { 
            loading = clip[0]; //begin loading
            clip[0] = TorpedoType.none;
            loadTimeRemaining = loadTime;
        }
    }

    void Fire()
    {

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
}
