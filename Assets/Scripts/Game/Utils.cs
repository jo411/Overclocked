using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils {
    public static System.Random rand = new System.Random();

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

    /// <summary>
    /// Returns true with chanceOfSuccess probability
    /// </summary>
    /// <param name="chanceOfSuccess"></param>
    /// <returns></returns>
    public static bool checkProbability(float chanceOfSuccess)
    {
        int chance = (int)(100 * chanceOfSuccess);
        return rand.Next(101) <= chance;
    }
    

}
