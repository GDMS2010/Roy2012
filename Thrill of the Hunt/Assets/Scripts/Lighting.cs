using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighting : MonoBehaviour
{
    public Light mylight;
    
    public bool changeRange = false;
    public float rangeSpeed = 1.0f;
    public float maxRange = 10.0f;
    public bool repeatRange = false;

    public bool changeIntensity = false;
    public float intensitySpeed = 1.0f;
    public float maxIntensity = 10.0f;
    public bool repeatIntensity = false;


    public bool changeColors = false;
    public float colorSpeed = 1.0f;
    public Color startColor;
    public Color endColor;
    public bool repeatColor = false;

    float startTime;

    void Start()
    {
        mylight = GetComponent<Light>();
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (changeRange) {
            if (repeatRange)
            {
                mylight.range = Mathf.PingPong(Time.time * rangeSpeed, maxRange);
            }
            else
            {
                mylight.range = Time.time * rangeSpeed;
                if(mylight.range >= maxRange)
                {
                    changeRange = false;
                }
            }
            
        }
        if (changeIntensity)
        {
            if (repeatIntensity)
            {
                mylight.intensity = Mathf.PingPong(Time.time * intensitySpeed, maxIntensity);
            }
            else
            {
                mylight.intensity = Time.time * intensitySpeed;
                if(mylight.intensity >= maxIntensity)
                {
                    changeIntensity = false;
                }
            }
            
        }
        if (changeColors)
        {
            if (repeatColor)
            {
                float t = (Mathf.Sin(Time.time - startTime * colorSpeed));
                mylight.color = Color.Lerp(startColor, endColor, t);
            }
            else
            {
                float t = Time.time - startTime * colorSpeed;
                mylight.color = Color.Lerp(startColor, endColor, t);
            }
            
        }
    }
}
