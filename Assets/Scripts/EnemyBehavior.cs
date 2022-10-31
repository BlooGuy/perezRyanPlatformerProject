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
    
    public Transform groundCheckPos;
    public LayerMask groundLayer;
    public Collider2D bodyCollider;
    public Transform player, shootPos;



    public int hp = 100;
    public GameObject deathEffect;
    public GameObject bullet;

    public AudioClip enemyDie;

    // Start is called before the first frame update
    void Start()
    {
        onPatrol = true;
        canShoot = true;
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        AudioSource.PlayClipAtPoint(enemyDie, transform.position);
        Instantiate(deathEffect, transform.position, Quaternion.identity );
        Destroy(gameObject);
    }

    
// Update is called once per frame
void Update()
    {
        if (onPatrol)
        {
            Patrol();
        }

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
        }
    }

    private void FixedUpdate()
    {
        if (onPatrol)
        {
            mustTurn = Physics2D.OverlapCircle(groundCheckPos.position, 0.1f, groundLayer);
        }

    }

    void Patrol()
    {
        rb.velocity = new Vector2(speed * Time.fixedDeltaTime, rb.velocity.y);
        if(mustTurn || bodyCollider.IsTouchingLayers(groundLayer))
        {
            Flip();
        }
    }

    void Flip()
    {
        onPatrol = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        speed *= -1;
        onPatrol = true;
    }

    IEnumerator Shoot()
    {
        canShoot = false;
        yield return new WaitForSeconds(timeShot);
        GameObject newBullet = Instantiate(bullet, shootPos.position, Quaternion.identity);
        newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed * speed * Time.fixedDeltaTime, 0f);
        canShoot = true;
    }
}
