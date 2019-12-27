using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	public float highScore;
	private float alltimeHighScore;
	public Text scoreDisplay;
	[SerializeField] private AudioClip loseSound;
	public static Score instance;

	void Awake(){
		instance = this;
	}

	void Start(){
		alltimeHighScore = PlayerPrefs.GetFloat("highScore", 0f);
	}
	
	void Update(){
		float currentHeight = CameraFollow.instance.target.transform.position.y;

		// new high score
		if(currentHeight > highScore){
			highScore = currentHeight;
			
			if(highScore > alltimeHighScore){
				alltimeHighScore = highScore;
			}

			SetDisplayText(highScore);
		}

		if(currentHeight < highScore - CameraFollow.instance.screenHeight/2 - CameraFollow.instance.target.circlingAround.circlingRadius*2){
			LoseGame();
		}
	}

	private void SetDisplayText(float score){
		scoreDisplay.text = "Height: " + Mathf.Round(score).ToString() + " | Best: " + Mathf.Round(alltimeHighScore).ToString();
	}

	public void LoseGame(){
		AudioSource.PlayClipAtPoint(loseSound, Camera.main.transform.position, 0.5f);
		SceneChanger.ChangeScene(0);
		SaveBestScore();
	}

	private void SaveBestScore(){
		float score = 0f;
		if(alltimeHighScore > highScore){
			score = alltimeHighScore;
		}else{
			score = highScore;
		}

		PlayerPrefs.SetFloat("highScore", score);
	}

	void OnApplicationQuit(){
		SaveBestScore();
	}
}
