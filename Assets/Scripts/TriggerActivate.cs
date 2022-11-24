using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerActivate : MonoBehaviour
{
    public GameObject trigger;
    // Start is called before the first frame update
    void Start()
    {
        trigger.SetActive(false);
    }

  
    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D()
    {
        trigger.SetActive(true);
    }
}
