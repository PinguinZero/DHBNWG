using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {
    public double timerTop;
    public bool singleTick;
    public bool trigger;

    private double timerTopOriginal;
    private const double UPDATE_RATE = 0.02;

    // Use this for initialization
    void Start()
    {
        singleTick = true;
        trigger = false;
        timerTopOriginal = timerTop;
    }

	void FixedUpdate () {
        timerTop -= UPDATE_RATE;

        if (timerTop <= 0)
        {
            trigger = true;
        }

        if (timerTop <= -0.3)
        {
            trigger = false;
            timerTop = timerTopOriginal;
            singleTick = true;
        }
    }
}
