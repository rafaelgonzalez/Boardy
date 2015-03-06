using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour {

	public GameObject floorObject;
    public GameObject wallObject;
	public GameObject playerObject;

	public int columns = 10;
	public int rows = 10;

	private List<Vector3> wallPositions = new List<Vector3>();

	void Awake() {
		CreateFloor();
		PlaceOuterWalls();

		SetWallPositions();
		PlaceWalls();

		SpawnPlayer();
	}

	void CreateFloor() {
		Vector3 position = new Vector3(rows / 2, 0f, columns / 2);

		Vector3 scale = new Vector3(rows / 10, 1, columns / 10);
		floorObject.transform.localScale = scale;

		Instantiate(floorObject, position, Quaternion.identity);
	}

	void PlaceOuterWalls() {
		float width = rows / 2;
		float height = columns / 2;

		Vector3 northWallPosition = new Vector3(rows, 0.5f, height);
		Vector3 southWalPosition = new Vector3(0, 0.5f, height);
		Vector3 eastWallPosition = new Vector3(width, 0.5f, 0);
		Vector3 westWallPosition = new Vector3(width, 0.5f, columns);

		GameObject northWall = Instantiate(wallObject, northWallPosition, Quaternion.identity) as GameObject;
		GameObject southWall = Instantiate(wallObject, southWalPosition, Quaternion.identity) as GameObject;
		GameObject eastWall = Instantiate(wallObject, eastWallPosition, Quaternion.identity) as GameObject;
		GameObject westWall = Instantiate(wallObject, westWallPosition, Quaternion.identity) as GameObject;

		northWall.transform.localScale = new Vector3(1, 1, columns);
		southWall.transform.localScale = new Vector3(1, 1, columns);
		eastWall.transform.localScale = new Vector3(rows, 1, 1);
		westWall.transform.localScale = new Vector3(rows, 1, 1);
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
			Instantiate(wallObject, wallPositions[i], Quaternion.identity);
		}
	}

	void SpawnPlayer() {
		Vector3 position = RandomAvailablePosition();

		Instantiate(playerObject, position, Quaternion.identity);
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
