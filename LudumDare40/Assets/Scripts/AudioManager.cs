using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    
    public GameObject containerSFX;
    private static AudioSource[] SFX;
    public static AudioGlobalList GlobalSounds;

	void Awake()
    {
        GlobalSounds = GetComponentInChildren<AudioGlobalList>();
        SFX = GetComponentsInChildren<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public static void PlaySFX(AudioClip clip, float volume)
    {
        for(int i = 0; i < SFX.Length; ++i)
        {
            if (!SFX[i].isPlaying)
            {
                SFX[i].PlayOneShot(clip, volume);
                break;
            }
            else
            {
                if(i == SFX.Length - 1)
                {
                    SFX[0].PlayOneShot(clip, volume);
                }
            }
        }
    }
}
