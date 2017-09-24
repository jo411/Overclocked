using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimePickUp : PickUp {
    public TimeBar timeBar;

     void Start()
    {
        timeBar = GameObject.Find("Canvas").GetComponent<TimeBar>();
    }
    protected override void ApplyEffect(GameObject target)
    {
        timeBar.IncrementTime(200f);
    }
}
