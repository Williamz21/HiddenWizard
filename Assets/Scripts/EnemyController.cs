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
    public float vida = 40;
    private Rigidbody2D rb2D;
    private AIPath aIPath;
    private int effect = 0;
    private int timer = 0;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        aIPath = GetComponent<AIPath>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (vida == 0)
        {
            animator.SetBool("dying", true);
            rb2D.simulated = false;
        }
        UpdateAnimation();
        UpdateEffect();
    }

    private void UpdateEffect(){
        if(timer == 40 && effect == 2){
            aIPath.maxSpeed = aIPath.maxSpeed/2;
            timer--;
        }
        else if(timer == 0 && effect == 2){
            aIPath.maxSpeed = aIPath.maxSpeed*2;
            effect = 0;
        }
        else{
            timer--;
            UnityEngine.Debug.LogError(effect);
        }
        if(timer > 0 && effect == 1){
            timer--;
        }
        else if(timer <= 0 && effect == 1){
            effect = 0;
        }
        else if(timer % 5 == 0 && effect == 1){
            vida--;
        }
    }

    public void setEffect(int m){
        if(m == 1){
            effect = 1;
            timer = 80;
        }
        if(m == 2){
            effect = 2;
            timer = 40;
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
            state = movementState.rightup;
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
        animator.SetInteger("state", (int)state);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
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
            UnityEngine.Debug.LogError('A');
        }
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}