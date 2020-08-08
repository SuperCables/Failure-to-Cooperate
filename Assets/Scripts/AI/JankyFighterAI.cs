using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JankyFighterAI : MonoBehaviour
{
    [Header("Assignment")]
    public Distributer body;
    public Distributer target;
    [Space(10)]
    public float turnMin = 4;
    public float turnMax = 8;

    [Header("Self Assign")]
    public ShipMovement move;

    public bool turnAway = false;

    void Start()
    {
        

    }

    void Update()
    {
        if (body == null) { return; } //if we arn't controling anyone, bail.
        if (target == null) { return; } //if we arn't doing anything, bail.
        if (move == null) {
            if (body?.entity?.movement == null) { return; }
            move = body.entity.movement;
        }

            Vector2 delta = G.V3toV2(body.transform.position - target.transform.position);
        
        float angle = -G.Vector2ToDegree(delta) - 90;

        float dist = delta.SqrMagnitude();
        if (dist > turnMax * turnMax) { turnAway = false; }
        if (dist < turnMin * turnMin) { turnAway = true; }
        if (turnAway) { angle += 180; }

        
        
        move.inputAngle = angle;
        move.inputThrottle = 1;
    }
}
