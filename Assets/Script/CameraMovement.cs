using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	public Vector3 relativePosition = new Vector3(0, 7, -7);

	private int cameraFocusIndex = 0;
	private BoardManager boardManager;

	void Start () {
		boardManager = transform.parent.GetComponent<BoardManager>();

		SetInitialPosition();
	}

	void Update () {
		if (Input.GetKeyDown(KeyCode.Tab)) {
			ChangeCharacterFocus(1);
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

		Vector3 endPosition = Vector3.zero + newCharacter.transform.position + relativePosition;
		transform.position = endPosition;
	}

	private GameObject CurrentCharacter() {
		return boardManager.playerCharacters[cameraFocusIndex];
	}
}
