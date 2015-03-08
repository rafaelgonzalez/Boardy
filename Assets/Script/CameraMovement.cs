using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	private int cameraFocusIndex = 0;
	private BoardManager boardManager;

	void Start () {
		boardManager = transform.parent.GetComponent<BoardManager>();

		SnapToCharacter(boardManager.playerCharacters[cameraFocusIndex]);
	}

	void Update () {
		if (Input.GetKeyDown(KeyCode.Tab)) {
			cameraFocusIndex = cameraFocusIndex + 1;
			if (cameraFocusIndex >= boardManager.playerCharacters.Count) {
				cameraFocusIndex = 0;
			}
			
			SnapToCharacter(boardManager.playerCharacters[cameraFocusIndex]);
		}
	}

	public void SnapToCharacter(GameObject character) {
		transform.position = character.transform.position;
		transform.rotation = Quaternion.identity;
		
		transform.parent = character.transform;
		
		Vector3 movement = new Vector3(0, 7, -7);
		transform.Translate(movement);
		
		Vector3 lookDirection = character.transform.position - GetComponent<Camera>().transform.position;
		Quaternion rotation = Quaternion.LookRotation(lookDirection);
		transform.rotation = rotation;
	}
}
