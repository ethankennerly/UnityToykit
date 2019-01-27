///Copied from:
///http://wiki.unity3d.com/index.php/Camera_Shake
///27 Jan 2019

///Description
///This script offers a simple Camera Shake effect with a given time and amount. The script does not affect the transform of the main Camera (or any Camera), so this script should work with any Character Controller, even with Unity 5's.
///
///Usage
///Place this script on a parent gameobject of the main camera, or any camera. The shake effect can be triggered at any point with the preset values, or you can pass in an amount and a duration, which will be added to the current. To pass values via another script, use:
///
///     GetComponent<CameraShake>().ShakeCamera(20f, 1f);

///Daniel Moore (Firedan1176) - Firedan1176.webs.com/
///26 Dec 2015
///
///Shakes camera parent object
 
using UnityEngine;
using System.Collections;
 
public class CameraShake : MonoBehaviour {

    public bool debugMode = false;//Test-run/Call ShakeCamera() on start
 
    public float shakeAmount;//The amount to shake this frame.
    public float shakeDuration;//The duration this frame.
 
    //Readonly values...
    float shakePercentage;//A percentage (0-1) representing the amount of shake to be applied when setting rotation.
    float startAmount;//The initial shake amount (to determine percentage), set when ShakeCamera is called.
    float startDuration;//The initial shake duration, set when ShakeCamera is called.
 
    bool isRunning = false;    //Is the coroutine running right now?
 
    public bool smooth;//Smooth rotation?
    public float smoothAmount = 5f;//Amount to smooth

    public static CameraShake instance { get; private set; }
 
    void Start () {

        instance = this;

        if(debugMode) ShakeCamera ();
    }
 
 
    void ShakeCamera() {
 
        startAmount = shakeAmount;//Set default (start) values
        startDuration = shakeDuration;//Set default (start) values
 
        if (!isRunning) StartCoroutine (Shake());//Only call the coroutine if it isn't currently running. Otherwise, just set the variables.
    }
 
    public void ShakeCamera(float amount, float duration) {
 
        shakeAmount += amount;//Add to the current amount.
        startAmount = shakeAmount;//Reset the start amount, to determine percentage.
        shakeDuration += duration;//Add to the current time.
        startDuration = shakeDuration;//Reset the start time.
 
        if(!isRunning) StartCoroutine (Shake());//Only call the coroutine if it isn't currently running. Otherwise, just set the variables.
    }
 
 
    IEnumerator Shake() {
        isRunning = true;
 
        while (shakeDuration > 0.01f) {
            Vector3 rotationAmount = Random.insideUnitSphere * shakeAmount;//A Vector3 to add to the Local Rotation
            rotationAmount.z = 0;//Don't change the Z; it looks funny.
 
            shakePercentage = shakeDuration / startDuration;//Used to set the amount of shake (% * startAmount).
 
            shakeAmount = startAmount * shakePercentage;//Set the amount of shake (% * startAmount).
            shakeDuration = Mathf.Lerp(shakeDuration, 0, Time.deltaTime);//Lerp the time, so it is less and tapers off towards the end.
 
 
            if(smooth)
                transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(rotationAmount), Time.deltaTime * smoothAmount);
            else
                transform.localRotation = Quaternion.Euler (rotationAmount);//Set the local rotation the be the rotation amount.
 
            yield return null;
        }
        transform.localRotation = Quaternion.identity;//Set the local rotation to 0 when done, just to get rid of any fudging stuff.
        isRunning = false;
    }
 
}
