using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightCollider : MonoBehaviour {
    public playerMovement player;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Level") {
            player.collideRight = true;
            player.wallJump = 0;
            player.transform.position = new Vector3(collision.bounds.center.x - collision.bounds.extents.x - player.playerWidth, player.transform.position.y, 0);
        }
    }

    void OnTriggerStay2D(Collider2D collision) {
        if (collision.tag == "Level") {
            if (player.transform.position.x > collision.bounds.center.x - collision.bounds.extents.x - player.playerWidth) {
                player.transform.position = new Vector3(collision.bounds.center.x - collision.bounds.extents.x - player.playerWidth, player.transform.position.y, 0);
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision) {
        if (collision.tag == "Level") {
            player.collideRight = false;
        }
    }
}
