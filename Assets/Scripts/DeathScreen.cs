using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    private int sceneToLoad;
    // Start is called before the first frame update
    void Start()
    {
        sceneToLoad = SceneManager.GetActiveScene().buildIndex - 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.R)) 
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        if (Input.GetKey(KeyCode.Escape)) 
        {
            SceneManager.LoadScene(0);
        }
    }
}
