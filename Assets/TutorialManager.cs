using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] popUps; // popups are a list of tutorial instructions
    private int popUpIndex; // index of instructions in the tutorial
    //public PlayerMovement pm;
    private int jumpsLeft;
    //private DepthOfField dof = null;
    public GameObject rt;
    // Start is called before the first frame update
    void Start()
    {
        // we start in the UI saying press W to jump so:
        popUpIndex=0;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i =0; i<popUps.Length;i++){

            if(i==popUpIndex){
                popUps[i].SetActive(true); // press W to jump instruction activated here
            }else{
                popUps[i].SetActive(false); // press W twice or hold for double jump activated here
            }
        }
        
        if(popUpIndex==4){
            // this thing is test purposes only, after making the death just like in the game, we have to put it inside.
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            Scene scene = SceneManager.GetActiveScene();
            if (Input.GetKey(KeyCode.R)){
                SceneManager.LoadScene(scene.name);
            }
            Scene scene2 = SceneManager.GetSceneByName("SampleScene");
            if (Input.GetKey(KeyCode.G)){
                SceneManager.LoadScene("SampleScene");

            }
        }
        
        
        
          
    }

    void OnCollisionEnter2D(Collision2D collision){
        Collider2D collider = collision.collider;

        // For the single jump
        if(popUpIndex==0){
            if(collision.gameObject.tag=="lastsinglejump"){
                popUpIndex++;
                Debug.Log("double jump UI instructions here");
            }
            // For the double jump
        }else if(popUpIndex==1){
            Debug.Log("incrementing");
            if(collision.gameObject.tag=="lastdoublejump"){
                popUpIndex++;
                Debug.Log("more UI instructions here if we want to add something to the tutorial");
            }
        }else if(popUpIndex==2){
            if(collision.gameObject.tag=="lasttimewarp"){
                popUpIndex++;
                Debug.Log("entered time warp zone");
            }
            
            
        }
    }

    

    void OnTriggerEnter2D(Collider2D collider){
        if (collider.gameObject.tag == "death"){
            Debug.Log("We're in the death zone of the tutorial");
            popUpIndex = 4;
        }
    }
    
    
}