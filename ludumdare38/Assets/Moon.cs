using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moon : MonoBehaviour {

	public float radius{
		get{
			if(ren != null){
				return ren.bounds.extents.magnitude;
			}else{
				return 0;
			}
		}
	}

	public float circlingRadius{
		get{
			return radius*1.85f;
		}
	}

	private SpriteRenderer ren;
	[SerializeField] private SpriteRenderer mouth;
	[SerializeField] private Sprite exertSprite;
	[SerializeField] private Sprite smileSprite;

	void Awake()
	{
		ren = GetComponent<SpriteRenderer>();
	}

	void Start(){
		InvokeRepeating("CheckDestroy", 2f, 2f);
		SetEyesAndHands();
	}

	private void SetEyesAndHands(){
		LookAt2D[] eyes = GetComponentsInChildren<LookAt2D>();
		Color[] pixels = ren.sprite.texture.GetPixels();
		Color handColor = pixels[pixels.Length/2];
		handColor.a = 1f;

		foreach(LookAt2D eye in eyes){
			eye.lookAtTarget = CameraFollow.instance.target.transform;

			if(eye.name == "Hands"){
				int childcount = eye.transform.childCount;

				for(int i = 0; i < childcount; i++){
					eye.transform.GetChild(i).GetComponent<SpriteRenderer>().color = handColor;
				}
			}
		}
	}

	public void CheckDestroy(){
		GameObject player = FindObjectOfType<Player>().gameObject;

		//if moon way off screen
		if(((player.transform.position - transform.position)).y > CameraFollow.instance.screenBottom.y){
			MoonSpawner.instance.DestroyMoon(this);
			//MoonSpawner.instance.SpawnMoon();
		}
	}

	public void Exert(){
		if(mouth != null){
			mouth.sprite = exertSprite;
		}
	}

	public void Relax(){
		if(mouth != null){
			mouth.sprite = smileSprite;
		}
	}
}
