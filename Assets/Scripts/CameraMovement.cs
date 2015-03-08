using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	public float moveSpeed = 5.0f;
	public Vector3 relativePosition = new Vector3(0, 7, -7);

	private int cameraFocusIndex = 0;
	private BoardManager boardManager;
	private bool isMoving = false;

	void Start () {
		boardManager = transform.parent.GetComponent<BoardManager>();

		SetInitialPosition();
	}

	void Update () {
		if (!isMoving) {
			if (Input.GetButtonDown("Next character"))
				ChangeCharacterFocus(1);
			else if (Input.GetButtonDown("Previous character"))
				ChangeCharacterFocus(-1);
		}
	}

	private void SetInitialPosition () {
		transform.position = Vector3.zero;
		transform.position = CurrentCharacter().transform.position + relativePosition;
		
		Vector3 lookDirection = CurrentCharacter().transform.position - transform.position;
		Quaternion rotation = Quaternion.LookRotation(lookDirection);
		
		transform.rotation = rotation;
		transform.parent = CurrentCharacter().transform;
	}

	private void ChangeCharacterFocus(int indexChange) {
		GameObject oldCharacter = boardManager.playerCharacters[cameraFocusIndex];

		cameraFocusIndex = cameraFocusIndex + indexChange;

		if (cameraFocusIndex >= boardManager.playerCharacters.Count)
			cameraFocusIndex = 0;
		else if (cameraFocusIndex < 0)
			cameraFocusIndex = boardManager.playerCharacters.Count - 1;

		if (oldCharacter != CurrentCharacter())
			SnapToCharacter(oldCharacter, CurrentCharacter());
	}

	private void SnapToCharacter(GameObject oldCharacter, GameObject newCharacter) {
		transform.parent = newCharacter.transform;

		StartCoroutine (SmoothMovement (newCharacter));
	}

	IEnumerator SmoothMovement (GameObject character) {
		isMoving = true;

		Vector3 destination = Vector3.zero + character.transform.position + relativePosition;
		float sqrRemainingDistance = (transform.position - destination).sqrMagnitude;
		
		while (sqrRemainingDistance > 0.1f) {
			Vector3 newPosition = Vector3.Lerp(transform.position, destination, Time.deltaTime * moveSpeed);
			
			transform.position = newPosition;

			destination = Vector3.zero + character.transform.position + relativePosition;
			sqrRemainingDistance = (transform.position - destination).sqrMagnitude;
			
			yield return null;
		}
		
		isMoving = false;
	}

	private GameObject CurrentCharacter() {
		return boardManager.playerCharacters[cameraFocusIndex];
	}
}
