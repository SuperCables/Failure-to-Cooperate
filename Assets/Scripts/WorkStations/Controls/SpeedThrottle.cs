using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedThrottle : MonoBehaviour, IClickHoldable, IScrollable
{

    public float throttle = 0;

    [Header("Assignment")]
    public Transform handle;
    public Transform arrow;

    public ToggleButton reverseBtn;

    float handleTargetAngle = 0;
    float arrowTargetAngle = 0;
    float handleAngle = 0;
    float arrowAngle = 0;
    float smoothDampSpeedHandle = 0;
    float smoothDampSpeedArrow = 0;

    float direction = 1;

    ShipMovement moveing;

    void Start()
    {
        reverseBtn.Toggled += ReverseClicked;
    }

    void Update()
    {
        UpdateGuage();

    }

    void UpdateGuage()
    {
        if (InGame.global.localConnection == null) { return; }
        moveing = InGame.global?.localConnection?.playerEntity?.vessel?.movement;
        if (moveing == null) { return; } //if everything is set up, continue

        //Set Speed Arrow
        float targetSpeed = Mathf.Abs(moveing.inputThrottle); //read target and current
        float percentSpeed = moveing.speed / moveing.perfectMaxSpeed; //how fast it is going / how fast it could be going
        if (!float.IsNaN(percentSpeed)) //if max speed > 0
        {
            arrowTargetAngle = 45 + (90 * percentSpeed);
            arrowAngle = Mathf.SmoothDamp(arrowAngle, arrowTargetAngle, ref smoothDampSpeedArrow, 0.07f);
        }

        //Set Throttle Handle
        handleTargetAngle = 45 + (90 * throttle);
        handleAngle = Mathf.SmoothDamp(handleAngle, handleTargetAngle, ref smoothDampSpeedHandle, 0.07f);
        
        handle.localRotation = Quaternion.Euler(handleAngle, 0, 0);
        arrow.localRotation = Quaternion.Euler(arrowAngle, 0, 0);

        float ver = Input.GetAxis("Vertical");
        if (Mathf.Abs(ver) > 0.1f)
        {
            throttle = Mathf.Clamp01(throttle + ver * Time.deltaTime / 2);
            InGame.global.localConnection.CmdSetShipThrust(throttle * direction); //tell the ship to change speed
        }
   }

    void ReverseClicked(bool rev)
    {
        direction = (rev) ? -1 : 1;
        InGame.global.localConnection.CmdSetShipThrust(Mathf.Abs(moveing.inputThrottle) * direction);
    }

    void Click(Vector3 pos)
    {
        float angle = 90 - (Mathf.Atan2(pos.x, pos.z) * Mathf.Rad2Deg);
        throttle = Mathf.Clamp01((angle - 45) / 90);
        SendThrottle(throttle);
    }

    void SendThrottle(float throttle)
    {
        InGame.global?.localConnection?.CmdSetShipThrust(throttle * direction);
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
