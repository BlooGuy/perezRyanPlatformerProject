using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Rocket : MonoBehaviour
{
    private GameController gameController;
    private Transform player;
    private Rigidbody2D rb;
    public float dieTime, damage;
    public float Speed = 5;
    public float rotateSpeed = 200f;
    public AudioClip rocketShoot;
    public AudioClip rocketBoom;

    // public GameObject 
    void Start()
    {
        AudioSource.PlayClipAtPoint(rocketShoot, transform.position);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        gameController = FindObjectOfType<GameController>();
        StartCoroutine(CountDownTimer());
    }

    void FixedUpdate()
    {
        Vector2 direction = (Vector2)player.position - rb.position;

        direction.Normalize();


        float rotateAmount = Vector3.Cross(direction, transform.up).z;

        rb.angularVelocity = -rotateAmount * rotateSpeed;
        rb.velocity = transform.up * Speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
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
        AudioSource.PlayClipAtPoint(rocketBoom, transform.position);
        Destroy(gameObject);
    }
}
