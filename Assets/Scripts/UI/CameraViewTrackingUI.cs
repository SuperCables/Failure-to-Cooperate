using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraViewTrackingUI : MonoBehaviour
{

    public Transform station;
    
    public float sensitivity = 1;

    
    
    [Space(20)]
    public CameraViewLockPointUI view;
    public Vector3 lockingAngle;
    public LayerMask stationLayer;
    public CameraViewLockPointUI[] views;
    float smoothDampSpeedX;
    float smoothDampSpeedY;
    float smoothDampSpeedFOV;

    float currentFOV;
    float targetFOV;
    Vector3 rotation;
    Camera cam;

    //set every frame by GetMouseOver();
    Vector3 localClickPos;
    Transform mouseObject;

    void Start()
    {
        views = station.GetComponentsInChildren<CameraViewLockPointUI>();
        cam = GetComponent<Camera>();
        rotation = transform.localRotation.eulerAngles;
        foreach (CameraViewLockPointUI v in views)
        {
            v.lockingAngle = Quaternion.LookRotation(v.transform.position - transform.position, Vector3.up);
        }
        targetFOV = 60; //60;
    }

    void Update()
    {
        DragCamUpdate();
        GetMouseOver();
        if (Input.GetMouseButtonDown(0)){ClickedMouse(true);}
        if (Input.GetMouseButtonDown(1)){ClickedMouse(false);}
        if (Input.GetMouseButton(0)) { HoldMouse(true); }
        if (Input.GetMouseButton(1)) { HoldMouse(false); }
        if (Input.mouseScrollDelta.y != 0) { ScrolledMouse((int)Input.mouseScrollDelta.y); }

    }

    //called when the mouse is clicked, finds the object under the mouse and sends an event
    void ClickedMouse(bool left)
    {
        IClickable clickable = mouseObject?.GetComponentInParent<IClickable>();
        if (clickable != null)
        {
            if (left)
            {
                clickable.LeftClick(localClickPos);
            }else{
                clickable.RightClick(localClickPos);
            }
        }//end is clickable
    }//end function

    //called when the mouse is held, finds the object under the mouse and sends an event
    void HoldMouse(bool left)
    {
        IClickHoldable clickable = mouseObject?.GetComponentInParent<IClickHoldable>();
        if (clickable != null)
        {
            if (left)
            {
                clickable.LeftHold(localClickPos);
            }else{
                clickable.RightHold(localClickPos);
            }
        }//end is clickable
    }//end function

    //called when the mouse is scrolled, finds the object under the mouse and sends an event
    void ScrolledMouse(int amount)
    {
        IScrollable scrollable = mouseObject?.GetComponentInParent<IScrollable>();
        if (scrollable != null)
        {
            scrollable.Scroll(amount);
        }//end is scrollable
    }//end function

    //gets the object currently under the mouse
    void GetMouseOver()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 50, stationLayer, QueryTriggerInteraction.Collide))
        {
            localClickPos = hit.transform.InverseTransformPoint(hit.point);
            mouseObject = hit.transform;
        }
        else
        {//end hit somthing
            mouseObject = null;
        }
    }

    //called every frame to handle looking around and locking one to controls
    void DragCamUpdate()
    {
        if (Input.GetMouseButtonDown(2)) //start looking
        {
            targetFOV = 70;
            Cursor.lockState = CursorLockMode.Locked; //lock mouse
            Cursor.visible = false;
        }

        if (Input.GetMouseButtonUp(2)) //stop looking
        {
            targetFOV = 60;
            Cursor.lockState = CursorLockMode.None; //unlock mouse
            Cursor.visible = true;

            float minDeltaAngle = 180;
            CameraViewLockPointUI tmpView = null;
            foreach (CameraViewLockPointUI v in views)
            {
                float deltaAngle = Quaternion.Angle(Quaternion.Euler(rotation), v.lockingAngle);
                if (deltaAngle < minDeltaAngle && deltaAngle < v.lockingThresh && v != view)
                {
                    minDeltaAngle = deltaAngle;
                    tmpView = v;

                }
            }
            view = tmpView;
            if (tmpView != null)
            {

                lockingAngle = view.lockingAngle.eulerAngles;
                targetFOV = view.FOV;
                //print(view.title);
            }
        }

        if (Input.GetMouseButton(2))
        {
            float Dpitch = Input.GetAxis("Mouse Y") * sensitivity; //turn camera with mouse
            float Dyaw = Input.GetAxis("Mouse X") * sensitivity;

            rotation.y += Dyaw;
            rotation.x -= Dpitch;

            //print(rotation);

            rotation.x = Mathf.Clamp(rotation.x, -85, 85);
            rotation.y = Mathf.Clamp(rotation.y, -85, 85);

        }
        else //if not holding look
        {
            if (view != null)
            {
                //print(lockingAngle);
                rotation.x = Mathf.SmoothDampAngle(rotation.x, lockingAngle.x, ref smoothDampSpeedX, 0.1f);
                rotation.y = Mathf.SmoothDampAngle(rotation.y, lockingAngle.y, ref smoothDampSpeedY, 0.1f);

            }
            //damp to the closest lockable
        }
        currentFOV = Mathf.SmoothDampAngle(currentFOV, targetFOV, ref smoothDampSpeedFOV, 0.15f);
        transform.localRotation = Quaternion.Euler(rotation);
        cam.fieldOfView = currentFOV;
    }

}

