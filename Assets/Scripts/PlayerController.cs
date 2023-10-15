using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public int speed = 5;
    Rigidbody2D rb2D;
    public Animator animator;
    private float dirX = 0;
    private float dirY = 0;
    private enum movemntStatemnt {idle, left, rigth, up, down, upleft, uprigth}
    public SpriteRenderer spriteRend;
    public int lives = 3;
    public int mode = 0; //0:normal  1:fuego  2:tiempo
    /*public Projectile head;*/

    // Start is called before the first frame update
    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
    }

    void updateAnim(){
        movemntStatemnt state = 0;
        if(dirX == 0 && dirY == 0){
            state = movemntStatemnt.idle;
        }
        else if(dirX > 0f && dirY > 0f){
            state = movemntStatemnt.uprigth;
        }
        else if(dirX < 0f && dirY > 0f){
            state = movemntStatemnt.upleft;
        }
        else if(dirX > 0f && dirY <= 0f){
            state = movemntStatemnt.rigth;
        }
        else if(dirX < 0f && dirY <= 0f){
            state = movemntStatemnt.left;
        }
        else if(dirY < 0f){
            state = movemntStatemnt.down;
        }
        else if(dirY > 0f){
            state = movemntStatemnt.up; 
        }
        animator.SetInteger("mode", mode);
        animator.SetInteger("state", (int)state);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        dirX = Input.GetAxis("Horizontal");
        dirY = Input.GetAxis("Vertical");
        Vector2 movimiento = new Vector2(dirX, dirY) * speed;
        rb2D.velocity = movimiento;
        updateAnim();

    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.Tab) && dirX ==0f  && dirY ==0f){
            if(mode == 2)
                mode = 0;
            else
                mode++;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "NextLevel")
        {
            SceneManager.LoadScene(1);
        }
    }

}