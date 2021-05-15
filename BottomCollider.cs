using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomCollider : MonoBehaviour
{
    public playerMovement player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Level") {
            player.offGround = false;
            player.onLad = false;
            player.jump = 0;
            player.dash = 0;
            player.gravity = 0;
        }
    }

    void OnTriggerExit2D(Collider2D collision) {
        if (collision.tag == "Level") {
            player.offGround = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        //set distance from objects
        if (collision.tag == "Level") {
            if (player.offGround == false) {
                player.transform.position = new Vector3(player.transform.position.x, collision.bounds.center.y + collision.bounds.extents.y + player.playerHeight, 0);
            }

            if (player.onLad) {
                if (player.transform.position.y < (collision.bounds.center.y + collision.bounds.extents.y + player.playerHeight)) {
                    player.transform.position = new Vector3(player.transform.position.x, collision.bounds.center.y + collision.bounds.extents.y + player.playerHeight, 0);
                }
            }
        }
    }
}
