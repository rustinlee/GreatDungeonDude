using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StoreEvents : MonoBehaviour {
	public Text goldCounter;
	public Text storekeeperDialogue;
	public string dialogueNotEnoughGold;
	public string dialogueThanksForPurchase;

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
			storekeeperDialogue.text = dialogueThanksForPurchase;

			ApplicationModel.playerStr++;
		} else {
			storekeeperDialogue.text = dialogueNotEnoughGold;
		}
	}

	public void BuyDex() {
		if (ApplicationModel.playerGold >= dexCost) {
			ApplicationModel.playerGold -= dexCost;
			goldCounter.text = ApplicationModel.playerGold.ToString();
			storekeeperDialogue.text = dialogueThanksForPurchase;

			ApplicationModel.playerDex++;
		} else {
			storekeeperDialogue.text = dialogueNotEnoughGold;
		}
	}

	public void BuyAgi() {
		if (ApplicationModel.playerGold >= agiCost) {
			ApplicationModel.playerGold -= agiCost;
			goldCounter.text = ApplicationModel.playerGold.ToString();
			storekeeperDialogue.text = dialogueThanksForPurchase;

			ApplicationModel.playerAgi++;
		} else {
			storekeeperDialogue.text = dialogueNotEnoughGold;
		}
	}
}
