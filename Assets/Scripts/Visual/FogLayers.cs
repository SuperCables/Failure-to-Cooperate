using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogLayers : MonoBehaviour
{
    public GameObject layer;
    public float pitch = 10;
    public int layers = 50;
    public float drift = 20;

    int currentLayer;

    void Start()
    {
        while (currentLayer < layers)
        {
            GameObject go = Instantiate(layer, transform);
            go.transform.localPosition = new Vector3(Random.Range(-drift, drift) , -currentLayer * pitch, Random.Range(-drift, drift));
            currentLayer += 1;
        }
    }

    void Update()
    {
        
    }
}
