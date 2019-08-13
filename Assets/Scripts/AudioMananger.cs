using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMananger : MonoBehaviour
{
    float masterVolume = 1;
    float MusicVolume = 1;
    float SFXVolume = 1;

    public AudioSource[] musicSources;
    int activeMusicIndex = 0;

    void Awake()
    {

    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void PlayMusic(AudioClip clip, float fadeTime = 1)
    {
        activeMusicIndex = 1 - activeMusicIndex;
        musicSources[activeMusicIndex].clip = clip;
        musicSources[activeMusicIndex].Play();
        StartCoroutine(FadeMusic(fadeTime));
    }

    public void PlaySound(AudioClip clip, Vector3 pos)
    {
        AudioSource.PlayClipAtPoint(clip, pos, SFXVolume * masterVolume);
    }

    IEnumerator FadeMusic(float length)
    {
        float percent = 0;
        while (percent < 1) {
            percent += Time.deltaTime / length;
            float maxMusic = MusicVolume * masterVolume;
            musicSources[activeMusicIndex].volume = Mathf.Lerp(0, maxMusic, percent);
            musicSources[1-activeMusicIndex].volume = Mathf.Lerp(maxMusic, 0, percent);
            yield return null;
        }
        musicSources[1 - activeMusicIndex].Stop();
    }
}
