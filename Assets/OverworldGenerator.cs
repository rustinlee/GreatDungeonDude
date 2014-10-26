using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OverworldGenerator : MonoBehaviour {
	public Transform grassTile;
	public Transform waterTile;
	public Transform dungeonEntranceTile;
	public Vector2 mapDimensions;
	public Vector2 tileSize;
	public int dungeons;

	private List<string> newMap;

	string ChangeCharAtPos(string str, int index, char character) {
		char[] charArr = str.ToCharArray();
		charArr[index] = character;
		return new string(charArr);
	}

	void SpawnTile(Vector2 pos, Transform tilePrefab) {
		Transform tile = Instantiate(tilePrefab) as Transform;
		tile.position = new Vector3(pos.x * tileSize.x, pos.y * tileSize.y, tile.position.z);
		tile.localScale = new Vector3(tileSize.x, tileSize.y, 1);
		tile.parent = gameObject.transform;
	}

	List<string> AddDungeons(List<string> map, int num) {
		int i = 0;
		while (i < num) {
			int x = Random.Range(0, (int)mapDimensions.x);
			int y = Random.Range(0, (int)mapDimensions.y);
			if (map[x][y] == 'g') {
				map[x] = ChangeCharAtPos(map[x], y, 'd');
				num--;
			}
		}

		return map;
	}

	List<string> AddOcean(List<string> map) {
		int mapLength = map[0].Length;
		int mapHeight = map.Count;

		int i;
		int count;
		for (i = 0; i < mapLength; i++) {
			count = Random.Range(0, 3);
			while (count < 3) {
				int index = 2 - count;
				map[index] = ChangeCharAtPos(map[index], i, 'w');
				count++;
			}

			count = Random.Range(0, 3);
			while (count < 3) {
				int index = mapLength - 1 - (2 - count);
				map[index] = ChangeCharAtPos(map[index], i, 'w');
				count++;
			}
		}

		for (i = 0; i < mapHeight; i++) {
			count = Random.Range(0, 3);
			while (count < 3) {
				int index = 2 - count;
				map[i] = ChangeCharAtPos(map[i], index, 'w');
				count++;
			}

			count = Random.Range(0, 3);
			while (count < 3) {
				int index = mapLength - 1 - (2 - count);
				map[i] = ChangeCharAtPos(map[i], index, 'w');
				count++;
			}
		}

		return map;
	}

	List<string> GenerateIsland(Vector2 dimensions) {
		int x;
		int y;

		string row = "";
		for (x = 0; x < dimensions.x; x++) {
			row += "g";
		}

		List<string> mapArray = new List<string>(); 
		for (y = 0; y < dimensions.y; y++) {
			mapArray.Add(row);
		}

		mapArray = AddOcean(mapArray);
		mapArray = AddDungeons(mapArray, dungeons);

		return mapArray;
	}

	void Start () {
		newMap = GenerateIsland(mapDimensions);

		for (var x = 0; x < newMap.Count; x++) {
			for (var y = 0; y < newMap[x].Length; y++) {
				char symbol = newMap[x][y];
				switch (symbol) {
					case 'w':
						SpawnTile(new Vector2(x, y), waterTile);
						break;
					case 'g':
						SpawnTile(new Vector2(x, y), grassTile);
						break;
					case 'd':
						SpawnTile(new Vector2(x, y), grassTile);
						SpawnTile(new Vector2(x, y), dungeonEntranceTile);
						break;
				}
			}
		}
	}

	void Update () {
	
	}

	public char GetTile(int x, int y) {
		return newMap[x][y];
	}
}
