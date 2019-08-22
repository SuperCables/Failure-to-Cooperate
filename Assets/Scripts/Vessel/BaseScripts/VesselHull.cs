using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class VesselHull : NetworkBehaviour
{
	public float hull = 100;
	public float maxHull = 100;
    [Header("Armor")]
    public int armorSegments = 8;
    public float maxArmor = 25;
    [Header("Shields")]
    public int shieldsSegments = 16;
    public float maxShields = 50;
    [Header("Misc")]
    public float maxBoundsSize = 5; //furthest point from center (for display scaleing and such)
    public float mass = 3;

    public SyncListFloat armor;
    public SyncListFloat shields;

    [HideInInspector]
    public Entity entity;

    // Use this for initialization
    void Start ()
    {
        GetComponentInParent<Entity>().FullRebuild += Rebuild;
        Rebuild();
    }
	
    void Rebuild()
    {
        entity = GetComponentInParent<Entity>();
        armor.Clear();
        shields.Clear();

        for (int i = 0; i < armorSegments; i++)
        {
            armor.Add(maxArmor);
        }

        for (int i = 0; i < shieldsSegments; i++)
        {
            shields.Add(maxShields);
        }
    }

	// Update is called once per frame
	void Update () {

    }

    void FixedUpdate()
    {
        ShieldFlow();
    }

    public void TakeDamage (float ammount, Vector3 from, float shieldMul, float armorMul)
    {
        Vector3 localImpactPos = entity.transform.InverseTransformPoint(from); //change impact point to local space
        float angle = 90 - Game.Vector2ToDegree(Game.V3toV2(localImpactPos));
        int shieldIndex = (Mathf.RoundToInt(angle * shieldsSegments / 360) + shieldsSegments) % shieldsSegments;
        int armorIndex = (Mathf.RoundToInt(angle * armorSegments / 360) + armorSegments) % armorSegments;
        //print(shieldIndex);

        float shieldDam = Mathf.Min(ammount * shieldMul, shields[shieldIndex]);
        shields[shieldIndex] -= shieldDam;
        ammount -= shieldDam / shieldMul;

        float armorDam = Mathf.Min(ammount * armorMul, armor[armorIndex]);
        armor[armorIndex] -= armorDam;
        ammount -= armorDam / armorMul;

        hull -= ammount;
        if (hull < 0)
        {
            Distributer distributer;
            distributer = entity?.vessel?.distributer;
            
            Destroy(entity.gameObject);
        }

    }

    void ShieldFlow()
    {
        for (int i = 0; i < shieldsSegments; i++)
        {
            int o = (i + 1) % shieldsSegments;

            float differance = shields[i] - shields[o];
            float flow = differance * 0.01f;
            shields[i] -= flow;
            shields[o] += flow;

            if (shields[i] > maxShields)
            {
                shields[i] = maxShields;
            }

            if (shields[i] < 0)
            {
                shields[i] = 0;
            }
        }
    }
}
