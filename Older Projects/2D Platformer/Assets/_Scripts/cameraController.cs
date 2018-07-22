using UnityEngine;
using System.Collections;

public class cameraController : MonoBehaviour {

	public Transform player;
	private Vector3 pos;

	void FixedUpdate () {
		if(player.position.y > -5f){
			pos = new Vector3(player.position.x + 6f, 0f, -10f);
			this.gameObject.transform.position = pos;
		}
	}
}
