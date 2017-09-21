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

    private List<GameObject> listeners = new List<GameObject>();

    private int index;
	// Use this for initialization
	void Start ()
    {
        index = baseIndex;
        if (music == null)
        {
            music = GameObject.Find("Music").GetComponentInChildren<AudioSource>();
        }
        soundEffects = GetComponentInChildren<AudioSource>();
        //listeners = new List<GameObject>();
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
        sendMessageToListeners("OnSlowTime");
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
        sendMessageToListeners("OnAccelerateTime");
    }

    public void resetTime()
    {
        index = baseIndex;
        scale = scaleValues[index];
        music.pitch = musicScaleValues[index];
        music.outputAudioMixerGroup.audioMixer.SetFloat("MusicPitchShift", musicScaleValues[6 - index]);
        sendMessageToListeners("OnResetTime");
    }

    public void addListener(GameObject listener)
    {
        listeners.Add(listener);
    }

    private void sendMessageToListeners(string message)
    {
        foreach (GameObject listener in listeners)
        {
            if (listener != null)
            {
                listener.BroadcastMessage(message);
            }
        }
    }
}
