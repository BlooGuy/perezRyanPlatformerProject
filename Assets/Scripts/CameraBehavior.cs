using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public Transform player;
    
    
    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.Find("Player").transform;
        
    }

    
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y, -10);
    }
}