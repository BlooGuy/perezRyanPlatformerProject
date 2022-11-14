using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos; 
    public Transform firePoint;
    public Transform kickPoint;
    public GameObject bullet;
    public GameObject kickBox;
    public GameObject crosshair;
    public bool canFire, canKick;
    private float timer, kickTimer;
    public float timeBetweenFiring;
    public float timeBetweenKicks;
    public AudioClip shootSound;
    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        //method 1
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePos - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        //method 2
        //Vector3 aim = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        //float pew = Mathf.Atan2(aim.y, aim.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(0f, 0f, pew);

        crosshair.transform.position = new Vector2(mousePos.x, mousePos.y);
        if (!canFire)
        {
            timer += Time.deltaTime;
            if(timer > timeBetweenFiring)
            {
                canFire = true;
                timer = 0;
            }
        }
        if (Input.GetMouseButton(0) && canFire)
        {
            canFire = false;
            Instantiate(bullet, firePoint.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(shootSound, transform.position);
        }

        if (!canKick)
        {
            kickTimer += Time.deltaTime;
            if (kickTimer > timeBetweenKicks)
            {
                canKick = true;
                kickTimer = 0;
            }
        }
        if (Input.GetMouseButton(0) && canFire)
        {
            canFire = false;
            Instantiate(bullet, firePoint.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(shootSound, transform.position);
        }

        //if (Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.L)) 
        //{

        //  Shoot();
        //}

        if (Input.GetMouseButtonDown(1) && canKick)
        {
            canKick = false;
          Kick();
        }

    }

    

    void Kick()
    {
        Instantiate(kickBox, kickPoint.position, Quaternion.identity);
    }
}
