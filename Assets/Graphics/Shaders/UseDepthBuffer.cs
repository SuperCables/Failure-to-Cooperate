using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseDepthBuffer : MonoBehaviour
{
    // Give me depth buffer
    private void Start()
    {
        Camera cam = GetComponent<Camera>();
        cam.depthTextureMode = cam.depthTextureMode | DepthTextureMode.Depth;
    }
}
