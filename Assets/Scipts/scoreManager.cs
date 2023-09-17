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

    // this function is getting called in bullet script
    public void AddScore(int points)
    {
        //add scrore whenever bullet destroy an enemy
        score += points;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        //this method is for the score to reset every time the player restart the game
        scoreText.text = "Score: " + score.ToString();
        displayScoreOnGameOverScreen.text = "Score: " + score.ToString();
    }

    //this function is being called in the button manager restartGame so whenever the player restart the game the score is reset to 0 then update the score text UI
    public void ResetScore()
    {
        score = 0;
        UpdateScoreText();
    }

}
