using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderCollider : MonoBehaviour
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

    void OnTriggerExit2D(Collider2D collision) {
        player.onLad = false;
    }

    void OnTriggerStay2D(Collider2D collision) {
        //set distance from objects
        if (collision.tag == "Climbable") {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)) {
                player.onLad = true;
                player.offGround = true;
                //player.ladOrigin = collision.bounds.extents;
                //player.swingSpeed = 0;
            }

            if (player.onLad == true) {
                player.transform.position = new Vector3(collision.bounds.center.x, player.transform.position.y, 0);
            }

        }
    }
}
