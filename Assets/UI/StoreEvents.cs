using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StoreEvents : MonoBehaviour {
	public Text goldCounter;

	void Start () {
		goldCounter.text = ApplicationModel.playerGold.ToString();
	}

	void Update () {
	
	}

	public void ExitShop() {
		Application.LoadLevel("overworld");
	}
}
