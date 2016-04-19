using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CurrentPlayer : MonoBehaviour {
	public GameManager gameManager;
	Text text;
	string playerText;

//	void Awake () {
//		Debug.Log ("GM:");
//		Debug.Log (gameManager);
//		Debug.Log ("CP:");
//		Debug.Log (gameManager.currentPlayer);
//		if (gameManager.currentPlayer.isWhite)
//			playerText = "Current Player: Player 2";
//		else
//			playerText = "Current Player: Player 1";
//		text = gameObject.GetComponent<Text> ();
//		text.text = playerText;
//	}
	private void setPlayerText()
	{
		if (gameManager.currentPlayer.isWhite)
			playerText = "Current Player: Player 2";
		else
			playerText = "Current Player: Player 1";
		text = gameObject.GetComponent<Text> ();
		text.text = playerText;
	}
	
	// Update is called once per frame
	void Update () {
		setPlayerText ();
	}
}
