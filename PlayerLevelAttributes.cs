using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerLevelAttributes : MonoBehaviour
{
	private int baseLevel = 1;
    public int currentLevel;
	
	private int baseMaxHealth = 100;
    public int currentMaxHealth;
	
	private int baseStrength = 25;
    public int currentStrength;
	
	private int baseSorcery = 40;
	public int currentSorcery;
	
	private int baseExpNeedToLevelUp = 100;
	public int expNeedToLevelUp;
	ExpController expController;
	int exp;
	PlayerHealth playerHealth;
	InfoEvent infoEvent;
	
	[SerializeField] TextMeshProUGUI levelText;
	
	[SerializeField] TextMeshProUGUI strengthText;
	[SerializeField] TextMeshProUGUI healthText;
	[SerializeField] TextMeshProUGUI sorceryText;
	[SerializeField] TextMeshProUGUI toNextLevel;

	

	void Start() {
		expController = GetComponentInChildren<ExpController>();
		playerHealth = GetComponentInChildren<PlayerHealth>();
		infoEvent = GetComponent<InfoEvent>();

		playerHealth.SetMaxHealth(currentMaxHealth);

		currentLevel = baseLevel;
		exp = 0;
		SetLevelAttributes();
	}
	
	void Update() {	
		LevelUp();
		CheckForDeath();
		SetTextAttributes();
	}
	
	private void LevelUp() {
		exp = expController.GetExp();
		if(exp >= expNeedToLevelUp) {
			exp = 0;
			currentLevel += 1;
			levelText.text = "Lv. "+ currentLevel.ToString();
			infoEvent.triggerEvent("Level Up");
			SetLevelAttributes();
			playerHealth.SetMaxHealth(currentMaxHealth);
			expController.SetCurrentExp(0);
			expController.SetExp(expNeedToLevelUp);
		}
	}
	
	private void CheckForDeath() {
		if(playerHealth.currentHealth <= 0) {
			infoEvent.triggerEvent("YOU HAVE PERISHED");
		}
	}
	
	private void SetTextAttributes() {
	
		healthText.text = "Max Health " + currentMaxHealth.ToString();
		strengthText.text = "Strength " + currentStrength.ToString();
		sorceryText.text = "Sorcery " + currentSorcery.ToString();
		toNextLevel.text = "Total Experience Needed " + expNeedToLevelUp.ToString();

	}
	
	void SetLevelAttributes() {
		currentLevel = baseLevel * currentLevel;
		
		currentMaxHealth = baseMaxHealth * currentLevel;
		
		currentStrength = baseStrength + (currentLevel * 2);
			
		currentSorcery = baseSorcery + (currentLevel * 2);
		
		expNeedToLevelUp = baseExpNeedToLevelUp * currentLevel;
	}
}
