using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follower : MonoBehaviour {
    public playerMovement player;
    int index = 0;
    float startTime;
    public float activationTime;
    bool activated = false;

    // Start is called before the first frame update
    void Start() {
        startTime = Time.time;
        index = 0;
    }

    // Update is called once per frame
    void Update() {
        if (player.dead) {
            this.gameObject.SetActive(false);
        }

        if (Time.time >= startTime + activationTime) {
            activated = true;
        } else {
            activated = false;
        }

        if (activated) {
            if (player.prevPos.Count <= index) {
                index -= 1;
            } else {
                this.transform.position = player.prevPos[index];
                index++;
            }
        }
    }
}
