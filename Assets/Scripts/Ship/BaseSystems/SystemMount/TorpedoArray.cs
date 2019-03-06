using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorpedoArray : MonoBehaviour
{
    public TorpedoTube[] tubes;
    public List<TorpedoTube> reloadAgenda = new List<TorpedoTube>();
    public float reloadTime = 10;


    public float reloadTimeRemaining;
    public TorpedoTube currentlyReloading;

    void Start()
    {

        GetComponentInParent<Entity>().FullRebuild += Rebuild;
        Rebuild();
    }

    void Rebuild()
    {
        tubes = GetComponentsInChildren<TorpedoTube>();
    }

    void Update()
    {
        if (currentlyReloading != null)
        {
            reloadTimeRemaining -= Time.deltaTime;
            if (reloadTimeRemaining <= 0)
            {
                //because we verified and removed these torpedos from storage when we began reloading
                //we don't need to verify we have them here
                currentlyReloading.clip = currentlyReloading.targetClip; //set the desired clip
                currentlyReloading.locked = false;
                currentlyReloading = null;
                reloadTimeRemaining = -1;

            }
        }
        else //we arn't reloading anything, check to see if we should start a new reload
        {
            if (reloadAgenda.Count > 0)
            {
                //TODO: verify we have the required torpedos and load them
                //possibly change targetClip to match what we can load if low ammo
                currentlyReloading = reloadAgenda[0];
                reloadAgenda.RemoveAt(0);
                reloadTimeRemaining = reloadTime;
            }
            
        }
    }

    void AddReload(TorpedoTube tube)
    {
        if (!reloadAgenda.Contains(tube))
        {
            reloadAgenda.Add(tube);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Vector3 direction = transform.TransformDirection(Vector3.forward) * 0.5f;
        Gizmos.DrawRay(transform.position, direction);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(0.5f, 0.05f, 0.01f));
    }
}
