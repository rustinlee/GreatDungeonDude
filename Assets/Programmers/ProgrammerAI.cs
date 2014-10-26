using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProgrammerAI : MonoBehaviour {
	public float talkDuration;
	public float talkCooldown;
	public List<string> idleDialogue;

	public Vector2 boxOffset = new Vector2(20, 30);
	public Vector2 boxSize = new Vector2(40, 6);

	private bool isTalking = false;
	private float timeSinceTalked = 0f;
	private string currentDialogue;

	void PickNewDialogue() {
		currentDialogue = idleDialogue[Random.Range(0, idleDialogue.Count)];
	}

	void Start() {
		PickNewDialogue();
		transform.localEulerAngles = new Vector3(0, 0, Random.Range(0, 360));
	}
	
	void Update() {
		timeSinceTalked += Time.deltaTime;

		if (isTalking) {
			if (timeSinceTalked > talkDuration) {
				isTalking = false;
				timeSinceTalked = 0;
				Debug.Log("stopped talking");
			}
		} else {
			if (timeSinceTalked > talkCooldown) {
				PickNewDialogue();
				isTalking = true;
				timeSinceTalked = 0;
				Debug.Log("started talking");
			}
		}
	}

	void OnGUI() {
		if (isTalking) {
			Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
			Vector2 boxPos = new Vector2(screenPos.x, -screenPos.y + Screen.height);
			boxPos += boxOffset;

			GUIStyle centeredStyle = GUI.skin.GetStyle("Label");
			centeredStyle.alignment = TextAnchor.UpperCenter;
			GUI.Label(new Rect(boxPos.x, boxPos.y, boxSize.x, boxSize.y), currentDialogue, centeredStyle);
		}

		//GUI.Label(new Rect(10, 10, 160, 60), currentDialogue);
	}
}
