using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillZoneBehavior : MonoBehaviour
{
    private int deathScreen;
    private GameObject player;
    public GameObject deathMark;
    public GameObject deathFade;
    public float deathTimer = 3;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        deathScreen = SceneManager.GetActiveScene().buildIndex + 1;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "PlayerParent")
        {
            deathFade.SetActive(true);
            deathMark.SetActive(true);
            StartCoroutine(DeathMark());
        }
        
    }

    // Update is called once per frame
    void Update()
    {

    }

        
  
    IEnumerator DeathMark()
    {
        yield return new WaitForSeconds(deathTimer);
        SceneManager.LoadScene(deathScreen);
    }
}
