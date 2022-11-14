using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    private GameController gameController;

    public AudioClip pickupSound;
    // Start is called before the first frame update
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.name == "PlayerParent")
        {
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);
            gameController.GainHealth();
            Die();
        }
        
    }
    // Update is called once per frame
    void Update()
    {

    }
    void Die()
    {

        Destroy(gameObject);
    }
}
