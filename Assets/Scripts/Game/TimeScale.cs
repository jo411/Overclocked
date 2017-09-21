using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScale : MonoBehaviour {

    private float scale = 1f;
    [SerializeField]
    private int baseIndex = 3;
    [SerializeField]
    private float[] scaleValues;
    [SerializeField]
    private float[] musicScaleValues;
    [SerializeField]
    private AudioSource music;
    [SerializeField]
    private AudioClip slowClip;
    [SerializeField]
    private AudioClip accelerateClip;
    [SerializeField]
    private AudioSource soundEffects;


    private int index;
	// Use this for initialization
	void Start () {
        index = baseIndex;
        if (music == null)
        {
            music = GameObject.Find("Music").GetComponentInChildren<AudioSource>();
        }
        soundEffects = GetComponentInChildren<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public float getScale()
    {
        return scale;
    }

    public void slowTime()
    {
        if (index > 0)
        {
            index--;
            soundEffects.PlayOneShot(slowClip);
        }
        scale = scaleValues[index];
        music.pitch = musicScaleValues[index];
        music.outputAudioMixerGroup.audioMixer.SetFloat("MusicPitchShift", musicScaleValues[6 - index]);
    }

    public void accelerateTime()
    {
        if (index < scaleValues.Length - 1)
        {
            index++;
            soundEffects.PlayOneShot(accelerateClip);
        }
        scale = scaleValues[index];
        music.pitch = musicScaleValues[index];
        music.outputAudioMixerGroup.audioMixer.SetFloat("MusicPitchShift", musicScaleValues[6 - index]);
    }

    public void resetTime()
    {
        index = baseIndex;
        scale = scaleValues[index];
        music.pitch = musicScaleValues[index];
        music.outputAudioMixerGroup.audioMixer.SetFloat("MusicPitchShift", musicScaleValues[6 - index]);
    }
}
