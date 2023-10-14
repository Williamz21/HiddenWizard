using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;
using UnityEngine.UIElements;
using UnityEditorInternal;
using Pathfinding;
using TMPro;

public class EnemyController : MonoBehaviour
{
    public float speed;
    private GameObject player;
    private float distance;
    private Vector2 direction;
    private Animator animator;
    private enum movementState { idle, left, right, up, down }
    public float vida = 10;
    private Rigidbody2D rigidbody2D;
    private AIPath aIPath;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Isaac");
        rigidbody2D = GetComponent<Rigidbody2D>();
        aIPath = GetComponent<AIPath>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (vida <= 0)
        {
            animator.SetBool("dying", true);
            rigidbody2D.simulated = false;
        UpdateAnimation();
    }

    void UpdateAnimation()
    {
        movementState state;
        /*movementState state;
        if(direction.x > 0f){
            state = movementState.right;
        }
        else if(direction.x < 0f){
            state = movementState.left;
        }
        else if(direction.y < 0f){
            state = movementState.down;
        }
        else if(direction.y > 0f){
            state = movementState.up;
        }
        else{
            state = movementState.idle;
        }
        animator.SetInteger("state", (int)state);*/
        Debug.Log(aIPath);
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
        animator.SetInteger("state", (int)state);
    }

    void OnTriggerEnter2D(Collider2D other)
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
}