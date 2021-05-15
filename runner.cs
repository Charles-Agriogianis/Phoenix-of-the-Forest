using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class runner : MonoBehaviour
{
    float direction = 1;
    public playerMovement player;
    public float speed;
    public float maxX;
    public float minX;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += direction * Vector3.right * speed * Time.deltaTime;

        if (this.transform.position.x >= maxX) {
            direction = -1;
        }

        if (this.transform.position.x <= minX) {
            direction = 1;
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player" && player.dashing) {
            Destroy(this.gameObject);
        }
    }
}
