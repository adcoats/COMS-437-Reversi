using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Player : MonoBehaviour {
	
	//referance to the GameManager
	public GameManager gameManager;
	// The pieces owned by this player.
	private List<GamePiece> myPieces;

	public bool isWhite;
	public bool isAI;
	private bool myTurn;

	void Awake()
	{
		myPieces = new List<GamePiece> ();
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (gameManager.currentPlayer.Equals(this)){
			gameManager.displayAvailableMoves ();
		}
	}

	public void addGamePiece(int x, int y)
	{
		GamePiece gp = gameManager.pieces [x, y];
		if (!isWhite)
			gp.transform.Rotate (new Vector3 (0, 0, 180));
		myPieces.Add (gameManager.pieces[x, y]);
	}

	public int getNumPieces()
	{
		return myPieces.Count;
	}


	// AI

	public void chooseMove()
	{

	}
}
