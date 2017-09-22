using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScale : MonoBehaviour {

    public float transitionTime = 1f;

    private float fromPitch = 1f, targetPitch = 1f;
    private float fromGroupPitch = 1f, targetGroupPitch = 1f;
    private float pitchLerp = 0f, groupPitchLerp = 0f;

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
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if (music.pitch != targetPitch)
        {
            pitchLerp += Time.deltaTime / transitionTime;
            music.pitch = Mathf.Lerp(fromPitch, targetPitch, pitchLerp);
        }
        float groupPitch;
        music.outputAudioMixerGroup.audioMixer.GetFloat("MusicPitchShift", out groupPitch);
        if (groupPitch != targetGroupPitch)
        {
            groupPitchLerp += Time.deltaTime / transitionTime;
            music.outputAudioMixerGroup.audioMixer.SetFloat("MusicPitchShift", Mathf.Lerp(fromGroupPitch, targetGroupPitch, groupPitchLerp));
        }
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
            //soundEffects.PlayOneShot(slowClip);
        }
        scale = scaleValues[index];
        updatePitch();
        sendMessageToListeners("OnSlowTime");
    }

    public void accelerateTime()
    {
        if (index < scaleValues.Length - 1)
        {
            index++;
            //soundEffects.PlayOneShot(accelerateClip);
        }
        scale = scaleValues[index];
        updatePitch();
        sendMessageToListeners("OnAccelerateTime");
    }

    public void resetTime()
    {
        index = baseIndex;
        scale = scaleValues[index];
        updatePitch();
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

    private void updatePitch()
    {
        //music.pitch = musicScaleValues[index];
        //music.outputAudioMixerGroup.audioMixer.SetFloat("MusicPitchShift", musicScaleValues[6 - index]);
        //Reset lerp variables
        pitchLerp = 0f;
        groupPitchLerp = 0f;
        //Reset from and to values
        fromPitch = music.pitch;
        targetPitch = musicScaleValues[index];
        music.outputAudioMixerGroup.audioMixer.GetFloat("MusicPitchShift", out fromGroupPitch);
        targetGroupPitch = musicScaleValues[6 - index];
    }
}
