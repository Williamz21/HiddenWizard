using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    public float tear_speed = 8f;
    private float time = 0;
    private float range;
    public Rigidbody2D rb;
    public int mode; //0: default,  1:Fuego,  2:Tiempo
    private enum movemntStatemnt { left, rigth, up, down }
    private bool colision = false;
    // Start is called before the first frame update
    void Start()
    {

    }
    public void defineDirection(int stat, float ran, Vector2 velocityPlayer)
    {
        range = ran;
        if (stat == 0)
        {
            rb.velocity = transform.right * -tear_speed;
        }
        if (stat == 1)
        {
            rb.velocity = transform.right * tear_speed;
        }
        if (stat == 2)
        {
            rb.velocity = transform.up * tear_speed;
        }
        if (stat == 3)
        {
            rb.velocity = transform.up * -tear_speed;
        }
        rb.velocity = rb.velocity + (velocityPlayer / 2);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (time >= range)
        {
            Destroy(gameObject);
        }
        time++;
        if (colision)
        {
            Destroy(gameObject);
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Target")
        {
            if (other.GetComponent<EnemyController>() != null)
            {
                other.GetComponent<EnemyController>().setEffect(mode);
                UnityEngine.Debug.Log(mode);
                Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
