using UnityEngine;
using System.Collections.Generic;

public class InGame : MonoBehaviour
{
	
	//public static Player player;
	public static Global global;
    public static WorldMananger worldMananger;
    public static Definitions definitions;
    //public static Interface ui;

    void Awake() {
		GameObject g;

		g = safeFind("Global"); // (some persistent gema object)
		global = (Global)safeComponent( g, "Global" );
        g = safeFind("WorldMananger"); // (some persistent gema object)
        worldMananger = (WorldMananger)safeComponent(g, "WorldMananger");
        g = safeFind("Definitions"); // (some persistent gema object)
        definitions = (Definitions)safeComponent(g, "Definitions");

        //g = safeFind("Canvas"); // (some persistent gema object)
        //ui = (Interface)safeComponent( g, "Interface" );

    }

	public static void SayHello()
	{
		Debug.Log("Confirming to developer that the Grid is working fine.");
	}

	//This returns the angle in radians
	public static float AngleInRad(Vector2 vec1, Vector2 vec2) {
		return Mathf.Atan2(vec2.y - vec1.y, vec2.x - vec1.x);
	}

	//This returns the angle in degrees
	public static float AngleInDeg(Vector2 vec1, Vector2 vec2) {
		return AngleInRad(vec1, vec2) * 180 / Mathf.PI;
	}

	public static Vector2 RadianToVector2(float radian)
	{
		return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
	}

	public static Vector2 RadianToVector2(float radian, float length)
	{
		return RadianToVector2(radian) * length;
	}

	public static Vector2 DegreeToVector2(float degree)
	{
		return RadianToVector2(degree * Mathf.Deg2Rad);
	}

	public static Vector2 DegreeToVector2(float degree, float length)
	{
		return RadianToVector2(degree * Mathf.Deg2Rad) * length;
	}

    public static float Vector2ToDegree(Vector2 v2) {
		return Mathf.Atan2(v2.y, v2.x) * Mathf.Rad2Deg;
	}

    public static Vector2 V3toV2(Vector3 xz) {
		return new Vector2 (xz.x, xz.z);
	}

	public static Vector3 V2toV3(Vector2 xy) {
		return new Vector3 (xy.x, 0, xy.y);
	}

    public static float Map(float x, float in_min, float in_max, float out_min, float out_max)
    {
        return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
    }

    // just some convenience routines to save people copy pasting 

    // when Grid wakes up, it checks everything is in place...
    private static GameObject safeFind(string s)
	{
		GameObject g = GameObject.Find(s);
		if ( g == null ) bigProblem("The " +s+ " game object is not in this scene. You're stuffed.");
		// next .... see Vexe to check that there is strictly ONE of these fuckers. you never know.
		return g;
	}
	private static Component safeComponent(GameObject g, string s)
	{
		Component c = g.GetComponent(s);
		if ( c == null ) bigProblem("The " +s+ " component is not there. You're stuffed.");
		return c;
	}
	private static void bigProblem(string error)
	{
		for (int i=10;i>0;--i) Debug.LogError(" >>> Cannot proceed... " +error);
		for (int i=10;i>0;--i) Debug.LogError(" !!!!!  Is it possible you just forgot to launch from scene zero.");
		Debug.Break();
	}
}
