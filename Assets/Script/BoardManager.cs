using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour {

	public GameObject floorPrefab;
    public GameObject wallPrefab;
	public GameObject playerPrefab;
	public GameObject cameraPrefab;

	public int columns = 10;
	public int rows = 10;

	public int numberOfPlayerCharacters = 5;

	private List<Vector3> wallPositions = new List<Vector3>();
	private List<GameObject> playerCharacters = new List<GameObject>();
	private GameObject camera;

	void Awake() {
		CreateFloor();
		PlaceOuterWalls();

		SetWallPositions();
		PlaceWalls();

		SpawnPlayerCharacters();
		SetCamera();
	}

	void CreateFloor() {
		Vector3 position = new Vector3(rows / 2f, 0f, columns / 2f);

		Vector3 scale = new Vector3(rows / 10f, 1f, columns / 10f);

		GameObject floorInstance = Instantiate(floorPrefab, position, Quaternion.identity) as GameObject;

		floorInstance.name = "Floor";
		floorInstance.transform.localScale = scale;
	}

	void PlaceOuterWalls() {
		float width = rows / 2;
		float height = columns / 2;

		Vector3 northWallPosition = new Vector3(0, 0.5f, height);
		Vector3 southWalPosition = new Vector3(rows, 0.5f, height);
		Vector3 eastWallPosition = new Vector3(width, 0.5f, columns);
		Vector3 westWallPosition = new Vector3(width, 0.5f, 0);

		GameObject northWall = Instantiate(wallPrefab, northWallPosition, Quaternion.identity) as GameObject;
		GameObject southWall = Instantiate(wallPrefab, southWalPosition, Quaternion.identity) as GameObject;
		GameObject eastWall = Instantiate(wallPrefab, eastWallPosition, Quaternion.identity) as GameObject;
		GameObject westWall = Instantiate(wallPrefab, westWallPosition, Quaternion.identity) as GameObject;

		northWall.transform.localScale = new Vector3(1, 1, columns);
		southWall.transform.localScale = new Vector3(1, 1, columns);
		eastWall.transform.localScale = new Vector3(rows, 1, 1);
		westWall.transform.localScale = new Vector3(rows, 1, 1);

		northWall.name = "North Wall";
		southWall.name = "South Wall";
		eastWall.name = "East Wall";
		westWall.name = "West Wall";
	}
	
	void SetWallPositions() {
		int numberOfWalls = (int) ((columns + rows) / 2);

		for (int i = 0; i < numberOfWalls; i++) {
			Vector3 position = RandomAvailablePosition();

			wallPositions.Add(position);
		}
    }

 	void PlaceWalls() {
		for (int i = 0; i < wallPositions.Count; i++) {
			Instantiate(wallPrefab, wallPositions[i], Quaternion.identity);
		}
	}

	void SpawnPlayerCharacters() {
		for (int i = 0; i < numberOfPlayerCharacters; i++) {
			Vector3 position = RandomAvailablePosition();

			GameObject character = Instantiate(playerPrefab, position, Quaternion.identity) as GameObject;

			playerCharacters.Add(character);
		}
  	}
  
	void SetCamera() {
		GameObject camera = Instantiate(cameraPrefab, playerCharacters[0].transform.position, Quaternion.identity) as GameObject;
		camera.transform.parent = playerCharacters[0].transform;

		Vector3 movement = new Vector3(0, 7, -7);
		camera.transform.Translate(movement);

		Vector3 lookDirection = playerCharacters[0].transform.position - camera.transform.position;
		Quaternion rotation = Quaternion.LookRotation(lookDirection);
		camera.transform.rotation = rotation;
	}

	Vector3 RandomAvailablePosition() {
		Vector3 position = RandomPosition();

		while (wallPositions.Exists(e => e == position)) {
			position = RandomPosition();
	    }

		return position;
	  }

 	Vector3 RandomPosition() {
		int randomX = Random.Range (2, rows - 2);
		int randomZ = Random.Range (2, columns - 2);
		
		Vector3 position = new Vector3(randomX, 0.5f, randomZ);

		return position;
	}
}
