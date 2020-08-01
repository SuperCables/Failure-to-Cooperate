using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MusicMananger : MonoBehaviour
{
    [Header("Assignment")]
    public AudioClip SpaceBack;
    public AudioClip CombatBack;
    [Header("Debug")] //TODO: Auto find ships
    public Transform player;
    public Transform evil;

    float musicCoolDown = 0;
    int mode = 0; //0 = calm, 1=combat

    AudioMananger audioMananger;

    void Start()
    {
        audioMananger = gameObject.GetComponent<AudioMananger>();
    }

    void Update()
    {
        if (NetworkServer.active == false) { return; }
        //FIXME!
        musicCoolDown -= Time.deltaTime;
        if (musicCoolDown < 0) {
            if (player == null || evil == null) return;
            Vector2 delta = G.V3toV2(player.position - evil.position);
            float dist = delta.SqrMagnitude();

            if (mode == 1 && dist > 500 * 500) {
                mode = 0;
                audioMananger.PlayMusic(SpaceBack, 4);
                musicCoolDown = 5;
            }

            if (mode == 0 && dist < 200 * 200) {
                mode = 1;
                audioMananger.PlayMusic(CombatBack, 2);
                musicCoolDown = 20;
            }
        }
    }
}
