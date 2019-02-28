using UnityEngine;
using System.Collections;

public class ScrollingStars : MonoBehaviour {

	ParticleSystem.Particle[] stars;
	ParticleSystem effect;

	public int maxStars = 5000;
	public float radius = 250;
	public float size = 1;

    float sqrmag;
    int lastMaxStars = 1;

	void Start () {
		effect = GetComponent<ParticleSystem> ();
        
        SetStars();
    }

    void OnEnable()
    {
        if (effect != null)
        {
            SetStars();
        }
    }

    void SetStars()
    {
        stars = new ParticleSystem.Particle[maxStars];
        Vector3 pos = transform.position;
        for (int i = 0; i < maxStars; i++)
        {
            //stars[i].position = Random.onUnitSphere * radius + pos;
            stars[i].position = Random.insideUnitSphere * radius + pos;
            stars[i].startColor = Color.white;
            stars[i].startSize = size;
        }
    }

	void Update () {

        sqrmag = radius * radius + 1;
        if (lastMaxStars != maxStars)
        {
            SetStars();
            lastMaxStars = maxStars;
        }

		Vector3 pos = transform.position;
		for (int i = 0; i < maxStars; i++) {
			float sqrdist = (pos - stars[i].position).sqrMagnitude;
			if (sqrdist > sqrmag) {
				stars [i].position = Random.onUnitSphere * radius + pos;
                stars[i].startColor = Color.white;
                stars[i].startSize = size;
            }
			if (sqrdist < 2) {
				stars[i].position = Random.onUnitSphere * radius + pos;
                stars[i].startColor = Color.white;
                stars[i].startSize = size;
            }

		}
		effect.SetParticles(stars, stars.Length);
	}

}
