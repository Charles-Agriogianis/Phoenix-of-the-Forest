using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class player2Movement : MonoBehaviour
{
    public float speed;
    public Camera sceneCam;
    public List<PowerUp> powerUp = new List<PowerUp>();
    public List<float> start;
    int selected;
    float currentX;
    float currentY;
    float moveX;
    float moveY;
    PowerUp placeHolder;
    Vector3 mousePos;

    // Start is called before the first frame update
    void Start() {
        start = new List<float>(powerUp.Count);

        for (int i = 0; i < start.Capacity; i++) {
            start.Add(0);
        }

        mousePos = sceneCam.ScreenToWorldPoint(Input.mousePosition);

        if (powerUp[0] != null) {
            placeHolder = Instantiate(powerUp[selected], new Vector3(mousePos.x, mousePos.y, 0), powerUp[selected].transform.rotation);
            placeHolder.shouldDelete = false;
            placeHolder.gameObject.tag = "PowerUp";
            placeHolder.gameObject.layer = LayerMask.NameToLayer("PowerUp");
        }
    }

    // Update is called once per frame
    void Update() {
        mousePos = sceneCam.ScreenToWorldPoint(Input.mousePosition);

        if (placeHolder != null) {
            placeHolder.transform.position = new Vector3(mousePos.x, mousePos.y, 0);

            if (placeHolder.canPlace) {
                placeHolder.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .5f);
            } else {
                placeHolder.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f, .5f);
            }

            if (placeHolder.canRotate4) {
                if (Input.GetAxisRaw("Mouse ScrollWheel") > 0) {
                    placeHolder.transform.Rotate(0, 0, 90);
                } else if (Input.GetAxisRaw("Mouse ScrollWheel") < 0) {
                    placeHolder.transform.Rotate(0, 0, -90);
                }
            }

            if (placeHolder.canRotate8) {
                if (Input.GetAxisRaw("Mouse ScrollWheel") > 0) {
                    placeHolder.transform.Rotate(0, 0, 45);
                } else if (Input.GetAxisRaw("Mouse ScrollWheel") < 0) {
                    placeHolder.transform.Rotate(0, 0, -45);
                }
            }
        }

        currentX = this.transform.position.x;
        currentY = this.transform.position.y;
        moveX = 0;
        moveY = 0;

        if (mousePos.x < currentX) {
            if (currentX - (speed * Time.deltaTime) > mousePos.x) {
                moveX = -(speed * Time.deltaTime);
            } else {
                moveX = -(currentX - mousePos.x);
            }
        }

        if (mousePos.x > currentX) {
            if (currentX + (speed * Time.deltaTime) < mousePos.x) {
                moveX = speed * Time.deltaTime;
            } else {
                moveX = mousePos.x - currentX;
            }
        }

        if (mousePos.y < currentY) {
            if (currentY - (speed * Time.deltaTime) > mousePos.y) {
                moveY = -(speed * Time.deltaTime);
            } else {
                moveY = -(currentY - mousePos.y);
            }
        }

        if (mousePos.y > currentY) {
            if (currentY + (speed * Time.deltaTime) < mousePos.y) {
                moveY = speed * Time.deltaTime;
            } else {
                moveY = mousePos.y - currentY;
            }
        }

        this.transform.position += new Vector3(moveX, moveY, 0);

        if (Input.GetMouseButtonDown(0) && powerUp[0] != null && (start[selected] == 0 || start[selected] + powerUp[selected].cooldown <= Time.time) && placeHolder.canPlace) {
            powerUp[selected].createTime = Time.time;
            start[selected] = Time.time;
            powerUp[selected].shouldDelete = true;
            if (powerUp[selected] != null) {
                Instantiate(powerUp[selected], new Vector3(mousePos.x, mousePos.y, 0), placeHolder.transform.rotation);
            }
        }

        if (Input.GetMouseButtonDown(1)) {
            if (selected < powerUp.Count - 1) {
                selected++;
            } else {
                selected = 0;
            }

            Destroy(placeHolder.gameObject);
            placeHolder = Instantiate(powerUp[selected], new Vector3(mousePos.x, mousePos.y, 0), powerUp[selected].transform.rotation);
            placeHolder.shouldDelete = false;
            placeHolder.gameObject.tag = "PowerUp";
            placeHolder.gameObject.layer = LayerMask.NameToLayer("PowerUp");
        }
    }
}
