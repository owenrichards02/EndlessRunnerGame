using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerMovement pm;
    private ScoreDisplay sd;
    private GroundSpawning gs;
    private GameObject player;
    private GameObject spawner;

    void Start()
    {
        player = GameObject.Find("Player");
        spawner = GameObject.Find("gs");
        sd = GameObject.Find("ScoreText").GetComponent<ScoreDisplay>();
        pm = player.GetComponent<PlayerMovement>();
        gs = spawner.GetComponent<GroundSpawning>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void died(){
        Debug.Log("dead g");
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
