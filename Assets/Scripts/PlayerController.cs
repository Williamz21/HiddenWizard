using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public int speed = 2;
    Rigidbody2D rb2D;
    public Animator animator;
    private float dirX = 0;
    private float dirY = 0;
    private enum movemntStatemnt {idle, left, rigth, up, down}
    public SpriteRenderer spriteRend;
    public Projectile head;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
    }

    void updateAnim(){
        movemntStatemnt state;
        if(dirX > 0f){
            state = movemntStatemnt.rigth;
            spriteRend.flipX = false;
        }
        else if(dirX < 0f){
            state = movemntStatemnt.left;
            spriteRend.flipX = true;
        }
        else if(dirY < 0f){
            spriteRend.flipX = false;
            state = movemntStatemnt.down;
        }
        else if(dirY > 0f){
            spriteRend.flipX = false;
            state = movemntStatemnt.up;
        }
        else{
            spriteRend.flipX = false;
            state = movemntStatemnt.idle;
        }
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

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "NextLevel")
        {
            SceneManager.LoadScene(1);
        }
    }
}