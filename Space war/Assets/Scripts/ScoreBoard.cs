using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreBoard : MonoBehaviour
{
    int score;

    TMP_Text scoreText;

    private void Start() {
        scoreText = GetComponent<TMP_Text>();
        scoreText.text = "Start";
    }

    public void IncrementScore(int addedPoints){
        score += addedPoints;
        scoreText.text = score.ToString();

    }
}
