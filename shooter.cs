using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooter : MonoBehaviour
{
    RaycastHit2D toPlayer;
    public playerMovement player;
    LayerMask mask;
    public float cooldown;
    public projectile bullets;
    float lastShot;

    // Start is called before the first frame update
    void Start()
    {
        mask = LayerMask.GetMask("Player2", "Follower", "PowerUp", "Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 temp = new Vector3(player.transform.position.x - this.transform.position.x, player.transform.position.y - this.transform.position.y, 0);
        toPlayer = Physics2D.Raycast(transform.position, temp, Mathf.Infinity, ~mask.value);

        if (toPlayer.collider != null) {
            if (toPlayer.collider.tag == "Player" && lastShot + cooldown <= Time.time) {
                lastShot = Time.time;
                projectile placeHolder = Instantiate(bullets, this.transform.position, bullets.transform.rotation);
                placeHolder.move = Vector3.Normalize(temp);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player" && player.dashing) {
            Destroy(this.gameObject);
        }
    }
}
