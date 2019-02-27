using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiveThrottle : MonoBehaviour, IClickHoldable, IScrollable
{
    public float throttle = .5f;

    [Header("Assignment")]
    public Transform handle;
    public Transform arrow;

    float handleTargetAngle = 0;
    float arrowTargetAngle = 0;
    float handleAngle = 0;
    float arrowAngle = 0;
    float smoothDampSpeedHandle = 0;
    float smoothDampSpeedArrow = 0;

    ShipMovement moveing;
    Rigidbody body;

    void Start()
    {
        
    }

    void Update()
    {
        UpdateGuage();

    }

    void UpdateGuage()
    {
        if (Game.global.localConnection == null) { return; }
        moveing = Game.global?.localConnection?.playerEntity?.vessel?.movement;
        body = Game.global?.localConnection?.playerEntity?.body;
        if (moveing == null) { return; } //if everything is set up, continue

        //Set Speed Arrow
        if (Mathf.Abs(moveing.inputThrottle) > 0.001f)
        {
            Vector3 vector = body.velocity;
            Vector2 hor = new Vector2(vector.x, vector.z);
            float diveAngle = Mathf.Atan2(hor.magnitude, vector.y) * Mathf.Rad2Deg;

            arrowTargetAngle = 45 + (90 * Game.Map(diveAngle, 180, 0, 0, 1));
        }
        else
        {
            arrowTargetAngle = 90;
        }

        arrowAngle = Mathf.SmoothDamp(arrowAngle, arrowTargetAngle, ref smoothDampSpeedArrow, 0.07f);

        //Set Throttle Handle
        handleTargetAngle = 45 + (90 * throttle);
        handleAngle = Mathf.SmoothDamp(handleAngle, handleTargetAngle, ref smoothDampSpeedHandle, 0.07f);

        handle.localRotation = Quaternion.Euler(handleAngle, 0, 0);
        arrow.localRotation = Quaternion.Euler(arrowAngle, 0, 0);

    }

    void Click(Vector3 pos)
    {
        float angle = 90 - (Mathf.Atan2(pos.x, pos.z) * Mathf.Rad2Deg);
        throttle = Mathf.Clamp01((angle - 45) / 90);
        SendThrottle(throttle);
    }

    void SendThrottle(float throttle)
    {
        Game.global?.localConnection?.CmdSetShipDive(Game.Map(throttle, 0, 1, 90, -90));
    }

    void IClickHoldable.LeftHold(Vector3 pos)
    {
        Click(pos);
    }

    void IClickHoldable.RightHold(Vector3 pos)
    {
        Click(pos);
    }

    void IScrollable.Scroll(int ammount)
    {
        throttle = Mathf.Clamp01(throttle + ammount / 6f);
        SendThrottle(throttle);
    }
}
