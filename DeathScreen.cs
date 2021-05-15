using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    [SerializeField] private GameObject levelFailedUI;
    public playerMovement player;
    public float restartTime;
    float deathTime = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (player.dead && deathTime == 0) {
            deathTime = Time.time;
        }
        */

        if (Time.time >= deathTime + restartTime && deathTime != 0) {
            SceneManager.LoadScene("SampleScene");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if ((collision.tag == "Area" || collision.tag == "Follower" || collision.tag == "Projectile" || (collision.tag == "Enemy" && player.dashing == false)) && deathTime == 0) {
            levelFailedUI.SetActive(true);
            player.dead = true;
            Debug.Log("You died");
            deathTime = Time.time;
        }
    }
}
