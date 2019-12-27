using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public Moon circlingAround;

	public bool shouldRotate = true;

	public Vector3 moveDirection;
	[SerializeField] private AudioClip jumpSound;
	public float flySpeed = 5;

	private float speedMultiplier = 1;

	private bool moveDirectionCalculated = false;
	public bool rotationDirectionCalculated = false;

	private float currentFloatTime = 0f;
	private float maxFlyBeforeLoseAtStart = 5f;
	private float maxFlyBeforeLose = 5f;

	private SpriteRenderer ren;

	public float rotationDirection;

	void Awake(){
		ren = GetComponent<SpriteRenderer>();
	}

	void Start(){
		circlingAround = FindClosestMoon();
	}

	void FixedUpdate(){
		if(shouldRotate){
			if(!rotationDirectionCalculated && circlingAround != null){
				rotationDirection = GetRotationDirection();
				rotationDirectionCalculated = true;
			}

			Rotate(rotationDirection);
		}else{
			if(!moveDirectionCalculated){

				Vector2 v = transform.position - circlingAround.transform.position;
				moveDirection = new Vector2(-v.y, v.x) / Mathf.Sqrt(v.x*v.x + v.y*v.y) * -rotationDirection;
				moveDirectionCalculated = true;
			}

			Fly(moveDirection);
		}
	}

	void Update(){
		HandleInput();
		CheckBounds();

		if(!shouldRotate){
			currentFloatTime += Time.deltaTime;
			if(currentFloatTime > maxFlyBeforeLose){
				Score.instance.LoseGame();
			}
		}
	}

	private void CheckBounds(){
		Vector3 area = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0.0f, 0.0f));

		Vector3 playerPosScreen = transform.position;
		float half_szX = ren.bounds.size.x / 2;
   		float half_szY = ren.bounds.size.y /2 ;

		if (playerPosScreen.x >= (area.x+half_szX)) {
			playerPosScreen.x = -(area.x-half_szX);
			transform.position = playerPosScreen;
		}

		if(playerPosScreen.x <= -(area.x+half_szX)){
			playerPosScreen.x = (area.x+half_szX);
			transform.position = playerPosScreen;
		}
	}

	private void Rotate(float dir){
		moveDirectionCalculated = false;

		if(circlingAround == null){
			return;
		}

		transform.position = (transform.position - circlingAround.transform.position).normalized 
		* circlingAround.circlingRadius 
		+ circlingAround.transform.position;

		transform.RotateAround(circlingAround.transform.position, Vector3.back, dir * flySpeed * speedMultiplier);
		transform.rotation = Quaternion.identity;
	}

	private float GetRotationDirection(){
		//compare vector to moon and movedir
        return ((circlingAround.transform.position - transform.position).y < moveDirection.y) ? -1.0f : 1.0f;
	}

	private void Fly(Vector3 dir){

		Moon target = FindClosestMoon();
		rotationDirectionCalculated = false;		

		if(target != null){
			
			// if close to moon, attach
			if(Vector3.Distance(transform.position, target.transform.position) < target.circlingRadius){
				circlingAround.Relax();
				circlingAround = target;
				target.Exert();
				shouldRotate = true;
				currentFloatTime = 0f;
				
				speedMultiplier = 1f + 1f/100000f*Mathf.Pow(transform.position.y,2);
				maxFlyBeforeLose = maxFlyBeforeLoseAtStart / speedMultiplier;
				return;
			}
		}

		transform.position += dir.normalized * flySpeed * speedMultiplier * Time.deltaTime;
	}

	private void HandleInput(){
		if((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && Time.timeSinceLevelLoad > 0.3f){
			Jump();
		}
	}

	private void Jump(){
		shouldRotate = false;
		AudioSource.PlayClipAtPoint(jumpSound, transform.position, Random.Range(0.95f, 1.05f));
	}

	//can't return current moon
	public Moon FindClosestMoon(){
		List<Moon> moons;
		moons = new List<Moon>(GameObject.FindObjectsOfType<Moon>());
		Moon closest = null;
		float distance = Mathf.Infinity;
		Vector3 position = transform.position;

		//remove current moon
		moons.Remove(circlingAround);

		/*if(moons.Exists(x => (x.transform.position - transform.position).magnitude < x.circlingRadius) == false){
			return null;
		}*/

		foreach (Moon go in moons)
		{
			Vector3 diff = go.transform.position - position;
			float curDistance = diff.sqrMagnitude;
			if (curDistance < distance)
			{
				closest = go;
				distance = curDistance;
			}
		}

		return closest;
	}
}
