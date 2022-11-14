using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWallsBehavior : MonoBehaviour
{

    public int hp = 100;
    public GameObject deathEffect;
    

    public AudioClip wallHurt;
    public AudioClip wallDie;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        AudioSource.PlayClipAtPoint(wallHurt, transform.position);
        if (hp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        AudioSource.PlayClipAtPoint(wallDie, transform.position);
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
