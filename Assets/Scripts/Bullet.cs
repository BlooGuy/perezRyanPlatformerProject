using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 mousePos;
    private Camera mainCam;
    public float speed = 20f;
    public int damage = 10;
    public float lifeTime = 1f;
    public Rigidbody2D rb;
    private GameObject player;
    public AudioClip bulletHit;

    [SerializeField] private TrailRenderer tr;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();   
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        Vector3 rotation = transform.position - mousePos;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * speed;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
        
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo != player)
        {
            EnemyBehavior enemy = hitInfo.GetComponent<EnemyBehavior>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            BreakableWallsBehavior wall = hitInfo.GetComponent<BreakableWallsBehavior>();
            if (wall != null)
            {
                wall.TakeDamage(damage / 2);
            }
            Destroy(gameObject);
        }
        AudioSource.PlayClipAtPoint(bulletHit, transform.position); 
    }
    // Update is called once per frame
    void Update()
    {
        StartCoroutine(BulletTime());
    }

    private IEnumerator BulletTime()
    {
       tr.emitting = true;
        yield return new WaitForSeconds(lifeTime);
       tr.emitting = false;
        Destroy(gameObject);
    }
}
