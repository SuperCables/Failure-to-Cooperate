using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ProjectileWeapon : BaseWeapon
{

    [Header("Ammo")]
    public int ammo = 2000;
    public int clipSize;

    [Header("Fireing")]
    public float cycleTime;
    public float shellSpeed;

    [Header("Assignment")]
    public Transform muzzle;
    public DirectProjectile TemplateShell;
    public GameObject fireEffectTemplate;

    float nextShot;
    float powerPerShot;

    public override void Start()
    {
        base.Start();
        powerPerShot = (wattage * cycleTime);
    }

    public override void Update()
    {
        base.Update();
        

        if (nextShot > 0)
        {
            nextShot -= Time.deltaTime;
        }
    }

    public override void HoldTrigger(Entity target) //attempts to shoot a target as if trigger held
    {//this is only called by the server, so this will never run on the client
        //is reloaded and has ammo and RPM < Inf and has power?
        while ((nextShot <= 0) && (ammo > 0) && (cycleTime > 0) && (WeaponManager.energy > powerPerShot)) 
        {
            nextShot += cycleTime;
            ammo -= 1;
            WeaponManager.energy -= powerPerShot;

            Shoot(target.gameObject);
        }
    }

    void Shoot(GameObject target)
    {//this is only called by the server, so this will never run on the client

        SpawnShell(target); //fire the server shell

        RpcClientFire(target); //tell all cliens to fire a shell

    }

    void SpawnShell(GameObject target) //called for all clients and server
    {
        FlashMuzzle();

        DirectProjectile go = Instantiate(TemplateShell);

        Vector3 pos = Mount.transform.position;
        Vector3 tarPos = target.transform.position;
        Vector3 diff = (tarPos - pos);

        go.transform.SetParent(InGame.global.tempStuff, true);

        go.timeTillImpact = (diff.magnitude / shellSpeed);
        go.isAHit = true; //TODO: Roll a die and decide if hit
        go.target = target;

        go.transform.position = pos;
    }

    void FlashMuzzle()
    {
        GameObject effect = Instantiate(fireEffectTemplate);
        effect.transform.SetParent(transform, true);
        effect.transform.position = muzzle.position;
        effect.transform.rotation = muzzle.rotation;
    }

    [ClientRpc]
    public void RpcClientFire(GameObject target)
    {
        if (!isServer) //if we arn't the server (so we won't have a shell spawned)
        {
            SpawnShell(target); //spawn our shell
        }
        
    }

    [ClientRpc]
    public void RpcClientMorph(WeaponData data) //should only be called after being created
    {
        if (!isServer) //if we arn't the server (so we arn't updated)
        {
            Morph(data); //become what the server demands
        }

    }

    public void Morph(WeaponData data) //unpack everything
    {
        title = data.title;
        wattage = data.wattage;
        thermalLoad = data.thermalLoad;
        range = data.range;
        clipSize = data.clipSize;
        cycleTime = data.cycleTime;
        shellSpeed = data.shellSpeed;
    }

}
