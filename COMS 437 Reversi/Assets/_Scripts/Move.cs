using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Move// : MonoBehaviour 
{

	public Vector2 move;
	public int moveValue;
	public List<Vector2> changes;
	public int [,] board;
	public int score;
	public List<Move> moves;

	public int getBoardScore()
	{
		int temp = 0;;
		for (int x = 0; x < board.GetLength (0); x++) 
		{
			for (int y = 0; y < board.GetLength (1); y++) 
			{
				temp += board [x, y];
			}
		}
		return temp;
	}

	public void applyChanges()
	{
		board [(int)move.x, (int)move.y] = moveValue;
		foreach (Vector2 temp in changes) 
		{
			board [(int)temp.x, (int)temp.y] *= -1;
		}
	}

	public List<Move> getMoves(int player)
	{
		MoveSelector ms = new MoveSelector ();
		int[,] tempBoard = new int[8, 8];
		Array.Copy (this.board, tempBoard, this.board.Length);
		ms.setBoard (tempBoard);
		moves = ms.getValidMoves (player, true);
		return moves;
	}
}
