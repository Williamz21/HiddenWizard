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

public class BossController : MonoBehaviour
{
    public float speed;
    private float distance;
    private Vector2 direction;
    private Animator animator;
    private enum movementState { idle, leftup, rightup, leftdown, rightdown }
    public float vida = 1000;
    private Rigidbody2D rb2D;
    private int effect = 0;
    public float distanciaMaxima = 10.0f;
    private int timer = 0;
    private bool canInvoke = false;
    float tiempoTranscurrido = 25f;
    float intervaloDeTiempo = 30f; // 30 segundos

    [SerializeField] private LifeBossBAR lifeBossBAR;

    public EnemyController enemyPrefab;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (vida <= 0 && rb2D.simulated == true)
        {
            Transform a = transform.GetChild(0);
            Animator anim = a.GetComponent<Animator>();
            anim.SetInteger("state", 0);
            animator.SetBool("died", true);
            rb2D.simulated = false;
        }
        // Resta el tiempo delta desde la variable timeRemaining.
        tiempoTranscurrido += Time.deltaTime;

        // Verifica si ha pasado el intervalo de tiempo
        if (tiempoTranscurrido >= intervaloDeTiempo)
        {
            // Llama a la función que quieres ejecutar
            Invoke();

            // Reinicia el temporizador
            tiempoTranscurrido = 0f;
        }
        /*if (rb2D.simulated == true)
        {
            UpdateAnimation();
        }*/
    }


    /*void UpdateAnimation()
    {
        movementState state;
        if (aIPath.desiredVelocity.x < 0f && aIPath.desiredVelocity.y > 0f)
        {
            state = movementState.leftup;
        }
        else if (aIPath.desiredVelocity.x > 0f && aIPath.desiredVelocity.y > 0f)
        {
            state = movementState.rightup; ;
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
    }*/

    private IEnumerator LoseControl()
    {
        canInvoke = true;
        yield return new WaitForSeconds(10f);
        canInvoke = false;
    }

    private void Invoke()
    {
        EnemyController obj = Instantiate(enemyPrefab, new Vector3(transform.position.x, transform.position.y - 3f, transform.position.z), transform.rotation);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Projectile")
        {
            vida -= 20;
            lifeBossBAR.ChangeLife(vida);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().rebote(other.GetContact(0).normal);
        }
    }
}
