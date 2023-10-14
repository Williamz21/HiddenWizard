using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor.Compilation;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    public float tear_speed = 4f;
    private float time = 0;
    private float range;
    public Rigidbody2D rb;
    private int mode; //0: palo,  1:Fuego,  2:Veneno,   3:Tiempo
    private enum movemntStatemnt {left, rigth, up, down}
    private bool colision = false;
    // Start is called before the first frame update
    void Start()
    {
       
    }
    public void defineDirection(int stat, float ran, Vector2 velocityPlayer){
        range = ran;
        if(stat == 0){
            rb.velocity = transform.right * -tear_speed;
        }
        if(stat == 1){
            rb.velocity = transform.right * tear_speed;
        }
        if(stat == 2){
            rb.velocity = transform.up * tear_speed;
        }
        if(stat == 3){
            rb.velocity = transform.up * -tear_speed;
        }
        rb.velocity = rb.velocity + (velocityPlayer/2);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(time >= range){
            Destroy(gameObject);
        }
        time++;
        if(colision){
            Destroy(gameObject);
        }

    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Target")
        {
            colision = true;
        } 
    }
}
