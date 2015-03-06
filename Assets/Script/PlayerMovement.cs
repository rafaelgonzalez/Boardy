using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	void Update () {
		int horizontal = 0;
		int vertical = 0;

		if (Input.GetKeyDown(KeyCode.Z))
			vertical = 1;
		else if (Input.GetKeyDown(KeyCode.S))
			vertical = -1;
		else if (Input.GetKeyDown(KeyCode.D))
			horizontal = 1;
		else if (Input.GetKeyDown(KeyCode.Q))
			horizontal = -1;

		if (horizontal != 0) {
			vertical = 0;
		}

		if (horizontal != 0 || vertical != 0) {
			Vector3 movement = new Vector3 (horizontal, 0.0f, vertical);

			gameObject.transform.Translate(movement);
		}
	}
}
