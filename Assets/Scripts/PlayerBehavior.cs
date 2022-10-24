using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehavior : MonoBehaviour
{
    public float Speed, horizontal;
    private int jumpMax = 2; 
    private int jumpBase = 0;
    private bool noJump = false;
    private Vector2 newPosition;
    public Vector2 jumpForce = new Vector2(0, 300); //editable in inspector
    public Transform firePoint;
    public Transform kickPoint;
    public GameObject bulletPrefab;
    public GameObject kickPrefab;
    private GameObject gameEnd;
    
    private bool isFacingRight = true;

    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 24f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 0.5f;

    [SerializeField] private TrailRenderer tr;


    private Rigidbody2D rb2d;
    // Start is called before the first frame update
    void Start()
    {
        
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (isDashing)
        {
            return;
        }
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
        if (Input.GetKey(KeyCode.A))
        {
            newPosition += Vector2.left * Time.deltaTime * Speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            newPosition += Vector2.right * Time.deltaTime * Speed;
        }
        transform.position = new Vector2(Mathf.Clamp(newPosition.x, -200, 200), newPosition.y);
        
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }

        Flip();
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        rb2d.velocity = new Vector2(horizontal * Speed, rb2d.velocity.y);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        jumpBase = 0;
        
        noJump = false;
        
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
}
