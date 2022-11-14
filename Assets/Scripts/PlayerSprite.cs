using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprite : MonoBehaviour
{
    
    private bool isFacingRight;
    public Transform crosshair;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        Flip();



    }

    private void Flip()
    {
        if (isFacingRight && transform.position.x < crosshair.position.x || !isFacingRight && transform.position.x > crosshair.position.x)
        {
            Vector3 localScale = transform.localScale;
            isFacingRight = !isFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

}
