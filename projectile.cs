using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    public float speed;
    public Vector3 move;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += move * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player" || collision.tag == "Level") {
            Destroy(this.gameObject);
        }
    }
}
