using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindTunnel : MonoBehaviour
{
    public float speed;
    public playerMovement player;
    Collider2D collided;
    bool inTunnel = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (inTunnel && collided != null) {
            this.transform.position += new Vector3(collided.transform.up.x * speed * Time.deltaTime, collided.transform.up.y * speed * Time.deltaTime, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "WindTunnel") {
            collided = collision;
            inTunnel = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.tag == "WindTunnel") {
            inTunnel = false;
            player.gravity = player.jumpStart * this.transform.up.y;

            if (player.gravity > 0) {
                player.offGround = true;
            }

            player.moveSpeed = player.moveAccel * this.transform.up.x;
        }
    }
}
