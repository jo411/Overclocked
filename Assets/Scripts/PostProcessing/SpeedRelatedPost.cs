using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class SpeedRelatedPost : MonoBehaviour {

    public Color vigColor;
    public TimeScale timeScale;

    private PostProcessingProfile profile;
    private PostProcessingBehaviour behaviour;
    private float fromVignette = 0f, targetVignette = 0f;
    private float fromSaturation = 1f, targetSaturation = 1f;
    private float fromTemp = 0f, targetTemp = 0f;
    private float fromDof = 13f, targetDof = 13f;
    private float vignetteLerp = 0f, dofLerp = 0f, saturationLerp = 0f, tempLerp = 0f, duration = .5f;

	// Use this for initialization
	void Start ()
    {
        timeScale = Utils.getTimeScale();
        timeScale.addListener(this.gameObject);

        behaviour = GetComponentInChildren<PostProcessingBehaviour>();
        if (behaviour.profile == null)
        {
            enabled = false;
            return;
        }
        profile = Instantiate(behaviour.profile);
        behaviour.profile = profile;

        profile.vignette.enabled = true;
        VignetteModel.Settings vignette = profile.vignette.settings;
        vignette.color = vigColor;
        vignette.smoothness = 1f;
        profile.vignette.settings = vignette;
	}
	
	// Update is called once per frame
	void Update ()
    {
        VignetteModel.Settings vignette = profile.vignette.settings;
        if (vignette.intensity != targetVignette)
        {
            vignetteLerp += Time.deltaTime / duration;
            vignette.intensity = Mathf.Lerp(fromVignette, targetVignette, vignetteLerp);
        }
        DepthOfFieldModel.Settings dof = profile.depthOfField.settings;
        if (dof.aperture != targetDof)
        {
            dofLerp += Time.deltaTime / duration;
            dof.aperture = Mathf.Lerp(fromDof, targetDof, dofLerp);
        }
        ColorGradingModel.Settings cgm = profile.colorGrading.settings;
        if (cgm.basic.saturation != targetSaturation)
        {
            saturationLerp += Time.deltaTime / duration;
            cgm.basic.saturation = Mathf.Lerp(fromSaturation, targetSaturation, saturationLerp);
        }
        if (cgm.basic.temperature != targetTemp)
        {
            tempLerp += Time.deltaTime / duration;
            cgm.basic.temperature = Mathf.Lerp(fromTemp, targetTemp, tempLerp);
        }
        profile.vignette.settings = vignette;
        profile.depthOfField.settings = dof;
        profile.colorGrading.settings = cgm;
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
        fromVignette = profile.vignette.settings.intensity;
        fromDof = profile.depthOfField.settings.aperture;
        fromSaturation = profile.colorGrading.settings.basic.saturation;
        fromTemp = profile.colorGrading.settings.basic.temperature;

        if (timeScale.getScale() < 1f)
        {
            //Vignette
            targetVignette = (1f - timeScale.getScale()) / 1.2f;
            vignetteLerp = 0f;
        }
        else if (timeScale.getScale() > 1f)
        {
            //Dof
            targetDof = 15f - (2f * timeScale.getScale());
            dofLerp = 0f;

            //Saturation
            targetSaturation = 1f - ((timeScale.getScale() - 1f)/2f);
            saturationLerp = 0f;

            //Temperature
            targetTemp = timeScale.getScale() * 20f;
            tempLerp = 0f;
        }
        else
        {
            vignetteLerp = 0f;
            targetVignette = 0f;
            dofLerp = 0f;
            targetDof = 13f;
            saturationLerp = 0f;
            targetSaturation = 1f;
            tempLerp = 0f;
            targetTemp = 0f;
        }
    }

    //public void LerpVignette(float current, float target)
    //{
    //    vignette.intensity.Override(Mathf.Lerp(current, target, ))
    //}
}
