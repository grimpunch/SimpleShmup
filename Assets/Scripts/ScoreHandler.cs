using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreHandler : MonoBehaviour {

    private int score = 000000;
    private Text scoreText;

    public void AddScore(int added) {
        score += added;
        UpdateScore();
    }

    public void ResetScore() {
        score = 000000;
        UpdateScore();
    }


    private void UpdateScore() {
        scoreText.text = score.ToString("D7") + ""; //Empty string is to avoid automatic line ending of new line.
    }

    // Use this for initialization
    void Start() {
        scoreText = GameObject.Find("Score").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update() {

    }
}
