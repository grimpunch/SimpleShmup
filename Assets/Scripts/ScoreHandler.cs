using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreHandler : MonoBehaviour {

    private int score = 000000;
	private int multiplier = 1;
	
	public float multiplierTimeLeft;
	public float multiplierBaseTimeLeft;
	public float multiplierCountDownRate;
	public float multiplierBaseCountDownRate;
	public float multiplierCountDownRateIncrement;

	private Text scoreText;
	private Text multiplierText;
	private Image multiplierBG;
	private Image multiplierDial;


    public void AddScore(int added) {
		int scoreToAdd = (added * multiplier);
		score += scoreToAdd;
		RaiseMultiplier();
		ResetMultiplierCountdown();
        UpdateScore();
		UpdateMultiplier();
    }

    public void ResetScore() {
        score = 000000;
		ResetMultiplierCountdown();
        UpdateScore();
		UpdateMultiplier();
    }


    private void UpdateScore() {
        scoreText.text = score.ToString("D7") + ""; //Empty string is to avoid automatic line ending of new line.
    }

	private void UpdateMultiplier() {
		if (multiplier == 1) {
			multiplierText.canvasRenderer.SetAlpha(0);
			multiplierBG.canvasRenderer.SetAlpha(0);
			multiplierDial.canvasRenderer.SetAlpha(0);
		}
		else {
			multiplierText.canvasRenderer.SetAlpha(1);
			multiplierBG.canvasRenderer.SetAlpha(1);
			multiplierDial.canvasRenderer.SetAlpha(1);
		}
		multiplierText.text = "X" + multiplier.ToString() + ""; //Empty string is to avoid automatic line ending of new line.
	}

    // Use this for initialization
    void Start() {
        scoreText = GameObject.Find("Score").GetComponent<Text>();
		multiplierText = GameObject.Find("Multiplier").GetComponent<Text>();
		multiplierBG = GameObject.Find("MultiplierTimeDialBG").GetComponent<Image>();
		multiplierDial = GameObject.Find("MultiplierTimeDial").GetComponent<Image>();

    }

	void RaiseMultiplier() {
		switch (multiplier){
			//Multiplier goes to x2 then adds 2 per step.
		case 1: multiplier = multiplier * 2; break;
		default: multiplier = multiplier += 2; break;
		}

		multiplierCountDownRate += multiplierCountDownRateIncrement;
	}

	public void ResetMultiplier ()
	{
		multiplier = 1;
		multiplierCountDownRate = multiplierBaseCountDownRate;
		UpdateMultiplier ();
		ResetMultiplierCountdown ();
	}

	void ResetMultiplierCountdown() {
		multiplierTimeLeft = multiplierBaseTimeLeft;
	}

    // Update is called once per frame
    void Update() {
		if (Utils.Paused) return;
		if (multiplierTimeLeft > 0){
			if (multiplier > 1){
				multiplierTimeLeft -= (Time.deltaTime * multiplierCountDownRate);
				multiplierDial.fillAmount = (multiplierTimeLeft * (1 / multiplierBaseTimeLeft));
				Debug.Log((multiplierTimeLeft * (1 / multiplierBaseTimeLeft)));
			}
		}
		else{
			ResetMultiplier ();
		}
    }
}
