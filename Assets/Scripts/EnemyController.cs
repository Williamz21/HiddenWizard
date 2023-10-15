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
        UpdateAnimation();
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
            vida--;
        }
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}