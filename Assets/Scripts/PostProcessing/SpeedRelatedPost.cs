using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class SpeedRelatedPost : MonoBehaviour {

    public Color vigColor;
    public TimeScale timeScale;

    private PostProcessVolume volume;
    private Bloom bloom;
    private Vignette vignette;
    private MotionBlur motionBlur;

    private float fromVignette = 0f, targetVignette = 0f;
    private float fromBloom = 0f, targetBloom = 0f;
    private float vignetteLerp = 0f, bloomLerp = 0f, duration = .5f;

	// Use this for initialization
	void Start ()
    {
        timeScale = Utils.getTimeScale();
        timeScale.addListener(this.gameObject);
        vignette = ScriptableObject.CreateInstance<Vignette>();
        motionBlur = ScriptableObject.CreateInstance<MotionBlur>();
        bloom = ScriptableObject.CreateInstance<Bloom>();
        volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, vignette, motionBlur, bloom);
        volume.isGlobal = true;
        bloom.enabled.Override(true);
        bloom.threshold.Override(1.1f);
        vignette.enabled.Override(true);
        vignette.color.Override(vigColor);
        vignette.smoothness.Override(1f);
        motionBlur.enabled.Override(true);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (vignette.intensity != targetVignette)
        {
            vignetteLerp += Time.deltaTime / duration;
            vignette.intensity.Override(Mathf.Lerp(fromVignette, targetVignette, vignetteLerp));
        }
        if (bloom.intensity != targetBloom)
        {
            bloomLerp += Time.deltaTime / duration;
            bloom.intensity.Override(Mathf.Lerp(fromBloom, targetBloom, bloomLerp));
        }

    }

    public void OnSlowTime()
    {
        UpdateVolumes();
    }

    public void OnAccelerateTime()
    {
        UpdateVolumes();
    }

    public void OnResetTime()
    {
        UpdateVolumes();
    }

    public void UpdateVolumes()
    {
        fromVignette = vignette.intensity;
        fromBloom = bloom.intensity;

        if (timeScale.getScale() < 1f)
        {
            //Vignette
            targetVignette = (1f - timeScale.getScale()) / 2f;
            vignetteLerp = 0f;
        }
        else if (timeScale.getScale() > 1f)
        {
            //Bloom
            targetBloom = (5f * timeScale.getScale());
            bloomLerp = 0f;
        }
        else
        {
            vignetteLerp = 0f;
            bloomLerp = 0f;
            targetVignette = 0f;
            targetBloom = 0f;
        }
    }

    //public void LerpVignette(float current, float target)
    //{
    //    vignette.intensity.Override(Mathf.Lerp(current, target, ))
    //}
}
