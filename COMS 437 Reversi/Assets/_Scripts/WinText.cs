using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WinText : MonoBehaviour {
	public GameManager gameManager;
	Text text;
	string winText;
	// Use this for initialization
	void Awake () {
		text = gameObject.GetComponent<Text> ();
	}

	public void activate()
	{
		int p1Score = gameManager.player1.getNumPieces ();
		int p2Score = gameManager.player2.getNumPieces ();
		if (p1Score > p2Score) {
			text.text = "Player 1 Wins!";
		} else if (p2Score > p1Score) {
			text.text = "Player 2 Wins!";
		}
		else
			text.text = "The Game was Tied!";
	}
}
