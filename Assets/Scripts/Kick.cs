using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kick : MonoBehaviour
{
    private Vector3 mousePos;
    private Camera mainCam;
    public int damage;
    public float speed = 20f;
    public Rigidbody2D rb;
    public float dieTime;
    public AudioClip kickSound;
    public AudioClip kickHit;
    // public GameObject 
    void Start()
    {
        AudioSource.PlayClipAtPoint(kickSound, transform.position);
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        Vector3 rotation = transform.position - mousePos;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * speed;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot);
        StartCoroutine(CountDownTimer());
    }

 

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        EnemyBehavior enemy = hitInfo.GetComponent<EnemyBehavior>();
        if (enemy != null)
        {
            AudioSource.PlayClipAtPoint(kickHit, transform.position);
            enemy.TakeDamage(damage /2);
        }
        BreakableWallsBehavior wall = hitInfo.GetComponent<BreakableWallsBehavior>();
        if (wall != null)
        {
            wall.TakeDamage(damage * 10);
        }

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
