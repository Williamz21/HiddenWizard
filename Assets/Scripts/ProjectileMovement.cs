using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor.Compilation;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    public float tear_speed = 4f;
    public float time = 0;
    public float range;
    public Rigidbody2D rb;
    private enum movemntStatemnt {left, rigth, up, down}
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
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Target")
        {
            Destroy(gameObject);
        }
    }
}
