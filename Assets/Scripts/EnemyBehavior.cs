using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    private GameController gameController;
    public float speed, range, timeShot, bulletSpeed;
    private float playerDistance;

    public bool onPatrol;
    private bool mustTurn, canShoot;
    public Rigidbody2D rb;
    public float timeToDie = 1.6f;
    
    public Transform groundCheckPos;
    public LayerMask groundLayer;
    public Collider2D bodyCollider;
    public Transform shootPos;
    private Transform player;


    public int hp = 100;
    public GameObject deathEffect;
    public GameObject bullet;

    public AudioClip enemyHurt;
    public AudioClip enemyDie;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        onPatrol = true;
        canShoot = true;
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        AudioSource.PlayClipAtPoint(enemyHurt, transform.position);
        if (hp <= 0)
        {
            timeShot = 999;
            range = 0;
            speed = 0;
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        AudioSource.PlayClipAtPoint(enemyDie, transform.position);
        GetComponent<Animator>().SetBool("dying", true);
        yield return new WaitForSeconds(timeToDie);
        Instantiate(deathEffect, transform.position, Quaternion.identity );
        Destroy(gameObject);
    }

    
// Update is called once per frame
void Update()
    {
        

        float v = Vector2.Distance(transform.position, player.position);
        playerDistance = v;

        if (playerDistance <= range)
        {
            if (player.position.x > transform.position.x && transform.localScale.x < 0
                || player.position.x < transform.position.x && transform.localScale.x > 0)
            {
                Flip();
            }

            onPatrol = false;
            rb.velocity = Vector2.zero;
            if (canShoot)
            {
                StartCoroutine(Shoot());
            }
        }
        else
        {
            onPatrol = true;
            GetComponent<Animator>().SetBool("shooting", false);
        }
    }

    private void FixedUpdate()
    {
       
        if (onPatrol)
        {
            rb.velocity = new Vector2(speed * Vector2.right.x * Time.deltaTime, rb.velocity.y);
            if (mustTurn || bodyCollider.IsTouchingLayers(groundLayer))
            {
                Flip();
            }
            mustTurn = Physics2D.OverlapCircle(groundCheckPos.position, 0.5f, groundLayer);
        }

    }

    //void Patrol()
    //{
      //  rb.velocity = new Vector2(speed * Vector2.right.x * Time.deltaTime, rb.velocity.y);
        //if(mustTurn || bodyCollider.IsTouchingLayers(groundLayer))
        //{
           // Flip();
        //}
    //}

    void Flip()
    {
        onPatrol = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        speed *= -1;
        onPatrol = true;
    }

    IEnumerator Shoot()
    {
        GetComponent<Animator>().SetBool("shooting", true);
      
        canShoot = false;
        yield return new WaitForSeconds(timeShot);
        GameObject newBullet = Instantiate(bullet, shootPos.position, Quaternion.identity);
        newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed * speed * Time.fixedDeltaTime, 0f);
        canShoot = true;
    }
}
