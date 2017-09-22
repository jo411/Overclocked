using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils {

	public static TimeScale getTimeScale()
    {
        GameObject obj = GameObject.Find("TimeScale");
        if (obj != null)
        {
            TimeScale ts = obj.GetComponent<TimeScale>();
            return ts;
        }
        return null;
    }

    public static void setCollision(GameObject gameObject, bool state)
    {
        foreach (Collider c in gameObject.GetComponentsInChildren<Collider>())
        {
            c.enabled = state;
        }
    }
}
