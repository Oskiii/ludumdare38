using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour {

	[SerializeField] private RectTransform pausePanel;
	private bool gamePaused = false;

	void Start(){
		SetPaused(true);
	}

	void Update(){
		if(Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)){
			TogglePause();
		}
	}

	private void TogglePause(){
		SetPaused(!gamePaused);
	}

	public void SetPaused(bool paused){
		if(paused == true){
			Time.timeScale = 0;
			pausePanel.gameObject.SetActive(true);
		}else{
			Time.timeScale = 1;
			pausePanel.gameObject.SetActive(false);
		}

		gamePaused = paused;
	}

	void OnApplicationFocus(bool focus){
		SetPaused(!focus);
	}
}
