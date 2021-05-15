using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopCollider : MonoBehaviour
{
    public playerMovement player;

    // Start is called before the first frame updated
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Level") {
            player.gravity = 0;
            player.jump = 2;
        }
    }
}
