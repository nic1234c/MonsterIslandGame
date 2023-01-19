using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIHandler : MonoBehaviour
{
   [SerializeField] GameObject titleScreen;
   [SerializeField] GameObject setHighScoreScreen;
   [SerializeField] GameObject viewHighScoreScreen;
   [SerializeField] TMP_InputField name;

	PlayerHealth playerHealth;
	PlayerLevelAttributes playerLevelAttributes;
	
	string highscoreName;
	
	int highscore;
	bool canSetScore;
	[SerializeField] TextMeshProUGUI score;
	
	private void Start() {
		playerHealth = GetComponentInChildren<PlayerHealth>();
		playerLevelAttributes = GetComponent<PlayerLevelAttributes>();

		StartCoroutine(EventTime("Title"));
	}
	
	public void Update() {
		
		if(playerHealth.currentHealth <= 0) {
			StartCoroutine(EventTime("SetHighScore"));
			canSetScore = true;
		}
	}
	
	public void SetHighScore() {
		if(canSetScore) {
			if(PlayerPrefs.GetInt("highscore") < playerLevelAttributes.currentLevel) {
				PlayerPrefs.SetString("highscoreName", name.text);
				PlayerPrefs.SetInt("highscore", playerLevelAttributes.currentLevel);
			}
			score.text = PlayerPrefs.GetString("highscoreName") + "         " + " Lv. " + PlayerPrefs.GetInt("highscore").ToString();
			StartCoroutine(EventTime("ViewHighScore"));
		}
	}
 
	IEnumerator EventTime(string screen) {
		if(screen == "Title") {
		   yield return new WaitForSeconds(3f);
		   titleScreen.SetActive(false);
		}
		else if(screen == "SetHighScore") {
			yield return new WaitForSeconds(1f);
		    setHighScoreScreen.SetActive(true);
		} 
		else if(screen == "ViewHighScore") {
			yield return new WaitForSeconds(1f);
		    viewHighScoreScreen.SetActive(true);
			yield return new WaitForSeconds(6f);
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
   }
}
