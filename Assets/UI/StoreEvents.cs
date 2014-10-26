using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StoreEvents : MonoBehaviour {
	public Text goldCounter;
	public Text storekeeperDialogue;
	public string dialogue_notEnoughGold;
	public string dialogue_thanksForPurchase;

	private int strCost = 20;
	private int dexCost = 20;
	private int agiCost = 20;

	void Start () {
		goldCounter.text = ApplicationModel.playerGold.ToString();
	}

	void Update () {
	
	}

	public void ExitShop() {
		Application.LoadLevel("overworld");
	}

	public void BuyStr() {
		if (ApplicationModel.playerGold >= strCost) {
			ApplicationModel.playerGold -= strCost;
			goldCounter.text = ApplicationModel.playerGold.ToString();

			ApplicationModel.playerStr++;
		}
	}

	public void BuyDex() {
		if (ApplicationModel.playerGold >= dexCost) {
			ApplicationModel.playerGold -= dexCost;
			goldCounter.text = ApplicationModel.playerGold.ToString();

			ApplicationModel.playerDex++;
		}
	}

	public void BuyAgi() {
		if (ApplicationModel.playerGold >= agiCost) {
			ApplicationModel.playerGold -= agiCost;
			goldCounter.text = ApplicationModel.playerGold.ToString();

			ApplicationModel.playerAgi++;
		}
	}
}
