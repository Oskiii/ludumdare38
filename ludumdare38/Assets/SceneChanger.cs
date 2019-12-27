using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {

	public static void ChangeScene(int scene){
		SceneManager.LoadScene(scene);
	}

	public static void ChangeScene(string scene){
		SceneManager.LoadScene(scene);
	}
}
