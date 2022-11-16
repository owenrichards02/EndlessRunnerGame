using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    private int score;
    public int scoreMultiplier;
    private TMP_Text scoreText;
    private float timeSince;
    void Start()
    {
        scoreMultiplier = 1;
        timeSince = 0.0f;
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        timeSince += Time.deltaTime;
        score = scoreMultiplier * (int)(50.0f * timeSince);
        scoreText.text = "SCORE: " + score; 
    }

}
