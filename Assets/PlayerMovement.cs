using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    public Rigidbody2D camera;
    public float jumpSpeed;
    private int jumpsLeft;
    private float sinceLast;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpsLeft = 2;
    }

    // Update is called once per frame
    void Update()
    {
        camera.velocity = rb.velocity;


        if(Input.GetKey(KeyCode.W) && jumpsLeft > 0 && sinceLast > 0.3){
            Debug.Log("jumping");
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            jumpsLeft = jumpsLeft - 1;
            sinceLast = 0;
        }else{
            sinceLast += Time.deltaTime;
        }

        
    }

    void OnCollisionEnter2D(Collision2D collision){
        Debug.Log(collision);
        if (collision.gameObject.tag == "floor"){
            jumpsLeft = 2;
            Debug.Log("touched floor");
        }
    }
}
