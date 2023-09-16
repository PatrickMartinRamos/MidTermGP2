using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class scoreManager : MonoBehaviour
{
    private int score = 0; // Initialize score
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI displayScoreOnGameOverScreen;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the score display
        UpdateScoreText();
    }

    // This method can be called to add score points
    public void AddScore(int points)
    {
        score += points;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
        displayScoreOnGameOverScreen.text = "Score: " + score.ToString();
    }

    public void ResetScore()
    {
        score = 0;
        UpdateScoreText();
    }

}
