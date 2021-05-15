using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dasher2 : MonoBehaviour
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

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Dasher") {
            player.dash = 0;
            player.jump = 0;
        }
    }
}
