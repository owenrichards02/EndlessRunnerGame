using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    public int score;
    public float scoreMultiplier;
    public bool alive;
    private TMP_Text scoreText;
    private float timeSince;
    void Start()
    {
        scoreMultiplier = 1;
        timeSince = 0.0f;
        scoreText = GetComponent<TextMeshProUGUI>();
        alive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (alive){
            timeSince += Time.deltaTime;
            score = (int)(scoreMultiplier * (50.0f * timeSince));
            scoreText.text = "SCORE: " + score; 
        }
        
    }

}
