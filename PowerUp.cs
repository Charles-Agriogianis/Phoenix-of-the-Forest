using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float cooldown;
    public float duration;
    public float createTime;
    public bool shouldDelete;
    public bool canPlace;
    public bool colliding = false;
    public bool canRotate4 = false;
    public bool canRotate8 = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (createTime + duration <= Time.time && shouldDelete) {
            Destroy(this.gameObject);
        }

        if (colliding == false) {
            canPlace = true;
        }
    }

    void OnTriggerStay2D(Collider2D collision) {
        if (collision.tag == "Level" || collision.tag == "Player" || collision.tag == "PowerUp" || collision.tag == "Enemy" || collision.tag == "Climbable") {
            if (colliding == false) {
                canPlace = false;
                colliding = true;
            }
        } else {
            if (colliding == false) {
                canPlace = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision) {
        colliding = false;
    }
}
