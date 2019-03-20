using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamScript : MonoBehaviour {

	
    public float snapInc = 90;
    [Range(-89.9f, 89.9f)]
    public float pitch;

    [Header("Assignment")]
    public Camera mainCam;
    public Transform follow;
    public Transform mount;
    public Transform pivot;
    public Transform offset;

    float pitchAngle;
    float heightOffset;
    float angle;

    float camTurnSpeedDamp;
    float camPitchSpeedDamp;
    float camHeightSpeedDamp;

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void LateUpdate () {

		mount.position = follow.position;
		mount.rotation = follow.rotation;

		if (Input.GetMouseButton (1)) {
			angle += Input.GetAxisRaw ("Mouse X") * 2.8f;
		} else {
			float roundAngle =  Mathf.Round((angle) / snapInc);
			roundAngle *= snapInc;
			angle = Mathf.SmoothDampAngle(angle, roundAngle, ref camTurnSpeedDamp, 0.3f);

		}

        //float targetHeight = Mathf.Clamp(Game.Map(Mathf.Abs(pitch), 0, 30, 3, 0), 0, 3);
        float targetHeight = (pitch < 0) ? -3 : 3;

        pitchAngle = Mathf.SmoothDampAngle(pitchAngle, pitch, ref camPitchSpeedDamp, 0.7f);
        heightOffset = Mathf.SmoothDampAngle(heightOffset, targetHeight, ref camHeightSpeedDamp, 2.4f);

        pivot.localRotation = Quaternion.Euler (pitchAngle, angle, 0);
        offset.localPosition = new Vector3(0, heightOffset, 0);

    }
}
