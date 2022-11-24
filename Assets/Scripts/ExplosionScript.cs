using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    public AudioClip overallDeath;
    // Start is called before the first frame update
    void Start()
    {
        AudioSource.PlayClipAtPoint(overallDeath, transform.position);
        StartCoroutine(CountDownTimer());
    }


    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator CountDownTimer()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
