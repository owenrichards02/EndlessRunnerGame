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
    private float sinceLastJump;
    private float sinceLastSlowDown;
    private float slowTimer;
    public GameObject slowMoText;
    [SerializeField] private AudioSource jumpingSound;
    [SerializeField] private AudioSource deathSound;
    private AudioSource gameSound;
    

    private GroundMovement gm;
    private GameController gc;

    private Animator anim;

    // Tutorial UI elements
    public GameObject[] popUps;
    private int popUpIndex;

    void Start()
    {
        thisCollider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        gc = GetComponent<GameController>();
        jumpsLeft = 2;
        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        camera.velocity = rb.velocity;

        if(Input.GetKey(KeyCode.W) && jumpsLeft > 0 && sinceLastJump > 0.25){
            Debug.Log("jumping");
            anim.SetBool("isOnGround", false);
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            jumpingSound.Play();
            if (jumpsLeft == 2){
                anim.SetTrigger("onFirstJump");
            }else {
                anim.SetTrigger("onSecondJump");
            }
            jumpsLeft = jumpsLeft - 1;
            sinceLastJump = 0;
        }else{
            sinceLastJump += Time.deltaTime;
        }


        //TimeWarp stuff
        slowMoText.SetActive(sinceLastSlowDown > 7.0f);

        if(Input.GetKey(KeyCode.Q) && sinceLastSlowDown > 7.0f){
            Time.timeScale = 0.5f;
            sinceLastSlowDown = 0.0f;
            slowTimer = 0.0f;
        }

        if (Time.timeScale == 1.0f){
            sinceLastSlowDown += Time.deltaTime;
        }{
            slowTimer += Time.deltaTime;
        }

        if (slowTimer > 3.0f){
            Time.timeScale = 1.0f;
        }

    }

    void OnCollisionEnter2D(Collision2D collision){
        Debug.Log(collision);
        Collider2D collider = collision.collider;
        Vector2 center = collider.bounds.center;
        Vector2 contactPoint = collision.contacts[0].point;
        float lefSide = center.x - (collider.bounds.size.x/2);
        bool isValid = contactPoint.y > center.y && contactPoint.x > lefSide;
        
        // edited this if statement so I get the tags for the tutorial
        if ((collision.gameObject.tag == "floor"||collision.gameObject.tag=="lastsinglejump"||collision.gameObject.tag=="lastdoublejump"||collision.gameObject.tag=="lasttimewarp") && isValid){
            anim.SetBool("isOnGround", true);
            jumpsLeft = 2;
            Debug.Log("touched floor");
        }else{
            GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>() ;
            foreach(GameObject go in allObjects){ 
                if (go.activeInHierarchy && (go.tag=="floor" || go.tag=="lastsinglejump" || go.tag=="lastdoublejump"||collision.gameObject.tag=="lasttimewarp")){
                    gm=go.GetComponent<GroundMovement>();
                    gm.groundSpeed=0.0f;
                } 
            }
            // dont need gc.died();
            gc.groundMultiplier = 0;

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
            gc.died();
            deathSound.Play();
            // gameSound = (AudioSource)GameObject.Find("Game");
            // gameSound.Stop();
            //GameObject.Find("Player").SetActive(false);
            GetComponent<AudioSource>().Stop();
        }
    }

    }
