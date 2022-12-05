using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehavior : MonoBehaviour
{
    public float speed, horizontal;
    private int jumpMax = 2; 
    private int jumpBase = 0;
    private bool noJump = false;
    private Vector2 newPosition;
    public Vector2 jumpForce = new Vector2(0, 300); //editable in inspector
   
    
    
    private GameObject gameEnd;
    private GameObject ground;
    private Vector3 mousePos;
    private Camera mainCam;

    private bool isFacingRight = true;

    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 40f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 0.5f;
    public AudioClip dashSound, jumpSound, landSound;
    

    [SerializeField] private TrailRenderer tr;


    private Rigidbody2D rb2d;
    // Start is called before the first frame update
    void Start()
    {
        ground = GameObject.FindGameObjectWithTag("GroundLayer");
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb2d = GetComponentInChildren<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        if (isDashing)
        {
            return;
        }
        //print(jumpBase + "/" + jumpMax);
        Vector2 newPosition = transform.position;

                bool shouldJump = (Input.GetKeyDown(KeyCode.Space));

        if (shouldJump && !noJump) //! means "not" for  boolean
         {
            AudioSource.PlayClipAtPoint(jumpSound, transform.position);
            //print("Jump");
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
        if (Input.GetKey(KeyCode.A))
        {
            GetComponentInChildren<Animator>().SetBool("walking", true);
            newPosition += Vector2.left * Time.deltaTime * speed;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            GetComponentInChildren<Animator>().SetBool("walking", false);
        }
            if (Input.GetKey(KeyCode.D))
        {
            GetComponentInChildren<Animator>().SetBool("walking", true);
            newPosition += Vector2.right * Time.deltaTime * speed;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            GetComponentInChildren<Animator>().SetBool("walking", false);
        }
        transform.position = new Vector2(Mathf.Clamp(newPosition.x, -1000, 1000), newPosition.y);
        
        if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKey(KeyCode.D) && canDash)
        {
            AudioSource.PlayClipAtPoint(dashSound, transform.position);
            StartCoroutine(Dash());
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKey(KeyCode.A) && canDash)
        {
            AudioSource.PlayClipAtPoint(dashSound, transform.position);
            StartCoroutine(LeftDash());
        }
        
        //Flip();
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        rb2d.velocity = new Vector2(horizontal * speed, rb2d.velocity.y);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if the object i touch is labelled as "ground", refresh my jumps
        {
            if (collision.gameObject.tag == "GroundLayer") 
            {
                AudioSource.PlayClipAtPoint(landSound, transform.position);
                jumpBase = 0;

                noJump = false;
            }
            
        }
        //if (collision.gameObject.name == "HealthPickup")
        //{
        //  ;
        //}

    }

    

    private void Flip()
    {
        if(isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            Vector3 localScale = transform.localScale;
            isFacingRight = !isFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb2d.gravityScale;
        rb2d.gravityScale = 0f;
        
        rb2d.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb2d.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;


    }

    private IEnumerator LeftDash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb2d.gravityScale;
        rb2d.gravityScale = 0f;

        rb2d.velocity = new Vector2(-transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb2d.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;


    }
}
