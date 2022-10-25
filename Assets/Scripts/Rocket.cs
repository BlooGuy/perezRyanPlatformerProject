using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private GameController gameController;
    private GameObject Player;
    public float dieTime, damage;
    public float Speed = 5;
    // public GameObject 
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        gameController = FindObjectOfType<GameController>();
        StartCoroutine(CountDownTimer());
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
        float step = Speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, step);
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
