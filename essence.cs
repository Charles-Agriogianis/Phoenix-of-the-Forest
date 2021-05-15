using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class essence : MonoBehaviour
{
    public player2Movement p2;
    public float CDR;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {

    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            for (int i = 0; i < p2.powerUp.Count; i++) {
                if (p2.start[i] > CDR) {
                    p2.start[i] -= CDR;
                } else {
                    p2.start[i] = 0;
                }
            }

            Destroy(this.gameObject);
        }
    }
}
