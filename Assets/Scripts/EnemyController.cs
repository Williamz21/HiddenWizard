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
    public float vida = 10;
    private Rigidbody2D rb2D;
    private AIPath aIPath;

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
        /*else{
            distance= Vector2.Distance(transform.position, player.transform.position);
            direction = player.transform.position - transform.position;
            direction.Normalize();
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            
        }*/
        UpdateAnimation();
    }

    void UpdateAnimation()
    {
        /*movementState state;
        if (aIPath.desiredVelocity.x >= 0.01f)
        {
            state = movementState.right;
        }
        else if (aIPath.desiredVelocity.x <= -0.01f)
        {
            state = movementState.left;
        }
        else if (aIPath.desiredVelocity.y <= 0.01f)
        {
            state = movementState.up;
        }
        else if (aIPath.desiredVelocity.y >= -0.01f)
        {
            state = movementState.down;
        }   
        else
        {
            state = movementState.idle;
        }
        animator.SetInteger("state", (int)state);*/
    }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Projectile")
            {
                vida--;
            }
        }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}