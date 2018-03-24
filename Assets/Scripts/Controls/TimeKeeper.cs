using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeKeeper : MonoBehaviour {

    // Use this for initialization
    private float totalDelta;
    public float maxTime;

	void Start ()
    {
        totalDelta = 0;
        enabled = false;
	}

    public float getMaxTime()
    {
        return maxTime;
    }

    public void startTimer()
    {
        totalDelta = 0;
        enabled = true;
    }

    public float getTime()
    {
        return totalDelta;
    }

    public bool isActive()
    {
        return enabled;
    }

    public float stopTimer()
    {
        enabled = false;
        return totalDelta;
    }

    private void Update()
    {
        totalDelta += Time.deltaTime;
        if (totalDelta > maxTime)
            stopTimer();
    }

}
