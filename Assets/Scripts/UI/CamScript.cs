using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamScript : MonoBehaviour {

    public float yaw;
    public float pitch;

    public float horInc = 45;
    public float vertInc = 30;

    [Header("Assignment")]
    //FIXME should always follow player, or current radar center so so depth map is correct.
    public Transform follow;
    [Space(10)]
    public Camera MainCam;
    public Camera RadarCam;
    public Camera FogCam;
    [Space(10)]
    public Transform depthMount;
    public Transform mount;
    public Transform pivot;
    public Transform offset;
    [Space(10)]
    public RenderTexture radarDepthTexture;

    float heightOffset;

    float targetHeight;

    float roundYaw;
    float roundPitch;

    float camYawSpeedDamp;
    float camPitchSpeedDamp;
    float camHeightSpeedDamp;

    //ShipMovement move;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void LateUpdate () {

        if (Input.GetMouseButton(1))
        {
            yaw += Input.GetAxisRaw("Mouse X") * 2.8f;
            pitch -= Input.GetAxisRaw("Mouse Y") * 2.8f;
            pitch = Mathf.Clamp(pitch, -90, 90);
        }
        else
        {
            roundYaw = Mathf.Round((yaw) / horInc) * horInc;
            roundPitch = Mathf.Round((pitch) / vertInc) * vertInc;

            yaw = Mathf.SmoothDampAngle(yaw, roundYaw, ref camYawSpeedDamp, 0.4f);
            pitch = Mathf.SmoothDampAngle(pitch, roundPitch, ref camPitchSpeedDamp, 0.6f);

            targetHeight = (roundPitch < 0) ? -3 : 3;
        }
        heightOffset = Mathf.SmoothDampAngle(heightOffset, targetHeight, ref camHeightSpeedDamp, 1.4f);

        pivot.localRotation = Quaternion.Euler(pitch, yaw, 0);
        offset.localPosition = new Vector3(0, heightOffset, 0);


        if (follow == null) {
            PlayerConnection con = InGame.global?.localConnection;
            if (con != null)
            {
                follow = con?.playerEntity?.transform;
            }
        }

        if (follow == null) { return; }
        transform.position = follow.position;
		mount.rotation = follow.rotation;

        //pitch = move.inputDive;

    }
}
