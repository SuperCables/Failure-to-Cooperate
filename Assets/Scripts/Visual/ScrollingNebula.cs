using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingNebula : MonoBehaviour
{
    ParticleSystem.Particle[] stars;
    ParticleSystem effect;

    public float radius = 250;
    public float killTime = 0;

    float sqrmag;

    void Start()
    {
        effect = GetComponent<ParticleSystem>();
        StartCoroutine(clean());
    }

    void Update()
    {
        
    }

    IEnumerator clean()
    {
        while (true)
        {
            if (stars == null || stars.Length < effect.main.maxParticles)
            {
                stars = new ParticleSystem.Particle[effect.main.maxParticles];
            }

            sqrmag = radius * radius + 1;
            Vector3 pos = transform.position;

            int count = effect.GetParticles(stars);

            for (int i = 0; i < count; i++)
            {
                //print(stars[i].remainingLifetime + "   " + stars[i].startLifetime);
                //if (stars[i].remainingLifetime > killTime)
                if (stars[i].remainingLifetime > killTime)
                {
                    float sqrdist = (pos - stars[i].position).sqrMagnitude;
                    if (sqrdist > sqrmag)
                    {
                        stars[i].remainingLifetime = killTime; //start fading out
                    }
                }
            }

            effect.SetParticles(stars);

            yield return new WaitForSeconds(1f);
        }
    }

}
