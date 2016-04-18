using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public class Player : MonoBehaviour {
	
	//referance to the GameManager
	public GameManager gameManager;
	// The pieces owned by this player.
	private List<GamePiece> myPieces;
	private List<Move> moves;

	public bool isWhite;
	public bool isAI;
	private bool myTurn;

	void Awake()
	{
		myPieces = new List<GamePiece> ();
		moves = new List<Move> ();
		myTurn = false;
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (gameManager.currentPlayer.Equals (this)) {
			if (!gameManager.movesDisplayed) {
				moves = gameManager.displayAvailableMoves ();
			}
			if (isAI) {
				//chose move
				chooseMoveNaive();
			}

			myTurn = true;
		} else {
			myTurn = false;
		}
	}

	public void addGamePiece(int x, int y)
	{
		GamePiece gp = gameManager.pieces [x, y];
		if (!isWhite) {
			gp.transform.Rotate (new Vector3 (0, 0, 180));
		}
		gp.x = x;
		gp.y = y;
		gp.isWhite = this.isWhite;
		myPieces.Add (gameManager.pieces[x, y]);
	}

	public List<GamePiece> getPieces()
	{
		return myPieces;
	}
	public int getNumPieces()
	{
		return myPieces.Count;
	}


	// AI
	// assumed to be player2, so look for min
	public void chooseMoveNaive()
	{
		int min = moves [0].score;
		int index = 0;
		for (int x = 1; x < moves.Count; x++) 
		{
			if (moves [x].score < min) 
			{
				min = moves [x].score;
				index = x;
			}
		}
		// now that we have the min, find any move with same score
		int [] options = new int[moves.Count];
		for (int x = 0; x < moves.Count; x++) 
		{
			if (moves [x].score == min) 
			{
				options [options.GetLength (0)] = x;
			}
		}
		// now select an option randomly
		System.Random r = new System.Random();
		int choice = options[r.Next (options.GetLength(0))];

		gameManager.cubes [(int)moves [choice].move.x, (int)moves [choice].move.y].applyMove ();
	}
}
