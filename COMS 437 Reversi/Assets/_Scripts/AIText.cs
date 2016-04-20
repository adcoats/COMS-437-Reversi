using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AIText : MonoBehaviour {
	public Player player;
	Text text;

	void Awake () {
		text = gameObject.GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (player.isAI)
			text.text = "AI ON";
		else
			text.text = "AI OFF";
	}
}
