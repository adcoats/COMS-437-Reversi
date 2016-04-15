using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Score : MonoBehaviour {
	public Player player;
	Text text;
	string scoreText;
	// Use this for initialization
	void Awake () {
		if (player.isWhite)
			scoreText = "Player 2 Score: ";
		else
			scoreText = "Player 1 Score: ";
		text = gameObject.GetComponent<Text> ();
		text.text = scoreText + 0;
	}
	
	// Update is called once per frame
	void Update () {
		text.text = scoreText + player.getNumPieces ();
	}
}
