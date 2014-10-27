using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TitleScript : MonoBehaviour {
	public List<Sprite> characters;

	void Start () {
	
	}
	
	void Update () {
	
	}

	public void ChooseCharacter(int choice) {
		ApplicationModel.playerSprite = characters[choice];
		Application.LoadLevel("overworld");
	}
}
