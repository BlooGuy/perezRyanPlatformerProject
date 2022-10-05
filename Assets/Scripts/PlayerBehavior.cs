using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public float Speed;
    private int jumpMax = 2; 
    private int jumpBase = 0;
    private bool noJump = false;
    private Vector2 newPosition;
    public Vector2 jumpForce = new Vector2(0, 300); //editable in inspector

    private Rigidbody2D rb2d;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        print(jumpBase + "/" + jumpMax);
        Vector2 newPosition = transform.position;

                bool shouldJump = (Input.GetKeyDown(KeyCode.Space));

        if (shouldJump && !noJump) //! means "not" for  boolean
         {
            print("Jump");
        //reduce velocity and then jump up
        rb2d.velocity = Vector2.zero;
        rb2d.AddForce(jumpForce);
        jumpBase++;
        }
        if (jumpBase == jumpMax)
        {
            noJump = true;
        }

        if (noJump == true)
        {
            shouldJump = false;
        }
   
        if (Input.GetKey(KeyCode.S))
        {
            newPosition += Vector2.down * Time.deltaTime * Speed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            newPosition += Vector2.left * Time.deltaTime * Speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            newPosition += Vector2.right * Time.deltaTime * Speed;
        }

        transform.position = new Vector2(Mathf.Clamp(newPosition.x, -200, 200), newPosition.y);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        jumpBase = 0;
        noJump = false;
    }
}
