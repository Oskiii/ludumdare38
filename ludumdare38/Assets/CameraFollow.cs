using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Player target;

	public static CameraFollow instance;

	public Vector3 screenTop{
		get{
			return Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2, 0f, 0f));
		}
	}

	private Vector3 _screenBottom;
	public Vector3 screenBottom{
		get{
			if(_screenBottom == Vector3.zero){
				screenBottom = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2, Screen.height, 0f));
			}

			return _screenBottom;
		}

		private set{
			_screenBottom = value;
		}
	}

	public float screenHeight{
		get{
			return screenBottom.y;
		}
	}

	public float screenWidth{
		get{
			return Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f)).x;
		}
	}

	void Awake(){
		instance = this;
	}

	void Start(){
		screenBottom = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2, Screen.height, 0f));
	}

	void FixedUpdate()
	{
		float x;
		if(target.shouldRotate && target.circlingAround != null){
			x = target.circlingAround.transform.position.x;
		}else{
			x = target.transform.position.x;
		}

		//smooth follow with lerp
		Vector3 targetPos = new Vector3(screenBottom.x, (target.transform.position.y - screenBottom.y * -0.7f) , transform.position.z);
		transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * 5);
	}
}
