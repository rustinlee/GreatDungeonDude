using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapGenerator : MonoBehaviour {
	public int walkableTiles = 500;
	public Transform wallTile;
	public Transform groundTile;
	public Transform playerPrefab;
	public List<Transform> lootTable;
	public float lootChance;

	private Vector2 playerSpawn;
	private Transform lootHolder;

	Vector2 vec2RandomAdd(Vector2 vec2, int amt) {
		int switchVar = Random.Range(0, 4);

		switch (switchVar) {
			case 0:
				vec2 += new Vector2(amt, 0);
				break;
			case 1:
				vec2 += new Vector2(-amt, 0);
				break;
			case 2:
				vec2 += new Vector2(0, amt);
				break;
			case 3:
				vec2 += new Vector2(0, -amt);
				break;
		}

		return vec2;
	}

	void SpawnLoot(Vector2 pos) {
		int index = Random.Range(0, lootTable.Count); //exclusive for integers for some reason

		Transform loot = Instantiate(lootTable[index]) as Transform;
		loot.position += new Vector3(pos.x, pos.y, 0);
		loot.parent = lootHolder;
	}

	void DrunkardsWalk(int size) {
		Vector2 startPoint = new Vector2(0, 0);
		List<Vector2> points = new List<Vector2>();
		points.Add(startPoint);

		Vector2 currentPoint = new Vector2(startPoint.x, startPoint.y);

		int i = 0;
		int j = 0;

		for (i = 0; i < size - 1; i++) {
			bool generatingUniquePoint = true;
			while (generatingUniquePoint) {
				currentPoint = vec2RandomAdd(new Vector2(currentPoint.x, currentPoint.y), 1);

				bool searching = true;
				bool duplicate = false;

				while (searching) {
					if (currentPoint.x == points[j].x && currentPoint.y == points[j].y) {
						duplicate = true;
						searching = false;
						j = 0;
					}

					j++;

					if (j == points.Count) {
						searching = false;
						generatingUniquePoint = false;
						j = 0;
					}
				}

				if (!duplicate) {
					points.Add(currentPoint);
				}
			}
		}

		int lowestX = 0;
		int lowestY = 0;
		int greatestX = 0;
		int greatestY = 0;
		for (i = 0; i < points.Count; i++) {
			if (points[i].x < lowestX) {
				lowestX = (int)points[i].x;
			} else if (points[i].x > greatestX) {
				greatestX = (int)points[i].x;
			}

			if (points[i].y < lowestY) {
				lowestY = (int)points[i].y;
			} else if (points[i].y > greatestY) {
				greatestY = (int)points[i].y;
			}
		}
		
		Debug.Log("lowest x and y: " + lowestX + ' ' + lowestY);
		Debug.Log("highest x and y: " + greatestX + ' ' + greatestY);

		//shift values so lowest value is 0
		for (i = 0; i < points.Count; i++) {
			points[i] += new Vector2(Mathf.Abs(lowestX), Mathf.Abs(lowestY));
		}

		int domain = greatestX + Mathf.Abs(lowestX);
		int range = greatestY + Mathf.Abs(lowestY);

		//generate a blank map
		List<string> mapArray = new List<string>();
		string mapY = "";
		for (i = 0; i <= range; i++) {
			mapY += '#';
		}

		for (i = 0; i <= domain; i++) {
			mapArray.Add(mapY);
		}

		//add points to map
		for (i = 0; i < points.Count; i++) {
			string a = mapArray[(int)points[i].x];
			mapArray[(int)points[i].x] = a.Substring(0, (int)points[i].y);
			mapArray[(int)points[i].x] += ".";
			mapArray[(int)points[i].x] += a.Substring((int)points[i].y + 1, (a.Length - (int)points[i].y - 1));
		}

		mapArray.Insert(0, mapY);
		mapArray.Add(mapY);

		for (i = 0; i < mapArray.Count; i++) {
			mapArray[i] = '#' + mapArray[i] + '#';
		}

		//clean up unnecessary walls
		for (var x = 0; x < mapArray.Count; x++) {
			for (var y = 0; y < mapArray[x].Length; y++) {
				bool adj1 = false;
				bool adj3 = false;
				bool adj4 = false;
				bool adj5 = false;
				bool adj6 = false;
				bool adj8 = false;

				if (x - 1 > 0) {
					if (y - 1 > 0)
						adj1 = (mapArray[x - 1][y - 1] == '.');
					adj4 = (mapArray[x - 1][y] == '.');
					if (y + 1 < mapArray[0].Length - 1)
						adj6 = (mapArray[x - 1][y + 1] == '.');
				}

				if (x + 1 < mapArray.Count - 2) {
					if (y - 1 > 0)
						adj3 = (mapArray[x + 1][y - 1] == '.');
					adj5 = (mapArray[x + 1][y] == '.');
					if (y + 1 < mapArray[0].Length - 1)
						adj8 = (mapArray[x + 1][y + 1] == '.');
				}

				bool adj2 = false;
				if (y - 1 > 0)
					adj2 = (mapArray[x][y - 1] == '.');

				bool adj7 = false;
				if (y + 1 < mapArray[0].Length - 1)
					adj7 = (mapArray[x][y + 1] == '.');

				bool adj = adj1 || adj2 || adj3 || adj4 || adj5 || adj6 || adj7 || adj8;

				if (!adj) {
					string a = mapArray[x];
					mapArray[x] = a.Substring(0, y);
					mapArray[x] += ' ';
					mapArray[x] += a.Substring(y + 1, a.Length - y - 1);
				}
			}
		}

		/*
		for (i = 0; i < mapArray.Count; i++) {
			Debug.Log(mapArray[i] + i);
		}
		*/

		//place tiles
		for (var x = 0; x < mapArray.Count; x++) {
			for (var y = 0; y < mapArray[x].Length; y++) {
				char symbol = mapArray[x][y];
				Transform tile;

				switch (symbol) {
					case '#':
						tile = Instantiate(wallTile) as Transform;
						tile.position = new Vector3(x, y, tile.position.z);
						tile.parent = gameObject.transform;
						break;
					case '.':
						tile = Instantiate(groundTile) as Transform;
						tile.position = new Vector3(x, y, tile.position.z);
						tile.parent = gameObject.transform;
						//chance of spawning loot on walkable tiles
						if (Random.Range(0f, 1f) < lootChance)
							SpawnLoot(new Vector2(x, y));
						break;
				}
			}
		}

		//set player spawn
		playerSpawn = points[0] + new Vector2(1, 1);
	}

	void SpawnPlayer(Vector2 pos) {
		Transform player = Instantiate(playerPrefab) as Transform;
		player.position += new Vector3(pos.x, pos.y, 0);
	}

	void Awake() {
		lootHolder = GameObject.Find("Loot").transform;
		DrunkardsWalk(walkableTiles);
		if(!GameObject.FindGameObjectWithTag("Player"))
			SpawnPlayer(playerSpawn);
	}
	
	void Update() {
	
	}
}
