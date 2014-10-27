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
	public Transform shopkeeper;
	public int stores;

	private List<string> mapData;

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

	List<string> initMap() {
		List<string> newMap = GenerateIsland(mapDimensions);



		bool placingStore = true; //todo: fix redundant code
		while (placingStore) {
			int x = Random.Range(0, (int)mapDimensions.x);
			int y = Random.Range(0, (int)mapDimensions.y);
			if (newMap[y][x] == 'g') {
				newMap[y] = ChangeCharAtPos(newMap[y], x, 's');
				placingStore = false;
			}
		}

		bool placingSpawn = true;
		while (placingSpawn) {
			int x = Random.Range(0, (int)mapDimensions.x);
			int y = Random.Range(0, (int)mapDimensions.y);
			if (newMap[y][x] == 'g') {
				newMap[y] = ChangeCharAtPos(newMap[y], x, 'p');
				placingSpawn = false;
			}
		}

		return newMap;
	}

	void drawMap() {
		for (var x = 0; x < mapData.Count; x++) {
			for (var y = 0; y < mapData[x].Length; y++) {
				char symbol = mapData[x][y];
				Vector3 pos;
				switch (symbol) {
					case 'w': //water
						SpawnTile(new Vector2(y, x), waterTile);
						break;
					case 'g': //grass
						SpawnTile(new Vector2(y, x), grassTile);
						break;
					case 'd': //dungeon entrance
						SpawnTile(new Vector2(y, x), grassTile);
						SpawnTile(new Vector2(y, x), dungeonEntranceTile);
						break;
					case 's': //shop
						SpawnTile(new Vector2(y, x), grassTile);
						pos = new Vector3(y * tileSize.y, x * tileSize.x, 0f);
						Transform newShopkeeper = Instantiate(shopkeeper, pos, new Quaternion()) as Transform;
						break;
					case 'p': //player spawn
						SpawnTile(new Vector2(y, x), grassTile);
						pos = new Vector3(y * tileSize.y, x * tileSize.x, 0f);
						GameObject.FindGameObjectWithTag("Player").transform.position = pos;
						break;
				}
			}
		}
	}

	void Awake () {
		if (ApplicationModel.overworldMap == null) {
			ApplicationModel.overworldMap = initMap();
		}

		mapData = ApplicationModel.overworldMap;
		for (var x = 0; x < mapData.Count; x++) {
			for (var y = 0; y < mapData[x].Length; y++) {
				if (mapData[x][y] == 'd') {
					mapData[x] = ChangeCharAtPos(mapData[x], y, 'g');
				}
			}
		}

		mapData = AddDungeons(mapData, dungeons);
		drawMap();
	}

	void Update () {
	
	}

	public char GetTile(int x, int y) {
		return mapData[y][x];
	}

	public void ChangeTile(int x, int y, char c) {
		mapData[y] = ChangeCharAtPos(mapData[y], x, c);
	}
}
