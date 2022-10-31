using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamBehavior : MonoBehaviour
{

    private GameController gameController;
    private GameObject Player;
    public float dieTime, damage;
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

