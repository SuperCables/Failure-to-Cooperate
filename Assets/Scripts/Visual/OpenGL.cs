//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//
//public class OpenGL : MonoBehaviour {
//	public Material mat;
//
//
//	void Start() {
//		GL.LoadProjectionMatrix(Camera.main.projectionMatrix);
//		GL.modelview = Camera.main.worldToCameraMatrix;
//	}
//	
//	void OnPostRender() {
//		if (!mat) {
//			Debug.LogError("Please Assign a material on the inspector");
//			return;
//		}
//
//		foreach (GimbledTurret v in FindObjectsOfType<GimbledTurret>()) {
//			for (int i = 0; i < v.GLs.Length; i+=2) {
//				mat.SetPass(0);
//				GL.Begin(GL.LINES);
//				GL.Color(Color.red);
//				GL.Vertex(Game.V2toV3(v.GLs[i]));
//				GL.Vertex(Game.V2toV3(v.GLs[i+1]));
//				GL.End();
//			}
//				
//
//		}
//
//
//	}
//
//
//}
