using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject menuPause;
    [SerializeField] private GameObject menu;
    public int speed = 5;
    Rigidbody2D rb2D;
    public Animator animator;
    private float dirX = 0;
    private float dirY = 0;
    private enum movemntStatemnt { idle, left, rigth, up, down, upleft, uprigth }
    public SpriteRenderer spriteRend;
    public float lives = 3f;
    public int mode = 0; //0:normal  1:fuego  2:tiempo
    [SerializeField] public Lifebar lifebar;
    private bool canMove = true;
    public bool died = false;
    public bool fire = false;
    public bool time = false;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        if(SceneManager.GetActiveScene().name == "Level1"){
            
        }
        else{
            this.LoadPlayer();
        }
    }

    public void GameOver(){
        menu.SetActive(true);
        Time.timeScale = 0f;
        menuPause.SetActive(false);
    }


    public void SavePlayer(){
        SaveSystem.SavePlayer(this);
    }
    public void LoadPlayer(){
        PlayerData data = SaveSystem.LoadPlayer();
        speed = data.speed;
        died = data.died;
        fire = data.fire;
        time = data.time;
        mode = data.mode;
        lives = data.lives;
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
        animator.SetInteger("mode", mode);
        animator.SetInteger("state", (int)state);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(died){
            return;
        }
        if (canMove)
        {
            dirX = Input.GetAxis("Horizontal");
            dirY = Input.GetAxis("Vertical");
            Vector2 movimiento = new Vector2(dirX, dirY) * speed;
            rb2D.velocity = movimiento;
            updateAnim();
        }
        if (lives <= 0 && rb2D.simulated==true)
        {
            animator.SetBool("died", true);
            rb2D.simulated = false;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && dirX == 0f && dirY == 0f)
        {
            if(fire && time){
                if (mode == 2)
                    mode = 0;
                else
                    mode++;
            }
            else if(fire){
                if (mode == 1)
                    mode = 0;
                else
                    mode++;
            }
            else if(time){
                if(mode == 2)
                    mode = 0;
                else
                    mode = 2;
            }
        }
    }

    public void rebote(Vector2 point)
    {
        Vector2 velocity = new Vector2(5f, 5f);
        rb2D.velocity = new Vector2(-velocity.x * point.x, -velocity.y * point.y);
    }

    private IEnumerator LoseControl()
    {
        canMove = false;
        yield return new WaitForSeconds(0.5f);
        canMove = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "NextLevel")
        {
            SceneManager.LoadScene(1);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Target")
        {
            lives-=1;
            lifebar.ChangeLife(lives);
            StartCoroutine(LoseControl());
        }

    }
    public void gainObject(int item_id){
        if (item_id == 1)
        {
            fire = true;
        }
        if (item_id == 2)
        {
            time = true;
        }
        this.SavePlayer();
    }
    public void damage(Transform projectile){
        lives--;
        lifebar.ChangeLife(lives);
        StartCoroutine(LoseControl());
    }
}