using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour {

    private Text scoreText;
    private int score;
	
    void Awake()
    {
        scoreText = GetComponent<Text>();
    }

    void Update()
    {
        scoreText.text = score.ToString(); ;
    }

    public void ChangeScore(int addToScore)
    {

        // handle min and max scores
        if (score + addToScore < 0)
        {
            score = 0;
            addToScore = 0;
        }else if( score + addToScore > 999999)
        {
            score = 999999;
            addToScore = 0;
        }

        score += addToScore;
    }

}
