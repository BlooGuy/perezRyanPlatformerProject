using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuController : MonoBehaviour
{

    public AudioClip buttonClick;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    public void StartGame()
    {
        AudioSource.PlayClipAtPoint(buttonClick, transform.position);
        SceneManager.LoadScene(1);
        
    }

    public void QuitGame()
    {
        AudioSource.PlayClipAtPoint(buttonClick, transform.position);
        Application.Quit();
    }

    public void How2Play()
    {
        AudioSource.PlayClipAtPoint(buttonClick, transform.position);
    }

    public void OpenCodex()
    {
        AudioSource.PlayClipAtPoint(buttonClick, transform.position);
    }
    
}
