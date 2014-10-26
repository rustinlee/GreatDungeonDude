using UnityEngine;
using System.Collections;

public class StoreEvents : MonoBehaviour {
	void Start () {
	
	}

	void Update () {
	
	}

	public void ExitShop() {
		Application.LoadLevel("overworld");
	}
}
