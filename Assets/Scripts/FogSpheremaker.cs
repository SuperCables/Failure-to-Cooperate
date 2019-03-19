using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogSpheremaker : MonoBehaviour
{
    public Vector3[] points;

    public Mesh mesh;
    public Transform fogArmTemplate;

    void Start()
    {
        MakeSphere();
    }

    void MakeSphere()
    {
        foreach (Vector3 v in points)
        {
            Transform go = Instantiate(fogArmTemplate);
            go.SetParent(this.transform, false);
            go.localPosition = Vector3.zero;
            go.localRotation = Quaternion.LookRotation(v, Random.onUnitSphere);
        }
    }

    [ContextMenu("CalculatePoints")]
    void CalculatePoints ()
    {
        List<Vector3> usedPoints = new List<Vector3>();
        print("Resetting Points");
        usedPoints.Clear();
        foreach (Vector3 v in mesh.vertices)
        {
            bool newV = true;
            foreach (Vector3 chk in usedPoints)
            {
                if ((chk - v).magnitude < 0.1f)
                {
                    newV = false;
                }
            }

            if (newV)
            {
                usedPoints.Add(v);
                
            }

        }
        points = usedPoints.ToArray();
    }

}
