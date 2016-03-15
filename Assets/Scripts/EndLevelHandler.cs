using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class EndLevelHandler : MonoBehaviour
{
    public GameObject endLevelTextTitle;
    public GameObject endLevelScoreTitle;
    public GameObject endLevelScoreField;

	// Use this for initialization
	void Start()
	{
        GameObject.Find("MusicManager").GetComponent<MusicManager>().QueueTrack("EndLevelLoop");
        GameObject.Find("GamePlayArea").GetComponent<ScrollLevelForward>().stopped=true;
        GameManager.GameManagerInstance.UpdateScoreForEndOfLevel(GameObject.Find("Score").GetComponent<ScoreHandler>().GetScore());
        endLevelScoreField.GetComponent<Text>().text = GameObject.Find("Score").GetComponent<ScoreHandler>().GetScoreAsString();
        GameManager.GameManagerInstance.gameState = Utils.GameState.EndLevelMenu;
        endLevelTextTitle.SetActive(true);
        StartCoroutine(ActivationRoutine());
	}
  private IEnumerator ActivationRoutine()
  {        
    yield return new WaitForSeconds(5);

    endLevelScoreTitle.SetActive(true);

    yield return new WaitForSeconds(1);

    endLevelScoreField.SetActive(true);
  }
}
