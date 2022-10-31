using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Rocket : MonoBehaviour
{
    private GameController gameController;
    public Transform Player;
    private Rigidbody2D rb;
    public float dieTime, damage;
    public float Speed = 5;
    public float rotateSpeed = 200f;

    public AudioClip enemyDie;
    // public GameObject 
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        gameController = FindObjectOfType<GameController>();
        StartCoroutine(CountDownTimer());
    }

    void FixedUpdate()
    {
        Vector2 direction = (Vector2)Player.position - rb.position;

        direction.Normalize();


        float rotateAmount = Vector3.Cross(direction, transform.up).z;

        rb.angularVelocity = -rotateAmount * rotateSpeed;
        rb.velocity = transform.up * Speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {

            gameController.LoseLife();

        }
        Die();
    }
    // Update is called once per frame
    void Update()
    {
        //float step = Speed * Time.deltaTime;
        //transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, step);
    }

    IEnumerator CountDownTimer()
    {
        yield return new WaitForSeconds(dieTime);
        Die();
    }

    void Die()
    {
        
        Destroy(gameObject);
    }
}
