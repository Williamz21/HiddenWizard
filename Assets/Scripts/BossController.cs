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
    [SerializeField] private GameObject menuPause;
    [SerializeField] private GameObject win;
    public float speed;
    private float distance;
    private float dirX;
    private float dirY;
    private Vector2 direction;
    private Animator animator;
    private enum movemntStatemnt { idle, left, rigth, up, down, upleft, uprigth }
    public float vida = 1000;
    private Rigidbody2D rb2D;
    public Collider2D colider;
    private int effect = 0;
    public float distanciaMaxima = 10.0f;
    private int timer = 0;
    private bool canInvoke = false;
    float tiempoTranscurrido = 25f;
    float intervaloDeTiempo = 100f; // 30 segundos
    private bool falling = false;

    [SerializeField] private LifeBossBAR lifeBossBAR;

    public EnemyController enemyPrefab;

    public GameObject player;
    public SpriteRenderer render;
    public AIPath aiPath;
    public GameObject projectilePrefub;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        colider = GetComponent<Collider2D>();
        aiPath = GetComponent<AIPath>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        dirX = aiPath.velocity.x;
        dirY = aiPath.velocity.y;
        updateAttack();
        if (vida <= 0 && rb2D.simulated == true)
        {
            animator.SetBool("died", true);
            rb2D.simulated = false;
        }
        tiempoTranscurrido += 1;
        if (tiempoTranscurrido >= intervaloDeTiempo)
        {
            float distancia = Vector2.Distance(transform.position, player.transform.position);
            if(distancia > 8){
                jumpAttack();
            }
            tiempoTranscurrido = 0;
        }
        updateAnim();   
    }
    private void shoot()
    {
        GameObject proyectil = Instantiate(projectilePrefub, transform.position, Quaternion.identity);
        Vector2 direccion = (player.transform.position - transform.position).normalized;
        proyectil.GetComponent<BossProjectile>().defineDirectionVector(55, direccion);
    }
    private void updateAttack(){
        
        if(falling && vida >0){
            tiempoTranscurrido++;
            if(tiempoTranscurrido == 15){
                transform.position = player.transform.position;
            }
            if(tiempoTranscurrido > 15 && tiempoTranscurrido < 50){
                UnityEngine.Debug.LogError(transform.localScale.x);
                transform.localScale = new Vector2(transform.localScale.x+ 0.05f, transform.localScale.y + 0.05f);
            }
            else if(tiempoTranscurrido >= 50){
                colider.isTrigger = false;
                falling = false;
                tiempoTranscurrido = 0;
                aiPath.enabled = true;
                transform.localScale.Set(1, 1, 1);
                transform.localScale = new Vector2(1,1);
            }
            animator.SetInteger("state", 0);
            animator.SetBool("falling",falling);
            return;
        }
        else if (vida > 0){
            tiempoTranscurrido++;
            if(tiempoTranscurrido == 15){
                shoot();
                tiempoTranscurrido = 0;
            }
        }
    }
    private void jumpAttack(){
        animator.SetInteger("state", 0);
        falling = true;
        tiempoTranscurrido = 0;
        colider.isTrigger = true;
        aiPath.enabled = false;
        animator.SetBool("falling",falling);
    }
    
    void updateAnim()
    {
        movemntStatemnt state = 0;
        if (dirX == 0 && dirY == 0)
        {
            state = movemntStatemnt.idle;
        }
        else if (dirX > 0f && dirY > 0f)
        {
            state = movemntStatemnt.uprigth;
        }
        else if (dirX < 0f && dirY > 0f)
        {
            state = movemntStatemnt.upleft;
        }
        else if (dirX > 0f && dirY <= 0f)
        {
            state = movemntStatemnt.rigth;
        }
        else if (dirX < 0f && dirY <= 0f)
        {
            state = movemntStatemnt.left;
        }
        else if (dirY < 0f)
        {
            state = movemntStatemnt.down;
        }
        else if (dirY > 0f)
        {
            state = movemntStatemnt.up;
        }
        animator.SetInteger("state", (int)state);
    }

    private IEnumerator LoseControl()
    {
        yield return new WaitForSeconds(1f);
        Time.timeScale = 0f;
        win.SetActive(true);
    }

    public void WIN(){
        menuPause.SetActive(false);
        StartCoroutine(LoseControl());
    }

    private void Invoke()
    {
        EnemyController obj = Instantiate(enemyPrefab, new Vector3(transform.position.x, transform.position.y - 3f, transform.position.z), transform.rotation);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Projectile")
        {
            vida -= 50;
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
