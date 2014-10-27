using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ApplicationModel : MonoBehaviour {
	public static List<string> overworldMap;

	public static Sprite playerSprite;
	public static int playerGold = 0;
	public static int playerStr = 4;
	public static int playerDex = 4;
	public static int playerAgi = 4;

	void Start () {
	
	}
	
	void Update () {
	
	}

	public static void ResetGame() {
		overworldMap = null;

		playerGold = 0;
		playerStr = 4;
		playerDex = 4;
		playerAgi = 4;
	}
}
