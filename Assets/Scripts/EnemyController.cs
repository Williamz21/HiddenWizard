using System.Diagnostics;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;
using UnityEngine.UIElements;
using Pathfinding;
using TMPro;

public class EnemyController : MonoBehaviour
{
    public float speed;
    private float distance;
    private Vector2 direction;
    private Animator animator;
    private enum movementState { idle, leftup, rightup, leftdown, rightdown }
    public float vida = 1000;
    private Rigidbody2D rb2D;
    private AIPath aIPath;
    private int effect = 0;
    public float distanciaMaxima = 10.0f;
    private int timer = 0;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        aIPath = GetComponent<AIPath>();
        speed = aIPath.maxSpeed;
        aIPath.enabled = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(aIPath.enabled == false){
            GameObject jugador = GameObject.Find("Player");
            float distancia = Vector2.Distance(transform.position, jugador.transform.position);
            if (distancia <= distanciaMaxima)
            {
                aIPath.enabled = true;
            }
        }
        if (vida <= 0 && rb2D.simulated == true)
        {
            Transform a = transform.GetChild(0);
            Animator anim = a.GetComponent<Animator>();
            anim.SetInteger("state", 0);
            animator.SetBool("dying", true);
            rb2D.simulated = false;
        }
        if(rb2D.simulated == true){
            UpdateAnimation();
            UpdateEffect();
        }
    }

    private void UpdateEffect(){
        if(timer == 130 && effect == 2){
            aIPath.maxSpeed = speed/2;
            timer--;
        }
        else if(timer == 0 && effect == 2){
            aIPath.maxSpeed = speed;
            effect = 0;
        }
        else{
            timer--;
        }
        if(timer > 0 && effect == 1){
            timer--;
        }
        else if(timer <= 0 && effect == 1){
            effect = 0;
        }
        else if(timer % 20 == 0 && effect == 1){
            vida--;
        }
        if(timer > 0){
            timer--;
        }
    }

    public void setEffect(int m){
        if(m == 1){
            effect = 1;
            timer = 200;
        }
        if(m == 2){
            if(effect ==2){
                aIPath.maxSpeed = speed;
            }
            effect = 2;
            timer = 130;
        }
    }

    void UpdateAnimation()
    {
        movementState state;
        if (aIPath.desiredVelocity.x < 0f && aIPath.desiredVelocity.y > 0f)
        {
            state = movementState.leftup;
        }
        else if (aIPath.desiredVelocity.x > 0f && aIPath.desiredVelocity.y > 0f)
        {
            state = movementState.rightup;;
        }
        else if (aIPath.desiredVelocity.x > 0f && aIPath.desiredVelocity.y < 0f)
        {
            state = movementState.rightdown;
        }
        else if (aIPath.desiredVelocity.x < 0f && aIPath.desiredVelocity.y < 0f)
        {
            state = movementState.leftdown;
        }   
        else
        {
            state = movementState.idle;
        }
        Transform a = transform.GetChild(0);
        Animator anim = a.GetComponent<Animator>();
        anim.SetInteger("state", effect);
        animator.SetInteger("state", (int)state);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Projectile")
        {
            vida-=3;
        }
        if (other.tag == "Projectile")
        {
            vida-=3;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().rebote(other.GetContact(0).normal);
        }
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}