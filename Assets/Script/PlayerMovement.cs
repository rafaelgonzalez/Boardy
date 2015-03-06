using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public LayerMask blockingLayer;

	private SphereCollider sphereCollider;

	void Start () {
		sphereCollider = gameObject.GetComponent<SphereCollider>();
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
			Move(horizontal, vertical);
		}
	}

	void Move(int horizontal, int vertical) {
		Vector3 movement = new Vector3 (horizontal, 0.0f, vertical);

		if (CanMove(movement))
			gameObject.transform.Translate(movement);
	}

	bool CanMove(Vector3 movement) {
		Vector3 startPosition = gameObject.transform.position;
		Vector3 endPosition = startPosition + movement;

		sphereCollider.enabled = false;

		bool hit = Physics.Linecast(startPosition, endPosition, blockingLayer);

		sphereCollider.enabled = true;

		return !hit;
	}

}
