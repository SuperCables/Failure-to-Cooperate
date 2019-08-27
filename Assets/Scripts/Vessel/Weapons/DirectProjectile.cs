using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectProjectile : MonoBehaviour
{
    public float timeTillImpact;
    public bool isAHit;
    public GameObject target;

    [Header("Assignment")]
    public GameObject impactEffectTemplate;

    bool flying = true;
    Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        if (flying) //if hasn't impacted yet
        {
            timeTillImpact -= Time.deltaTime; //wait
            if (timeTillImpact < 0) //if should impact
            {
                flying = false; //impact! (or miss)
                if (isAHit)
                {
                    Impact();
                }
            }
        }

    }

    void Impact()
    {
        if (target == null) { Destroy(gameObject); return; }
        Entity entity = target?.GetComponent<Entity>();
        if (entity == null) { Destroy(gameObject); return; }
        VesselHull hull = entity.hull;
        if (hull == null) { Destroy(gameObject); return; }
        //object is damagable

        Vector3 tarPos = target.transform.position;
        Vector3 diff = (tarPos - startPos);
        float distance = diff.magnitude;

        Ray ray = new Ray(startPos, diff);

        RaycastHit hit; //TODO: add scatter

        Vector3 impactPos = tarPos;
        float bestRange = diff.magnitude;

        foreach (Collider v in entity.colliders) //loop through all colliders
        {
            v.Raycast(ray, out hit, distance); //raycast each one
            if (hit.distance < bestRange) //if new closest point
            {
                bestRange = hit.distance;
                impactPos = hit.point; //this is our new best impact point
            }
        }


        //print("Impact: " + angle);
        hull.TakeDamage(10, impactPos, 1, 1);

        GameObject effect = Instantiate(impactEffectTemplate); //create an impact effect
        effect.transform.SetParent(target.transform, true);
        effect.transform.position = impactPos;
        effect.transform.localScale = Vector3.one * 0.3f;
        transform.position = impactPos;
        Destroy(gameObject); //destroy projectile
    }
}
