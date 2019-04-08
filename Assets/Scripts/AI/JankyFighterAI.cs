using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JankyFighterAI : MonoBehaviour
{
    [Header("Assignment")]
    public Vessel body;
    public Vessel target;

    [Header("Self Assign")]
    public ShipMovement move;

    void Start()
    {
        

    }

    void Update()
    {
        
        move = body.movement;
        Vector3 delta = Game.V3toV2(body.transform.position - target.transform.position);
        float angle = Game.Vector2ToDegree(delta);
        move.inputAngle = angle;
        move.inputThrottle = 1;
    }
}
