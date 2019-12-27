using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt2D : MonoBehaviour {

	public Transform lookAtTarget;

	void Update(){
		if(lookAtTarget != null){
			LookAt();
		}
	}

	private void LookAt(){
		Vector3 diff = lookAtTarget.transform.position - transform.position;
        diff.Normalize();
 
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
	}
}
