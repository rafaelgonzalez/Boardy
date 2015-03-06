using UnityEngine;
//using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour {

	public GameObject wallObject;

	public int columns = 10;
	public int rows = 10;

	private List<Vector3> wallPositions = new List<Vector3>();

	void Awake() {
		PlaceBorderWalls();

		SetWallPositions();
		PlaceWalls();
	}

	void PlaceBorderWalls() {
		for (int x = 0; x <= rows; x++) {
			for (int z = 0; z <= columns; z++) {
				if (x == 0 || x == rows || z == 0 || z == columns) {
					Vector3 position = new Vector3(x, 0f, z);

					Instantiate(wallObject, position, Quaternion.identity);
				}
			}
		}
	}
	
	void SetWallPositions() {	
		int numberOfWalls = (int) ((columns + rows) / 2);

		for (int i = 0; i < numberOfWalls; i++) {
			Vector3 position = RandomPosition();

			while (wallPositions.Exists(e => e == position)) {
		        position = RandomPosition();
			}

			wallPositions.Add(position);
		}
    }

 	void PlaceWalls() {
		for (int i = 0; i < wallPositions.Count; i++) {
			Instantiate(wallObject, wallPositions[i], Quaternion.identity);
		}
	}

	Vector3 RandomPosition() {
		int randomX = Random.Range (2, rows - 2);
		int randomZ = Random.Range (2, columns - 2);
		
		Vector3 position = new Vector3(randomX, 0f, randomZ);

		return position;
	}
}
