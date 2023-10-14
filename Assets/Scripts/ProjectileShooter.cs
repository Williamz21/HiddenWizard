using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    public Transform weapon;
    public GameObject projectilePrefab;
    public float tear_delay = 24f;
    public float range = 30f;
    private float delay = 0.0f;
    private enum movemntStatemnt {left, rigth, up, down}
    public SpriteRenderer spriteRend;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(delay <= 0)
            Shoot();
        else
            delay--;
        //updateAnim();
    }
    void Shoot(){
        movemntStatemnt state=0;
        Rigidbody2D rbPlayer = gameObject.GetComponentInParent<Rigidbody2D>();
        Vector2 velocityPlayer = rbPlayer.velocity;
        if(Input.GetKey("left")){
            GameObject obj = Instantiate(projectilePrefab, new Vector3(weapon.position.x, weapon.position.y - 0.5f, weapon.position.z), weapon.rotation);
            state = movemntStatemnt.left;
            obj.GetComponent<ProjectileMovement>().defineDirection((int)state, range, velocityPlayer);
            delay=tear_delay;
        }
        else if(Input.GetKey("right")){
            GameObject obj = Instantiate(projectilePrefab, new Vector3(weapon.position.x, weapon.position.y - 0.5f, weapon.position.z), weapon.rotation);
            state = movemntStatemnt.rigth;
            obj.GetComponent<ProjectileMovement>().defineDirection((int)state, range, velocityPlayer);
            delay=tear_delay;
        }
        else if(Input.GetKey("up")){
            GameObject obj = Instantiate(projectilePrefab, new Vector3(weapon.position.x, weapon.position.y - 0.5f, weapon.position.z), weapon.rotation);
            state = movemntStatemnt.up;
            obj.GetComponent<ProjectileMovement>().defineDirection((int)state, range, velocityPlayer);
            delay=tear_delay;
        }
        else if(Input.GetKey("down")){
            GameObject obj = Instantiate(projectilePrefab, new Vector3(weapon.position.x, weapon.position.y - 0.5f, weapon.position.z), weapon.rotation);
            state = movemntStatemnt.down;
            obj.GetComponent<ProjectileMovement>().defineDirection((int)state, range, velocityPlayer);
            delay=tear_delay;
        }
    }
    void updateAnim(){
        int state = 0;// 0: idle   2: Horizontal    3: Down    1:Up
        if(Input.GetKey("right")){
            state = 2;
            spriteRend.flipX = false;
        }
        else if(Input.GetKey("left")){
            state = 2;
            spriteRend.flipX = true;
        }
        else if(Input.GetKey("up")){
            spriteRend.flipX = false;
            state = 1;
        }
        else if(Input.GetKey("down")){
            spriteRend.flipX = false;
            state = 3;
        }
        else{
            spriteRend.flipX = false;
            state = 0;
        }
        animator.SetInteger("state", state);
    }
}