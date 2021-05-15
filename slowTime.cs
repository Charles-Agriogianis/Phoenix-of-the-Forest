using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slowTime : MonoBehaviour
{
    public float slowTimeScale;
    public float maxTimeSlow;
    public float minTimeSlow;
    float timeRemaining;
    float fixedDeltaTime;
    bool timeSlowed;

    // Start is called before the first frame update
    void Start()
    {
        fixedDeltaTime = Time.fixedDeltaTime;
        timeRemaining = maxTimeSlow;
        timeSlowed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(2) && timeRemaining > minTimeSlow) {
            timeSlowed = true;
            Time.timeScale = slowTimeScale;
            Time.fixedDeltaTime = fixedDeltaTime * Time.timeScale;
        }

        if (timeSlowed && Input.GetMouseButton(2)) {
            timeRemaining -= Time.deltaTime;

            if (timeRemaining <= 0) {
                timeRemaining = 0;
                timeSlowed = false;
            }
        } else {
            timeSlowed = false;
            Time.timeScale = 1.0f;
            Time.fixedDeltaTime = fixedDeltaTime * Time.timeScale;

            timeRemaining += Time.deltaTime;

            if (timeRemaining > maxTimeSlow) {
                timeRemaining = maxTimeSlow;
            }
        }
    }
}
