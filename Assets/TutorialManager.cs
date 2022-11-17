using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] popUps; // popups are a list of tutorial instructions
    private int popUpIndex; // index of instructions in the tutorial
    public PlayerMovement pm;
    private int jumpsLeft;
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
            if(collision.gameObject.tag=="lastdoublejump"){
                popUpIndex++;
                Debug.Log("more UI instructions here if we want to add something to the tutorial");
            }
        }
    }

    
}
