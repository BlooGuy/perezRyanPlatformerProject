using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{
    public static bool isGameOver;
    public int Health = 10;
    public TMP_Text HealthText;
    public GameObject player;
    public GameObject gameOverScreen;
    public GameObject deathCam;
    // Start is called before the first frame update
    void Start()
    {
        HealthText.text = "Health:" + Health.ToString();
        
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
        Health--;
        HealthText.text = "Health: " + Health.ToString();
        
        if (Health <= 0)
        {
            player.SetActive(false);
            gameOverScreen.SetActive(true);
            deathCam.SetActive(true);
        }
    }
}
