using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [Header("Aviation status")]
    public AviationManager aviationManager;

    [Header("Audio sources")]
    public AudioSource lowPass;
    public AudioSource highPass;
    public AudioSource highPass2;

    public AudioSource airResistLeft;
    public AudioSource airRestRight;

    public AudioSource vulkanReady;
    public AudioSource vulkanShot;
    public AudioSource vulkanEnd;

    [Header("values")]
    private float currForce;
    private float throttle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currForce = aviationManager.thrustControl.currForce;
        throttle = aviationManager.throttle;

        lowPass.volume = 1 - throttle;
        highPass.volume = throttle;
        highPass2.volume = highPass.volume + 0.2f;

        airResistLeft.volume = currForce / 3000;
        airRestRight.volume = currForce / 3000;
    }
}
