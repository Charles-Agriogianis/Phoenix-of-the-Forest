using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishScreen : MonoBehaviour
{
    [SerializeField] private GameObject levelCompleteUI;

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    levelCompleteUI.SetActive(true);
    //    Debug.Log("LEvel Complete");
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            levelCompleteUI.SetActive(true);
            Debug.Log("Level Complete");
        }

    }
}


