using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public Transform firePoint;
    public Transform kickPoint;
    public GameObject Bullet;
    public GameObject KickBox;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.L)) 
        {
           
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Kick();
        }

    }

    void Shoot()
    {
        print("pew");
        Instantiate(Bullet, firePoint.position, firePoint.rotation);
    }

    void Kick()
    {
        Instantiate(KickBox, kickPoint.position, kickPoint.rotation);
    }
}
