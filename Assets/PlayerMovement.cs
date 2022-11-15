using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    private Collider2D thisCollider;
    [SerializeField] private new Rigidbody2D camera;
    public float jumpSpeed;
    private int jumpsLeft;
    private float sinceLast;

    public GroundMovement gm;

    private Animator anim;

    void Start()
    {
        thisCollider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        jumpsLeft = 2;
    }

    // Update is called once per frame
    void Update()
    {
        camera.velocity = rb.velocity;

        if(Input.GetKey(KeyCode.W) && jumpsLeft > 0 && sinceLast > 0.3){
            Debug.Log("jumping");
            anim.SetBool("isOnGround", false);
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            if (jumpsLeft == 2){
                anim.SetTrigger("onFirstJump");
            }else {
                anim.SetTrigger("onSecondJump");
            }
            jumpsLeft = jumpsLeft - 1;
            sinceLast = 0;
        }else{
            sinceLast += Time.deltaTime;
        }

    }

    void OnCollisionEnter2D(Collision2D collision){
        Debug.Log(collision);
        Collider2D collider = collision.collider;
        Vector2 center = collider.bounds.center;
        Vector2 contactPoint = collision.contacts[0].point;
        float lefSide = center.x - (collider.bounds.size.x/2);
        bool isValid = contactPoint.y > center.y && contactPoint.x > lefSide;
        if (collision.gameObject.tag == "floor" && isValid){
            anim.SetBool("isOnGround", true);
            jumpsLeft = 2;
            Debug.Log("touched floor");
        }else{
            GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>() ;
            foreach(GameObject go in allObjects){ 
                if (go.activeInHierarchy && go.tag=="floor"){
                    gm=go.GetComponent<GroundMovement>();
                    gm.groundSpeed=0.0f;
                } 
            }

            jumpsLeft = 0;
            collider.enabled=false;
        }

    }

    void OnCollisionExit2D(Collision2D collision){
        anim.SetBool("isOnGround", false);
        if (jumpsLeft == 2){
                jumpsLeft = 1;
        }

    }

    void OnTriggerEnter2D(Collider2D collider){
        if (collider.gameObject.tag == "death"){
            //do death stuff
            Debug.Log("dead g");
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }

}
