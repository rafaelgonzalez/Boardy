using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	public float moveSpeed = 5.0f;

	private int cameraFocusIndex = 0;
	private BoardManager boardManager;
	private bool isRotating = false;

	private enum Orientation {
		North,
		East,
		South,
		West,
	}

	private Orientation orientation = Orientation.North;

	void Start () {
		boardManager = transform.parent.GetComponent<BoardManager>();

		SetInitialPosition();
	}

	void Update () {
		if (!isRotating) {
			SnapToCharacter(boardManager.FocusedCharacter());

			if (Input.GetKeyDown(KeyCode.E))
				ChangeOrientation(1, -90);
			else if (Input.GetKeyDown(KeyCode.A))
				ChangeOrientation(-1, 90);
		}
	}

	void SetInitialPosition () {
		Vector3 position = boardManager.FocusedCharacter().transform.position;

		transform.position = position + relativePosition();
		
		Vector3 lookDirection = position - transform.position;
		Quaternion rotation = Quaternion.LookRotation(lookDirection);
		
		transform.rotation = rotation;
	}
	
	void SnapToCharacter(GameObject newCharacter) {
		StopCoroutine ("SmoothCameraMovement");
		StartCoroutine ("SmoothCameraMovement", newCharacter);
	}

	void ChangeOrientation(int iteration, int angle) {
		int orientationIndex = (int) orientation + iteration;

 		if (orientationIndex >= System.Enum.GetValues(typeof(Orientation)).Length)
			orientationIndex = 0;
		else if (orientationIndex < 0)
			orientationIndex = System.Enum.GetValues(typeof(Orientation)).Length - 1;

		transform.RotateAround(boardManager.FocusedCharacter().transform.position, Vector3.up, angle);
		orientation = (Orientation) orientationIndex;

//		StartCoroutine ("SmoothCameraRotation");
	}

	IEnumerator SmoothCameraMovement (GameObject character) {
		Vector3 destination = Vector3.zero + character.transform.position + relativePosition();
		float sqrRemainingDistance = (transform.position - destination).sqrMagnitude;
		
		while (sqrRemainingDistance > 0.1f) {
			Vector3 newPosition = Vector3.Lerp(transform.position, destination, Time.deltaTime * moveSpeed);
	
			transform.position = newPosition;

			sqrRemainingDistance = (transform.position - destination).sqrMagnitude;
			
			yield return null;
		}
	}

//	IEnumerator SmoothCameraRotation () {
////		isRotating = true;
//
//		Vector3 lookDirection = boardManager.FocusedCharacter().transform.position - transform.position;
//    	Quaternion lookRotation = Quaternion.LookRotation(lookDirection);
//    
//		while (transform.rotation != lookRotation) {
//			Quaternion rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * moveSpeed);
//			transform.rotation = rotation;
//      
//		    yield return null;
//		}
//
////		isRotating = false;
//	}

	Vector3 relativePosition() {
		Vector3 vector = Vector3.zero;

		if (orientation == Orientation.North)
			vector = new Vector3(0, 7, -7);
		else if (orientation == Orientation.West)
			vector = new Vector3(-7, 7, 0);
		else if (orientation == Orientation.South)
			vector = new Vector3(0, 7, 7);
		else if (orientation == Orientation.East)
			 vector = new Vector3(7, 7, 0);

		return vector;
	}
}
