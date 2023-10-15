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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 direccionEmpujon = (transform.position - collision.transform.position).normalized;
        rb2D.AddForce(direccionEmpujon * 999f, ForceMode2D.Impulse);
        UnityEngine.Debug.LogError("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
        // Verifica si la colisión ocurrió con el objeto deseado (por ejemplo, usando etiquetas).
        if (collision.gameObject.tag == "Target")
        {
            UnityEngine.Debug.LogError("AAAAAAAAA");
            // Calcula la dirección del empujón desde el objeto que colisionó hacia el objeto actual.
            //Vector2 direccionEmpujon = (transform.position - other.transform.position).normalized;
            // Aplica la fuerza de empujón al objeto actual.
            rb2D.AddForce(direccionEmpujon * 999f, ForceMode2D.Impulse);
        }
    }

}