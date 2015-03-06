using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public float moveTime = 0.01f;
	public LayerMask blockingLayer;

	private SphereCollider sphereCollider;
	private float inverseMoveTime;

	void Start () {
		sphereCollider = gameObject.GetComponent<SphereCollider>();
		inverseMoveTime = 1f / moveTime;
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
		Vector3 startPosition = gameObject.transform.position;
		Vector3 endPosition = startPosition + movement;

		if (CanMove(startPosition, endPosition)) {
			StartCoroutine (SmoothMovement (endPosition));
		}
	}

	bool CanMove(Vector3 startPosition, Vector3 endPosition) {
		sphereCollider.enabled = false;

		bool hit = Physics.Linecast(startPosition, endPosition, blockingLayer);

		sphereCollider.enabled = true;

		return !hit;
	}

	IEnumerator SmoothMovement (Vector3 endPosition) {
		float sqrRemainingDistance = (gameObject.transform.position - endPosition).sqrMagnitude;

		while (sqrRemainingDistance > float.Epsilon) {
			Vector3 newPosition = Vector3.MoveTowards(gameObject.transform.position, endPosition, inverseMoveTime * Time.deltaTime);

			gameObject.transform.position = newPosition;

			sqrRemainingDistance = (gameObject.transform.position - endPosition).sqrMagnitude;

			yield return null;
		}
	}
}
