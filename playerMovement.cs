using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour {
    public float gravMod;
    public float airSpeed;
    public float jumpStart;
    public float moveSpeed;
    public float maxSpeed;
    public float moveAccel;
    public float decelMod;
    public float dashMod;
    public float dashDecelMod;
    public bool offGround;
    public bool collideRight;
    public bool collideLeft;
    public bool onLad;
    public float gravity;
    public float wallMod;
    public float wallAccel;
    public float playerHeight;
    public float playerWidth;
    public float climbSpeed;
    public float dashMax;
    public float wallJumpMax;
    public float dashCooldown;
    public bool dead = false;
    public List<Vector3> prevPos;
    //public float swingSpeed;
    //public Vector3 ladOrigin;
    float startX;
    float startY;
    float currX;
    float currY;
    float dashDecelX;
    float dashDecelY;
    public int jump; //number of jumps since last touched ground, terrible variable name
    public int dash; //number of dashes since last touched ground, terrible variable name
    public bool dashing; 
    bool keyPressed;
    public int wallJump;
    float dashTime;
    RaycastHit2D up;
    RaycastHit2D down;
    RaycastHit2D left;
    RaycastHit2D right;
    RaycastHit2D upRight;
    RaycastHit2D downRight;
    RaycastHit2D upLeft;
    RaycastHit2D downLeft;
    LayerMask mask;


    // Start is called before the first frame update
    void Start() {
        mask = LayerMask.GetMask("Player1", "Player2", "Follower", "PowerUp");
        dashTime = 0;
    }

    // Update is called once per frame
    void Update() {
        prevPos.Add(this.transform.position);

        //update raycasts
        if (offGround == false && dashing == false) {
            dash = 0;
        }

        up = Physics2D.Raycast(transform.position, Vector2.up, Mathf.Infinity, ~mask.value);
        down = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, ~mask.value);
        left = Physics2D.Raycast(transform.position, Vector2.left, Mathf.Infinity, ~mask.value);
        right = Physics2D.Raycast(transform.position, Vector2.right, Mathf.Infinity, ~mask.value);
        upRight = Physics2D.Raycast(transform.position, Vector2.up + Vector2.right, Mathf.Infinity, ~mask.value);
        downRight = Physics2D.Raycast(transform.position, Vector2.down + Vector2.right, Mathf.Infinity, ~mask.value);
        upLeft = Physics2D.Raycast(transform.position, Vector2.up + Vector2.left, Mathf.Infinity, ~mask.value);
        downLeft = Physics2D.Raycast(transform.position, Vector2.down + Vector2.left, Mathf.Infinity, ~mask.value);

        keyPressed = false;

        //finish the dash
        if (dashing) {
            //Check for early collision
            if (startX > 0 && startY > 0) {
                if (upRight.collider != null) {
                    float distX = Mathf.Abs(upRight.point.x - transform.position.x);
                    float distY = Mathf.Abs(upRight.point.y - transform.position.y);


                    if (distX - playerWidth < Mathf.Abs(currX) * Time.deltaTime) {
                        currX = distX;
                    }

                    if (distY - playerHeight < Mathf.Abs(currY) * Time.deltaTime) {
                        currY = distY;
                    }
                }
            }

            if (startX < 0 && startY > 0) {
                if (upLeft.collider != null) {
                    float distX = Mathf.Abs(upLeft.point.x - transform.position.x);
                    float distY = Mathf.Abs(upLeft.point.y - transform.position.y);

                    if (distX - playerWidth < Mathf.Abs(currX) * Time.deltaTime) {
                        currX = -distX;
                    }

                    if (distY - playerHeight < Mathf.Abs(currY) * Time.deltaTime) {
                        currY = distY;
                    }
                }
            }

            if (startX > 0 && startY < 0) {
                if (downRight.collider != null) {
                    float distX = Mathf.Abs(downRight.point.x - transform.position.x);
                    float distY = Mathf.Abs(downRight.point.y - transform.position.y);

                    if (distX - playerWidth < Mathf.Abs(currX) * Time.deltaTime) {
                        currX = distX;
                    }

                    if (distY - playerHeight < Mathf.Abs(currY) * Time.deltaTime) {
                        currY = -distY;
                    }
                }
            }

            if (startX < 0 && startY < 0) {
                if (downLeft.collider != null) {
                    float distX = Mathf.Abs(downLeft.point.x - transform.position.x);
                    float distY = Mathf.Abs(downLeft.point.y - transform.position.y);

                    if (distX - playerWidth < Mathf.Abs(currX) * Time.deltaTime) {
                        currX = -distX;
                    }

                    if (distY - playerHeight < Mathf.Abs(currY) * Time.deltaTime) {
                        currY = -distY;
                    }
                }
            }

            if (startX > 0 && startY == 0) {
                if (right.collider != null) {
                    float distX = Mathf.Abs(right.point.x - transform.position.x);

                    if (distX - playerWidth < Mathf.Abs(currX) * Time.deltaTime) {
                        currX = distX;
                    }
                }
            }

            if (startX < 0 && startY == 0) {
                if (left.collider != null) {
                    float distX = Mathf.Abs(left.point.x - transform.position.x);

                    if (distX - playerWidth < Mathf.Abs(currX) * Time.deltaTime) {
                        currX = -distX;
                    }
                }
            }

            if (startX == 0 && startY > 0) {
                if (up.collider != null) {
                    float distY = Mathf.Abs(up.point.y - transform.position.y);

                    if (distY - playerHeight < Mathf.Abs(currY) * Time.deltaTime) {
                        currY = distY;
                    }
                }
            }

            if (startX == 0 && startY < 0) {
                if (down.collider != null) {
                    float distY = Mathf.Abs(down.point.y - transform.position.y);

                    if (distY - playerHeight < Mathf.Abs(currY) * Time.deltaTime) {
                        currY = -distY;
                    }
                }
            }

            //update position or stop dash
            if ((Mathf.Abs(startX) > 0 && Mathf.Abs(currX) <= Mathf.Abs(startX)) || (Mathf.Abs(startY) > 0 && Mathf.Abs(currY) <= Mathf.Abs(startY))) {
                if ((currX > 0 || currX < 0) && Mathf.Abs(moveSpeed) < Mathf.Abs(currX)) {
                    moveSpeed = currX;
                } else {
                    if (Mathf.Sign(moveSpeed) != startX) {
                        moveSpeed = startX;

                        this.transform.position += new Vector3(moveSpeed * Time.deltaTime, 0, 0);
                    }
                }

                if (startY > 0) {
                    gravity = jumpStart / 2;

                    if (startX > 0 || startX < 0) {
                        gravity = jumpStart / 4;
                    }

                    offGround = true;
                }

                dashing = false;
                currX = 0;
                currY = 0;
                startX = 0;
                startY = 0;
            } else {
                this.transform.position += new Vector3(currX * Time.deltaTime, currY * Time.deltaTime, 0);
                currX -= dashDecelX * Time.deltaTime;
                currY -= dashDecelY * Time.deltaTime;
            }
        } else if (onLad) {
            //float hypo = Mathf.Sqrt(Mathf.Pow(ladOrigin.y - this.transform.position.y, 2) + Mathf.Pow(ladOrigin.x - this.transform.position.x, 2));
            //float circum = 2 * Mathf.PI * hypo;
            //float currGrav = (Mathf.Abs(ladOrigin.x - this.transform.position.x) / hypo) * gravMod;

            if (Input.GetKey(KeyCode.W)) {
                this.transform.position += new Vector3(0, climbSpeed * Time.deltaTime, 0);
            }

            if (Input.GetKey(KeyCode.S)) {
                this.transform.position -= new Vector3(0, climbSpeed * Time.deltaTime, 0);
            }

            if (Input.GetKeyDown(KeyCode.Space)) {
                if (jump == 0 || jump == 1) {
                    jump += 1;
                    gravity = jumpStart;
                    offGround = true;
                    onLad = false;

                    if (Input.GetKey(KeyCode.A)) {
                        moveSpeed = -1;
                    } else if (Input.GetKey(KeyCode.D)) {
                        moveSpeed = 1;
                    } else {
                        moveSpeed = 0;
                    }

                    moveSpeed = Mathf.Sign(moveSpeed) * wallJumpMax;
                    this.transform.position += new Vector3(moveSpeed * Time.deltaTime, gravity * Time.deltaTime, 0);
                } else {
                    onLad = false;

                    if (Input.GetKey(KeyCode.A)) {
                        moveSpeed = -1;
                    } else if (Input.GetKey(KeyCode.D)) {
                        moveSpeed = 1;
                    } else {
                        moveSpeed = 0;
                    }

                    moveSpeed = Mathf.Sign(moveSpeed) * wallJumpMax;
                    this.transform.position += new Vector3(moveSpeed * Time.deltaTime, gravity * Time.deltaTime, 0);
                }
            }
        } else {
            //basic left right movement
            if (Input.GetKey(KeyCode.D)) {
                if (jump > 0 || offGround) {
                    moveSpeed += moveAccel * airSpeed * Time.deltaTime;
                } else {
                    keyPressed = true;

                    if (moveSpeed < 0) {
                        moveSpeed += moveAccel * decelMod * Time.deltaTime;
                    } else {
                        moveSpeed += moveAccel * Time.deltaTime;
                    }
                }
            }

            if (Input.GetKey(KeyCode.A)) {
                if (jump > 0 || offGround) {
                    moveSpeed -= moveAccel * airSpeed * Time.deltaTime;
                } else {
                    keyPressed = true;

                    if (moveSpeed > 0) {
                        moveSpeed -= moveAccel * decelMod * Time.deltaTime;
                    } else {
                        moveSpeed -= moveAccel * Time.deltaTime;
                    }
                }
            }

            //decelerate if no keys are pressed
            if (!keyPressed && !offGround && jump == 0) {
                if (moveSpeed > 0) {
                    if (moveSpeed - (decelMod * Time.deltaTime) > 0) {
                        moveSpeed -= decelMod * Time.deltaTime;
                    } else {
                        moveSpeed = 0;
                    }
                } else if (moveSpeed < 0) {
                    if (moveSpeed + (decelMod * Time.deltaTime) < 0) {
                        moveSpeed += decelMod * Time.deltaTime;
                    } else {
                        moveSpeed = 0;
                    }
                }
            }

            //setting jump variables
            if (Input.GetKeyDown(KeyCode.Space) && ((collideLeft || collideRight) && wallJump == 0)) {
                gravity = jumpStart;
                offGround = true;
                wallJump++;

                if (collideLeft) {
                    moveSpeed = 1;
                } else {
                    moveSpeed = -1;
                }

                moveSpeed = Mathf.Sign(moveSpeed) * wallJumpMax;
                this.transform.position += new Vector3(moveSpeed * Time.deltaTime, gravity * Time.deltaTime, 0);
            } else if (Input.GetKeyDown(KeyCode.Space) && (jump == 0 || jump == 1)) {
                jump += 1;
                gravity = jumpStart;
                offGround = true;
            }

            //gravity
            if (jump == 1 || jump == 2 || offGround) {
                if (!collideRight && !collideLeft) {
                    gravity -= gravMod * Time.deltaTime;
                } else {
                    gravity -= gravMod / wallMod * Time.deltaTime;
                }
            }

            //set max speed
            if (moveSpeed > maxSpeed) {
                moveSpeed = maxSpeed;
            } else if (moveSpeed < -maxSpeed) {
                moveSpeed = -maxSpeed;
            }

            //stop on vertical walls
            if (moveSpeed > 0 && collideRight) {
                moveSpeed = 0;
            }

            if (moveSpeed < 0 && collideLeft) {
                moveSpeed = 0;
            }

            //update position based on all the previous things
            this.transform.position += new Vector3(moveSpeed * Time.deltaTime, gravity * Time.deltaTime, 0);

            //dashing code
            if (Input.GetKeyDown(KeyCode.LeftShift) && dash < 1 && Time.time > (dashTime + dashCooldown)) {
                dashing = true;
                dashTime = Time.time;

                //minium dash speed
                if (Mathf.Abs(moveSpeed) < 1) {
                    moveSpeed = 1;
                }
                 
                //determine dash direction
                if (Input.GetKey(KeyCode.W)) {
                    startY = 1;
                } else if (Input.GetKey(KeyCode.S) && offGround) {
                    startY = -1;
                }

                if (Input.GetKey(KeyCode.D)) {
                    startX = 1;
                } else if (Input.GetKey(KeyCode.A)) {
                    startX = -1;
                }

                //dash forward if no buttons pressed
                if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)) {
                    startX = 1;
                }

                //set dash variables
                if (startX != 0) {
                    currX = startX * dashMax;
                }

                if (startY != 0) {
                    currY = startY * dashMax;
                }

                if ((startX > 0 && startY > 0) || (startX < 0 && startY < 0)) {
                    startX = startX * Mathf.Sin(45);
                    startY = startY * Mathf.Sin(45);
                }

                dashDecelX = currX / dashDecelMod;
                dashDecelY = currY / dashDecelMod;
                dash += 1;
            }
        }
    }
}
