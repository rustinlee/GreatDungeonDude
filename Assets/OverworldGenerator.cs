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
			if (map[y][x] == 'g') {
				map[y] = ChangeCharAtPos(map[y], x, 'd');
				num--;
			}
		}

		return map;
	}

	List<string> AddOcean(List<string> map) {
		int mapLength = map[0].Length;
		int mapHeight = map.Count;
		int lengthConstraint = mapLength / 5;
		int heightContraint = mapHeight / 5;

		int i;
		int lastCount1 = 2;
		int lastCount2 = 2;
		int count;
		for (i = 0; i < mapLength; i++) {
			count = lastCount1 + Random.Range(-1, 2);
			count = Mathf.Clamp(count, 0, lengthConstraint);
			lastCount1 = count;
			while (count <= lengthConstraint) {
				int index = lengthConstraint - count;
				map[index] = ChangeCharAtPos(map[index], i, 'w');
				count++;
			}

			count = lastCount2 + Random.Range(-1, 2);
			count = Mathf.Clamp(count, 0, lengthConstraint);
			lastCount2 = count;
			while (count <= lengthConstraint) {
				int index = mapHeight - 1 - (lengthConstraint - count);
				map[index] = ChangeCharAtPos(map[index], i, 'w');
				count++;
			}
		}

		for (i = 0; i < mapHeight; i++) {
			count = lastCount1 + Random.Range(-1, 2);
			count = Mathf.Clamp(count, 0, heightContraint);
			lastCount1 = count;
			while (count <= heightContraint) {
				int index = heightContraint - count;
				map[i] = ChangeCharAtPos(map[i], index, 'w');
				count++;
			}

			count = lastCount2 + Random.Range(-1, 2);
			count = Mathf.Clamp(count, 0, heightContraint);
			lastCount2 = count;
			while (count <= heightContraint) {
				int index = mapLength - 1 - (heightContraint - count);
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

	void Awake () {
		newMap = GenerateIsland(mapDimensions);

		for (var x = 0; x < newMap.Count; x++) {
			for (var y = 0; y < newMap[x].Length; y++) {
				char symbol = newMap[x][y];
				switch (symbol) {
					case 'w':
						SpawnTile(new Vector2(y, x), waterTile);
						break;
					case 'g':
						SpawnTile(new Vector2(y, x), grassTile);
						break;
					case 'd':
						SpawnTile(new Vector2(y, x), grassTile);
						SpawnTile(new Vector2(y, x), dungeonEntranceTile);
						break;
				}
			}
		}

		bool placingSpawn = true;
		while (placingSpawn) {
			int x = Random.Range(0, (int)mapDimensions.x);
			int y = Random.Range(0, (int)mapDimensions.y);
			if (newMap[y][x] == 'g') {
				Vector3 pos = new Vector3(x * tileSize.x, y * tileSize.y, 0f);
				GameObject.FindGameObjectWithTag("Player").transform.position += pos;
				placingSpawn = false;
			}
		}
	}

	void Update () {
	
	}

	public char GetTile(int x, int y) {
		return newMap[y][x];
	}
}
