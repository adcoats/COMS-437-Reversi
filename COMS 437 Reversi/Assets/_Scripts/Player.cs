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
	public int difficulty;
	private bool myTurn;

	void Awake()
	{
		myPieces = new List<GamePiece> ();
		moves = new List<Move> ();
		myTurn = false;
		isAI = false;
		difficulty = 1;
	}

	public void setDifficulty(int value)
	{
		if (value < 1)
			value = 1;
		else if (value > 10)
			value = 10;
		difficulty = value;
	}

	// Update is called once per frame
	void Update () {
		if (gameManager.currentPlayer.Equals (this)) 
		{
			if (gameManager.moveInProgress == false) 
			{
				if (!gameManager.movesDisplayed) 
				{
					moves = gameManager.displayAvailableMoves (isAI);
				}
				if (isAI) 
				{
					if (moves.Count < 1) {
						gameManager.endMyTurn ();
						myTurn = false;
					} else {
						chooseMoveNegaMax ();
					}
					//chose move
					//chooseMoveNaive();

					
				}

				myTurn = true;
			}
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
		gp.isWhite = this.isWhite;
		myPieces.Add (gameManager.pieces[x, y]);
	}
	public void addGamePiece(GamePiece gp)
	{
		myPieces.Add (gp);
	}
	public void removePiece(GamePiece gp)
	{
		myPieces.Remove (gp);
	}

	public List<GamePiece> getPieces()
	{
		return myPieces;
	}
	public int getNumPieces()
	{
		return myPieces.Count;
	}

	public void removeAllPieces()
	{
		myPieces = new List<GamePiece> ();
	}

	public void toggleAI()
	{
		isAI = !isAI;
	}

	// AI
	// assumed to be player2, so look for min
	public void chooseMoveNaive()
	{
		if (moves.Count > 0) {
			int min = moves [0].score;
			int index = 0;
			for (int x = 1; x < moves.Count; x++) {
				if (moves [x].score < min) {
					min = moves [x].score;
					index = x;
				}
			}
			// now that we have the min, find any move with same score
			int[] options = new int[moves.Count];
			index = 0;
			for (int x = 0; x < moves.Count; x++) {
				if (moves [x].score == min) {
//					options [options.GetLength (0)] = x;
					options [index] = x;
					index++;
				}
			}
			// now select an option randomly
			System.Random r = new System.Random ();
			int choice = options [r.Next (options.GetLength (0))];

			gameManager.cubes [(int)moves [choice].move.x, (int)moves [choice].move.y].applyMove ();
		} else
			gameManager.endMyTurn ();
	}

	private void chooseMoveNegaMax()
	{
		Move startNode = gameManager.moveSelector.evaluate (-1);
		Move alpha = new Move ();
		alpha.score = int.MinValue;
		Move beta = new Move ();
		beta.score = int.MaxValue;
		Move move = negaMax (startNode, difficulty, alpha, beta, -1);
	}
	public Move negaMax(Move node, int depth, Move alpha, Move beta, int color)
	{
		if (depth == 0 || node.moves.Count < 1) {
			node.score = color * node.score;
			return node;
		}

		Move bestValue = new Move ();
		bestValue.score = int.MinValue;

		List<Move> childNodes = node.getMoves (color);

		foreach (Move child in childNodes) {
			beta.score *= -1;
			alpha.score *= -1;
			Move value = negaMax (child, depth - 1, beta, alpha, -color);
			value.score *= -1;
			bestValue = max (value, bestValue);
			alpha = max (alpha, value);
			if (alpha.score >= beta.score)
				break;
		}
		return bestValue;
	}

	private Move max(Move m1, Move m2)
	{
		if (m1.score > m2.score)
			return m1;
		else
			return m2;
	}
}
