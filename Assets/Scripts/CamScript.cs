using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamScript : MonoBehaviour {

	public Transform follow;
    public float snapInc = 90;

    [Space(10)]
    public Transform mount;
    public Transform pivot;
    [Space(10)]
    public Camera mainCam;

    float camTurnSpeedDamp;

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void LateUpdate () {

		mount.position = follow.position;
		mount.rotation = follow.rotation;

		Vector3 localRotation = pivot.localRotation.eulerAngles;
		float angle = localRotation.y;
		if (Input.GetMouseButton (1)) {
			angle += Input.GetAxisRaw ("Mouse X") * 2.8f;
		} else {
			float roundAngle =  Mathf.Round((angle) / snapInc);
			roundAngle *= snapInc;
			angle = Mathf.SmoothDampAngle(angle, roundAngle, ref camTurnSpeedDamp, 0.3f);

		}
        pivot.localRotation = Quaternion.Euler (localRotation.x, angle, localRotation.z);

    }
}
