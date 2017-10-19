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
    private float duration = .5f;
    private float lerp = 0f;

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
        // Linearly update all post processing values which aren't at their target values.
        lerp += Time.deltaTime / duration;
        VignetteModel.Settings vignette = profile.vignette.settings;
        if (vignette.intensity != targetVignette)
        {
            vignette.intensity = Mathf.Lerp(fromVignette, targetVignette, lerp);
        }
        DepthOfFieldModel.Settings dof = profile.depthOfField.settings;
        if (dof.aperture != targetDof)
        {
            dof.aperture = Mathf.Lerp(fromDof, targetDof, lerp);
        }
        ColorGradingModel.Settings cgm = profile.colorGrading.settings;
        if (cgm.basic.saturation != targetSaturation)
        {
            cgm.basic.saturation = Mathf.Lerp(fromSaturation, targetSaturation, lerp);
        }
        if (cgm.basic.temperature != targetTemp)
        {
            cgm.basic.temperature = Mathf.Lerp(fromTemp, targetTemp, lerp);
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

        lerp = 0f;
        /* NOTE: Lots of magic numbers here.
         * I created basic mathematical equations to get our target variables
         * to reach desired values, dependent on what our time scale is.
         */
        if (timeScale.getScale() < 1f)
        {
            //Vignette
            targetVignette = (1f - timeScale.getScale()) / 1.2f;
        }
        else if (timeScale.getScale() > 1f)
        {
            //Dof
            targetDof = 15f - (2f * timeScale.getScale());

            //Saturation
            targetSaturation = 1f - ((timeScale.getScale() - 1f)/2f);

            //Temperature
            targetTemp = timeScale.getScale() * 20f;
        }
        else
        {
            targetVignette = 0f;
            targetDof = 13f;
            targetSaturation = 1f;
            targetTemp = 0f;
        }
    }
}
