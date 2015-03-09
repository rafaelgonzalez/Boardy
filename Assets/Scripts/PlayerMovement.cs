using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public float moveTime = 0.01f;
	public LayerMask blockingLayer;

	private SphereCollider sphereCollider;
	private Rigidbody rigidBody;
	private float inverseMoveTime;
	private bool isMoving = false;
	private Vector3 destination;
	private bool isFocused = false;

	void Start () {
		sphereCollider = GetComponent<SphereCollider>();
		rigidBody = GetComponent<Rigidbody>();
		inverseMoveTime = 1f / moveTime;
	}
	
	void Update () {
		if (isFocused) {
			int horizontal = 0;
			int vertical = 0;

			if (Input.GetKey(KeyCode.Z))
				vertical = 1;
			else if (Input.GetKey(KeyCode.S))
				vertical = -1;
			else if (Input.GetKey(KeyCode.D))
				horizontal = 1;
			else if (Input.GetKey(KeyCode.Q))
				horizontal = -1;

			if (horizontal != 0) {
				vertical = 0;
			}

			if (horizontal != 0 || vertical != 0) {
				Move(horizontal, vertical);
			}
		}
	}

	void Move(int horizontal, int vertical) {
		if (isMoving == true)
			return;

		Vector3 movement = new Vector3 (horizontal, 0.0f, vertical);
		destination = rigidBody.position + movement;

		if (CanMove()) {
			StartCoroutine (SmoothMovement ());
		}
	}

	bool CanMove() {
		sphereCollider.enabled = false;

		bool hit = Physics.Linecast(rigidBody.position, destination, blockingLayer);

		sphereCollider.enabled = true;

		return !hit;
	}

	IEnumerator SmoothMovement () {
		isMoving = true;

		float sqrRemainingDistance = (rigidBody.position - destination).sqrMagnitude;

		while (sqrRemainingDistance > float.Epsilon) {
			Vector3 newPosition = Vector3.MoveTowards(rigidBody.position, destination, inverseMoveTime * Time.deltaTime);

			rigidBody.MovePosition(newPosition);

			sqrRemainingDistance = (rigidBody.position - destination).sqrMagnitude;

			yield return null;
		}

		isMoving = false;
	}

	public void setFocus(bool focus) {
		isFocused = focus;
	}
}
