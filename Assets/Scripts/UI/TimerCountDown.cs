using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class TimerCountDown : MonoBehaviour
{

	public TextMeshProUGUI textDisplay;
	public TextMeshProUGUI ZombiesText;
	public float gameTime;
	public float timer;
	private bool stopTimer;

	private AudioSource audioSource;
	public AudioSource timeOut;
	private void Start()
	{
		stopTimer = false;
		timer = gameTime;

	}

	private void Update()
	{
			UpdateTimer();
		if(Data.numberOfZombies==0)
		{
			Data.endGameText = "You Survived";
			SceneManager.LoadScene("EndScene");
		}
	}

	private void UpdateTimer()
	{
		timer -= Time.deltaTime;
		int minutes = Mathf.FloorToInt(timer / 60);
		int seconds = Mathf.FloorToInt(timer - minutes * 60f);
		string textTime = string.Format("{0:00}:{1:00}", minutes, seconds);
		if (timer <= 0)
		{
			stopTimer = true;

			timer = 0;
			Data.endGameText = "Game Over";
			SceneManager.LoadScene("EndScene");
		}
		if (!stopTimer)
		{
			textDisplay.SetText(textTime);
			ZombiesText.text = "Remaining Zumbies : " + Data.numberOfZombies;
		}
	}
	

	
}
