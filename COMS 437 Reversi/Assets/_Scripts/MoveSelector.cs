using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class MoveSelector //: MonoBehaviour 
{

	public GameManager gameManager;


	private int [,] board;
	private int width, height;

	void Awake()
	{
		width = height = 8;
		board = new int[width, height];
		init ();
	}

	public MoveSelector()
	{
		width = height = 8;
		this.board = new int[width, height];
		init ();
	}
	void init()
	{
		for (int x = 0; x < width; x++) 
		{
			for (int y = 0; y < height; y++) 
			{
				board [x, y] = 0;
			}
		}
		this.board [3, 3] = -1;
		this.board [3, 4] = 1;
		this.board [4, 3] = 1;
		this.board [4, 4] = -1;
	}

	public void setBoard(int [,] newBoard)
	{
		this.board = newBoard;
	}

	public bool isOutOfBounds(int x, int y)
	{
		if ( ( x < 0 || y < 0 ) || ( x >= this.board.GetLength(0) || y >= this.board.GetLength(1) ) ) {
			return true;
		} else {
			return false;
		}
	}

	public List<Move> getValidMoves(int player, bool ai)
	{
		//printBoard ();

		List<Vector2> pieces;
		List<GamePiece> pieceObjects = new List<GamePiece> ();;
		List<Move> moves = new List<Move>();
		pieces = new List<Vector2> ();
		if (!ai) {
			if (player == 1) {
				pieceObjects = gameManager.player2.getPieces ();
			} else if (player == -1) {
				pieceObjects = gameManager.player1.getPieces ();
			}
			// list of piece positions
			//pieces = new List<Vector2> ();
			foreach (GamePiece gp in pieceObjects) {
				pieces.Add (new Vector2 (gp.x, gp.y));
				//Debug.Log ("Loading Piece " + gp.x + ", " + gp.y);
			}
		} else { // for negamax algorithm
			for (int x = 0; x < board.GetLength (0); x++) {
				for (int y = 0; y < board.GetLength (1); y++) {
					if (board [x, y] == player * -1) {
						pieces.Add(new Vector2(x, y));
					}
				}
			}
		}

		// loop through pieces and get valid positions
		foreach (Vector2 piece in pieces) 
		{
			// check all 8 adjacent spaces
			for (int x = (int)piece.x - 1; x <= (int)piece.x + 1; x++) 
			//for (int y = (int)piece.y - 1; y <= (int)piece.y + 1; y++)
			{
				for (int y = (int)piece.y - 1; y <= (int)piece.y + 1; y++) 
				//for (int x = (int)piece.x - 1; x <= (int)piece.x + 1; x++)
				{
					//Debug.Log ("Examining space " + x + ", " + y + " adjacent to piece " + (int)piece.x + ", " + (int)piece.y);
					// skip self
					if (x == (int)piece.x && y == (int)piece.y)
						continue;
					// space is within bouns and is empty
					if (!isOutOfBounds(x, y) && board[x,y] == 0)
					{
						//Debug.Log ("Found empty space " + x + ", " + y);
						//collect changes this move would cause. if no changes, not valid.
						Move move = new Move();
						move.move = new Vector2 (x, y);
						move.moveValue = player;
						move.board = new int[width,height];
						Array.Copy (this.board, move.board, this.board.Length);
						//move.board = this.board;
						move.changes = new List<Vector2> ();
						//Debug.Log ("Move: " + move.move.x + ", " + move.move.y);
						// examine all eight directions from space for pieces to flip
						for (int i = (int)move.move.x - 1; i <= (int)move.move.x + 1; i++) 
						//for (int j = (int)move.move.y - 1; j <= (int)move.move.y + 1; j++)
						{
							for (int j = (int)move.move.y - 1; j <= (int)move.move.y + 1; j++)
							//for (int i = (int)move.move.x - 1; i <= (int)move.move.x + 1; i++)	
							{
								//Debug.Log ("Checking space " + i + ", " + j + " from space " + (int)move.move.x + ", " + (int)move.move.y);
								// skip self
								if (i == (int)move.move.x && j == (int)move.move.y)
									continue;
								// get direction from move
								int dx = i - (int)move.move.x;
								int dy = j - (int)move.move.y;
								//Debug.Log ("Direction: " + dx + ", " + dy);
								// get traversal indexes;
								int tempX = i;
								int tempY = j;

								// value of move
								int temp = move.moveValue;
								// piece is within bounds and is an opponent's piece
								List<Vector2> tempVects = new List<Vector2>();
								//Debug.Log (tempX + ", " + tempY + " = " + this.board[tempX, tempY]);
								while(!isOutOfBounds(tempX, tempY) && this.board [tempX, tempY] == (player*-1))
								{
									// add position to list
									//Debug.Log("Adding " + tempX + ", " + tempY + " to tempVects...");
									tempVects.Add (new Vector2 (tempX, tempY));
									// increment position
									tempX += dx;
									tempY += dy;
								}
								// check why loop was broken
								// if space is out of bounds or empty, discard
								if (isOutOfBounds (tempX, tempY) || this.board[tempX, tempY] == 0 || tempVects.Count < 1) 
								{
									//Debug.Log ("Discarding changes");
									continue;
								}// if found one of players pieces, add to changes
								else if (this.board[tempX, tempY] == player && tempVects.Count > 0)
								{
									//Debug.Log ("Adding changes...");
									move.changes.AddRange (tempVects);
								}
							}
						}
						// finished finding changes
						// check if move has changes, if so, add to moves
						if (move.changes.Count > 0) 
						{
							move.applyChanges ();
							move.score = move.getBoardScore ();
							moves.Add (move);
							//Debug.Log ("Added move: " + move.move.x + ", " + move.move.y);
						}

					}
				}
			}
		}
		return moves;
	}

	void printBoard()
	{
		Debug.Log ("Board Dimensions: " + board.GetLength (0) + ", " + board.GetLength (1));
		//string s = "";
		System.Text.StringBuilder sb = new System.Text.StringBuilder ("");
		//for (int y = 0; y < board.GetLength (1); y++) 
		for (int x = 0; x < board.GetLength (0); x++)
		{
			//s += "[";
			sb.Append("[");
			//for (int x = 0; x < board.GetLength (0); x++) 
			for (int y = 0; y < board.GetLength (1); y++)
			{
				//s += board [x, y] + ", ";
				if (board[x,y] != -1)
					sb.Append (" " + board [x, y] + ", ");
				else
					sb.Append (board [x, y] + ", ");
			}
			//s += "]" + System.Environment.NewLine;
			sb.Append ("]" + System.Environment.NewLine);
			//Debug.Log ("y: " + y);
			//Debug.Log(s);
		}
		//Debug.Log (s);
		Debug.Log(sb.ToString());
		//Console.Write (s);
	}

	// get game state represented by a Move object
	public Move evaluate(int player)
	{
		Move state = new Move ();

		state.board = new int[width,height];
		Array.Copy (this.board, state.board, this.board.Length);

		state.score = state.getBoardScore ();

		state.getMoves (player);

		return state;
	}
}
