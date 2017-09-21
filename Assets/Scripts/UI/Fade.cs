using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour {

    public float fadeTime = 1f;
    public float targetAlpha = 1f;
    public CanvasRenderer target;

    private float stepAlpha;
    private float currentAlpha = 0f;

	void OnEnable ()
    {
        currentAlpha = 0f;
        target.SetAlpha(currentAlpha);
        stepAlpha = targetAlpha / (fadeTime * 60f);
        InvokeRepeating("FadeGraphic", 0f, 0.01666f);
	}
	
    public void FadeGraphic()
    {
        currentAlpha += stepAlpha;
        if (currentAlpha > targetAlpha)
        {
            currentAlpha = targetAlpha;
            CancelInvoke();
        }
        target.SetAlpha(currentAlpha);
    }
}
