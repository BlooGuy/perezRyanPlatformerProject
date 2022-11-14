using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.TerrainTools;

public class GameController : MonoBehaviour
{
    private int deathScreen;
    public int health = 20;
    public TMP_Text healthText;
    private GameObject player;
    public GameObject deathMark;
    public GameObject deathFade;
    public AudioClip playerPain;
    public float timeSoundDie;
    public float deathTimer = 3;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //deathMark = GameObject.FindGameObjectWithTag("DeathMark");
        deathScreen = SceneManager.GetActiveScene().buildIndex + 1;
        healthText.text = "Health:" + health.ToString();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
        else if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        
    }

    public void LoseLife()
    {
        health--;
        healthText.text = "Health: " + health.ToString();
        AudioSource.PlayClipAtPoint(playerPain, player.transform.position);
        
        if (health <= 0)
        {
            deathFade.SetActive(true);
            deathMark.SetActive(true);
            StartCoroutine(DeathMark());

        }
    }

    public void GainHealth()
    {
        health = health + 1;
        healthText.text = "Health: " + health.ToString();
    }
        IEnumerator DeathMark()
    {
        yield return new WaitForSeconds(deathTimer);
        SceneManager.LoadScene(deathScreen);
    }

    
    IEnumerator CountDownTimer()
    {
        yield return new WaitForSeconds(timeSoundDie);
        Destroy(playerPain);
    }
}
