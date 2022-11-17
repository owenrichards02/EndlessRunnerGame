using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerMovement pm;
    private ScoreDisplay sd;
    private GroundSpawning gs;
    private GameObject player;
    private GameObject spawner;
    private GroundMovement gm;
    private ScaleBackg sbg;
    private DepthOfField dof = null;
    public Volume vol;

    private float multiplier;
    public float maxMultiplier;
    private float camMultiplier;
    public float groundMultiplier;

    void Start()
    {
        multiplier = 1.0f;
        player = GameObject.Find("Player");
        spawner = GameObject.Find("Ground Spawner");
        sd = GameObject.Find("ScoreText").GetComponent<ScoreDisplay>();
        pm = player.GetComponent<PlayerMovement>();
        gs = spawner.GetComponent<GroundSpawning>();
        gm = GameObject.Find("test ground 1").GetComponent<GroundMovement>(); //MAKE SURE TO MAKE THIS WORK LOL
        sbg = GameObject.Find("Backg").GetComponent<ScaleBackg>();
        groundMultiplier = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (multiplier < maxMultiplier){
            multiplier += Time.deltaTime * 0.025f;
            if (multiplier > maxMultiplier){multiplier=maxMultiplier;}
        }
        camMultiplier = (multiplier - 1) / 2 + 1;

        //increase speed
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>() ;
        foreach(GameObject go in allObjects){ 
            if (go.activeInHierarchy && go.tag=="floor"){
                gm=go.GetComponent<GroundMovement>();
                gm.groundSpeed = multiplier * gm.groundSpeedDefault * groundMultiplier;
            }else if (go.activeInHierarchy && go.tag=="enemy"){
                gm=go.GetComponent<GroundMovement>();
                gm.groundSpeed = multiplier * gm.groundSpeedDefault * groundMultiplier;
            }
            if (go.transform.position.x < -200){
                go.SetActive(false);
            }
        }
        if (dof!=null){
            if (dof.focusDistance.value > 1.0f){
                dof.focusDistance.value -=1f;
            }
        }

        gs.spawnInterval = gs.spawnIntervalDefault / multiplier; //decrease spawn interval
        sd.scoreMultiplier = multiplier; // increase score

        //cameraZoom
        Camera.main.orthographicSize = 10 * camMultiplier;
        sbg.sizeMultiplier = camMultiplier;
        Debug.Log("multi: " + multiplier);
    }

    public void died(){
        Debug.Log("dead g");
        //temp reload
        //Scene scene = SceneManager.GetActiveScene();
        //SceneManager.LoadScene(scene.name);
        vol.profile.TryGet<DepthOfField>(out dof);
        //need to set dof=null on the reload
    }
}
