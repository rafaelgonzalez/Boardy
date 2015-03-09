using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	public float moveSpeed = 5.0f;
	public Vector3 relativePosition = new Vector3(0, 7, -7);

	private int cameraFocusIndex = 0;
	private BoardManager boardManager;

	void Start () {
		boardManager = transform.parent.GetComponent<BoardManager>();

		SetInitialPosition();
	}

	void Update () {
		SnapToCharacter(boardManager.FocusedCharacter());
	}

	void SetInitialPosition () {
		transform.position = boardManager.FocusedCharacter().transform.position + relativePosition;
		
		Vector3 lookDirection = boardManager.FocusedCharacter().transform.position - transform.position;
		Quaternion rotation = Quaternion.LookRotation(lookDirection);
		
		transform.rotation = rotation;
	}
	
	void SnapToCharacter(GameObject newCharacter) {
		StopCoroutine ("SmoothCameraMovement");
		StartCoroutine ("SmoothCameraMovement", newCharacter);
	}

	IEnumerator SmoothCameraMovement (GameObject character) {
		Vector3 destination = Vector3.zero + character.transform.position + relativePosition;
		float sqrRemainingDistance = (transform.position - destination).sqrMagnitude;
		
		while (sqrRemainingDistance > 0.1f) {
			Vector3 newPosition = Vector3.Lerp(transform.position, destination, Time.deltaTime * moveSpeed);
			
			transform.position = newPosition;

			destination = Vector3.zero + character.transform.position + relativePosition;
			sqrRemainingDistance = (transform.position - destination).sqrMagnitude;
			
			yield return null;
		}
	}
}
