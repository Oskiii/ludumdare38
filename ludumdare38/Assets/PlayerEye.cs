using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEye : MonoBehaviour {

	private LookAt2D lookat;
	private Player player;

	void Start(){
		lookat = GetComponent<LookAt2D>();
		player = GetComponentInParent<Player>();
	}

	void Update(){
		if(player.circlingAround != null){
			if(player.shouldRotate){
				lookat.lookAtTarget = player.circlingAround.transform;
			}else{
				lookat.lookAtTarget = player.FindClosestMoon().transform;
			}
		}
	}
}
