using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float Speed = 20f;
    public int damage = 10;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            rb.velocity = transform.right * Speed;
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            rb.velocity = (transform.right * Speed) * -1;
        }
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        EnemyBehavior enemy = hitInfo.GetComponent<EnemyBehavior>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
