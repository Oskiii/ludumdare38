using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hands : MonoBehaviour {

	private LookAt2D lookat;
	private Player player;

	void Start(){
		lookat = GetComponentInParent<LookAt2D>();
		player = transform.root.GetComponent<Player>();
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
