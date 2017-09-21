using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeAndDestroy : MonoBehaviour {

    public float initAlpha = .8f;
    public float fadeTime = 1f;
    public TimeScale timeScale;
    public bool obeyTimescale = false;

    private float fadeRate;
    private float alpha;
    private Renderer[] rend;

	// Use this for initialization
	void Start ()
    {
        timeScale = Utils.getTimeScale();
        fadeRate = 1 / (fadeTime * 60f);
        rend = GetComponentsInChildren<Renderer>();
	}

    private void FixedUpdate()
    {
        foreach (Renderer r in rend)
        {
            Color c = r.material.color;
            if (obeyTimescale)
            {
                c.a -= fadeRate * timeScale.getScale();
            }
            else
            {
                c.a -= fadeRate;
            }
            if (c.a <= 0f)
            {
                c.a = 0f;
                Destroy(gameObject);
            }
            r.material.color = c;
        }
    }


}
