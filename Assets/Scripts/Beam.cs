using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{
    private GameController gameController;
    private GameObject player;
    private Rigidbody2D rb;
    public float dieTime, damage, force;
    public AudioClip bulletSound;
   // public GameObject 
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        gameController = FindObjectOfType<GameController>();
        StartCoroutine(CountDownTimer());
        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
        AudioSource.PlayClipAtPoint(bulletSound, transform.position);
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
