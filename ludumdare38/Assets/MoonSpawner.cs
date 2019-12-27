using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonSpawner : MonoBehaviour {

	public List<Moon> moons = new List<Moon>();
	[SerializeField] List<Sprite> moonSprites;

	[SerializeField] private GameObject moonObject;

	private float currentHeight;
	private int moonsSpawned = 0;

	public static MoonSpawner instance;

	void Awake(){
		instance = this;
	}

	void Start(){
		moons.AddRange(GameObject.FindObjectsOfType<Moon>());

		for(int i = 0; i < 10; i++){
			SpawnMoon();
		}
	}

	public void SpawnMoon(Vector3? pos = null){

		Vector3 spawnPos = Vector3.zero;
		if(pos == null){
			currentHeight += CameraFollow.instance.screenHeight * Random.Range(0.6f, 0.8f);

			float xoffset = Random.Range(-CameraFollow.instance.screenWidth/2, CameraFollow.instance.screenWidth/2);
			spawnPos = new Vector3(CameraFollow.instance.transform.position.x + xoffset, currentHeight, 0f);
		}else{
			Vector3 vec = (Vector3)pos;
			spawnPos.Set(vec.x, vec.y, 0f);
		}

		GameObject moon = Instantiate(moonObject, spawnPos, Quaternion.identity);
		moon.GetComponent<SpriteRenderer>().sprite = moonSprites[Random.Range(0,moonSprites.Count)];

		//scale
		float scale = Random.Range(0.3f, 0.8f) * CameraFollow.instance.screenWidth.Scale(0f,15f,0.5f,1f);
		moon.transform.localScale = new Vector3(scale, scale);

		moons.Add(moon.GetComponent<Moon>());

		moonsSpawned++;
	}

	public void DestroyMoon(Moon m){
		moons.Remove(m);
		Destroy(m.gameObject);
		SpawnMoon();
	}
}
