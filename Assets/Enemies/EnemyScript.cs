using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {
	//enemy variable declarations
	public int maxHP = 50;

	private float HP;
	private Transform target;

	//hp bar declarations
	public Vector2 barOffset;
	private Vector2 barPos;
	//private GUIStyle HP_bar = new GUIStyle();
	public Vector2 barSize = new Vector2(60, 20);
	private Texture2D emptyTex;
	private Texture2D fullTex;
	private GUIStyle emptyStyle;
	private GUIStyle fullStyle;

	void Start() {
		HP = maxHP;

		target = GameObject.FindGameObjectWithTag("Player").transform;

		//HP_bar.padding = new RectOffset(0, 0, 0, 0);
		emptyTex = new Texture2D(1, 1);
		emptyTex.SetPixel(0, 0, Color.red);
		emptyTex.wrapMode = TextureWrapMode.Repeat;
		emptyTex.Apply();

		emptyStyle = new GUIStyle();
		emptyStyle.normal.background = emptyTex;

		fullTex = new Texture2D(1, 1);
		fullTex.SetPixel(0, 0, Color.green);
		fullTex.wrapMode = TextureWrapMode.Repeat;
		fullTex.Apply();

		fullStyle = new GUIStyle();
		fullStyle.normal.background = fullTex;
	}

	void OnGUI() {
		if (HP / maxHP != 1) {
			Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
			barPos = new Vector2(screenPos.x, -screenPos.y + Screen.height);
			barPos += barOffset;

			GUI.BeginGroup(new Rect(barPos.x, barPos.y, barSize.x, barSize.y));
				GUI.Box(new Rect(0, 0, barSize.x, barSize.y), GUIContent.none, emptyStyle);

				GUI.BeginGroup(new Rect(0, 0, barSize.x * (HP / maxHP), barSize.y));
					GUI.Box(new Rect(0, 0, barSize.x, barSize.y), GUIContent.none, fullStyle);
				GUI.EndGroup();
			GUI.EndGroup();
		}
	}

	void Update() {
		Vector3 dir = target.position - transform.position;
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}

	public void DamageHP(int amt) {
		HP -= amt;

		if (HP <= 0) {
			Destroy(gameObject);
		}
	}
}
