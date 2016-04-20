using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
	public GamePiece gamePiece;
	public Player player1;
	public Player player2;
	public Player currentPlayer;
	public CollisionCube cube;
	public MoveSelector moveSelector;
	public WinText winText;

	private bool endTurn;
	private bool reset;
	public bool movesDisplayed;
	public bool pieceFlipping;

	private int width = 8;
	private int hieght = 8;
	public Vector3[,] grid;
	public GamePiece [,] pieces;
	public CollisionCube [,] cubes;
	public Vector3 initPos;
	public Vector3 player1Spawn;
	public Vector3 player2Spawn;
	public bool moveInProgress;

	private int skipped;
	// last space being animated;
	private CollisionCube last;


	// Use this for initialization
	void Awake () {
		moveSelector = new MoveSelector ();
		moveSelector.gameManager = this;

		skipped = 0;

		pieceFlipping = false;
		moveInProgress = false;
		endTurn = false;
		reset = false;
		movesDisplayed = false;
		grid = new Vector3[hieght, width];
		pieces = new GamePiece[hieght, width];
		cubes = new CollisionCube[hieght, width];
		initPos =  new Vector3(-3.5f, 1.1f, 3.5f);
		player1Spawn = new Vector3 (0, 5, 7);
		player2Spawn = new Vector3 (0, 5, -7);
		currentPlayer = player1;
		float x = initPos.x; float y = initPos.y; float z = initPos.z;
		//Debug.Log ("Setting up Vector3 grid");
		for (int i = 0; i < hieght; i++) 
		{
			x = initPos.x;
			for (int j = 0; j < width; j++) 
			{
				// set vector coordinates
				grid [i, j] = new Vector3 (x, y, z);
				//Debug.Log (x + ", " + z + "\n");
				//pieces [i, j] = (GameObject)Instantiate (gamePiece, new Vector3 (x, y, z), Quaternion.identity);
				cubes [i, j] = (CollisionCube)Instantiate (cube, new Vector3 (x, y, z), Quaternion.identity);
				cubes [i, j].setIndices (i, j);
				x += 1;
			}
			z -= 1;
		}
		last = cubes [0, 0];
	}

	void Start()
	{
		init ();
	}

	void init ()
	{
//		gamePiece.transform.position = grid [3, 3];
//		pieces [3, 3] = gamePiece.gameObject;
		pieces = new GamePiece[hieght, width];

		pieces[3,3] = (GamePiece) Instantiate (gamePiece, grid[3,3], Quaternion.identity);
		player2.addGamePiece (3, 3);
		cubes [3, 3].setGamePiece (3, 3);

		pieces[3,4] = (GamePiece) Instantiate (gamePiece, grid[3,4], Quaternion.identity);
		player1.addGamePiece (3, 4);
		cubes [3, 4].setGamePiece (3, 4);

		pieces[4,3] = (GamePiece) Instantiate (gamePiece, grid[4,3], Quaternion.identity);
		player1.addGamePiece (4, 3);
		cubes [4, 3].setGamePiece (4, 3);

		pieces[4,4] = (GamePiece) Instantiate (gamePiece, grid[4,4], Quaternion.identity);
		player2.addGamePiece (4, 4);
		cubes [4, 4].setGamePiece (4, 4);
	}

	public void addPiece(GamePiece gp, int x, int y)
	{
		pieces [x, y] = gp;
		currentPlayer.addGamePiece (x, y);
	}

	public void addPiece(int x, int y)
	{
		pieces [x, y] = (GamePiece) Instantiate (gamePiece, grid[x,y], Quaternion.identity);
		currentPlayer.addGamePiece (x, y);
	}

	public void endMyTurn()
	{
		endTurn = true;
		hideMoves ();
	}
	public void resetGame()
	{
		reset = true;
	}

//	public List<CollisionCube> getValidMoves()
//	{
//		//Debug.Log ("In method getVaildMoves");
//		List<CollisionCube> tempList = new List<CollisionCube>();
//		// loop through every square
//		for (int i = 0; i < cubes.GetLength (0); i++) 
//		{
//			//Debug.Log ("i = " + i);
//			for (int j = 0; j < cubes.GetLength (1); j++) 
//			{
//				//Debug.Log ("j = " + j);
//				// check if square is adjacent to any square containing a GamePiece of the opposite color
//				CollisionCube temp = cubes[i,j];
//				// is cube occupied?
//				if (temp.getGamePiece() != null)
//					continue;
//				bool valid = false;
//				// loop through all 8 adjacent squares until proven valid
//				for (int x = i - 1; x <= i + 1; x++) 
//				{
//					//Debug.Log ("x = " + x);
//					for (int y = j - 1; y <= j + 1; y++) 
//					{
//						//Debug.Log ("y = " + y);
//						// ignore if square is the chosen square
//						if ((x == i && y == j))
//							continue;
//						// check if space is valid based on the adjacent square
//						Debug.Log("current space: " + i + ", " + j);
//						valid = isValidMove (temp, x, y);
//						Debug.Log ("Is valid: " + valid);
//						// if proven valid, add space to list and break
//						if (valid) {
//							tempList.Add (temp);
//							break;
//						}
//					}
//					// if valid, break
//					if (valid) {break;}
//				}
//				//tempList.Add(temp);
//			}
//		}
//
//		return tempList;
//
//
//		// for each adjacent piece of opposite color, continue searching in that
//		// direction until you find a gap (invalid) or a piece of the same color.
//
//		// for each of the opponents pieces colected in search, build new board state.
//
//		// send this board state to the minimax algorithm, scored by number of pieces each
//		// player has on the board.
//	}

//	public bool isOutOfBounds(int x, int y)
//	{
//		if ( ( x < 0 || y < 0 ) || ( x >= cubes.GetLength(0) || y >= cubes.GetLength(1) ) ) {
//			return true;
//		} else {
//			return false;
//		}
//	}
//	private bool isValidMove(CollisionCube space, int x, int y)
//	{
//		//  check if out of bounds
//		Debug.Log("Checking bounds: " + x + ", " + y);
//		if (!isOutOfBounds(x, y))
//		{
//			Debug.Log ("Within bounds: " + x + ", " + y);
//			CollisionCube adjacent = cubes [x, y];
//			// space is occupied by a GamePiece
//			if (adjacent.getGamePiece() != null) {
//				Debug.Log ("Adjacent has a GamePiece");
//				// player and GamePiece are different colors
//				Debug.Log ("Player is white:" + currentPlayer.isWhite + ", Adjacent piece is white: " + adjacent.getGamePiece().isWhite);
//				if (currentPlayer.isWhite != adjacent.getGamePiece().isWhite) {
//					return true;
//				}
//			}
//		}
//		return false;
//	}

	public List<Move> displayAvailableMoves(bool ai)
	{
		List<Move> moves = new List<Move> ();
		if (currentPlayer.Equals(player1))
			moves = moveSelector.getValidMoves(1, ai);
		else
			moves = moveSelector.getValidMoves(-1, ai);
		Debug.Log (moves.Count);
		setMoves (true, moves);
		movesDisplayed = true;
		if (moves.Count < 1)
			skipped++;
		else
			skipped = 0;
		return moves;
	}

	public void hideMoves ()
	{
		setMoves (false, cubes);
		movesDisplayed = false;
	}

	// enables or disables the renderer for the selection of CollisionCube objects
	public void setMoves(bool status, CollisionCube [,] tempCubes)
	{
		for (int i = 0; i < cubes.GetLength(0); i++) {
			for (int j = 0; j < cubes.GetLength (1); j++) {
				tempCubes [i, j].setRenderer (status);
				tempCubes [i, j].enableClick = status;
				if (status == false)
					tempCubes [i, j].move = null;
			}
		}
	}
	public void setMoves(bool status, List<CollisionCube> tempCubes)
	{
		Debug.Log (tempCubes.Count);
		for (int i = 0; i < tempCubes.Count; i++) {
			tempCubes[i].setRenderer (status);
			tempCubes [i].enableClick = status;
			if (status == false)
				tempCubes [i].move = null;
		}
	}

	public void setMoves (bool status, List<Move> moves)
	{
		foreach (Move temp in moves) 
		{
			cubes [(int)temp.move.x, (int)temp.move.y].setRenderer (status);
			cubes [(int)temp.move.x, (int)temp.move.y].enableClick = status;
			if (status == true)
				cubes [(int)temp.move.x, (int)temp.move.y].move = temp;
			else 
				cubes [(int)temp.move.x, (int)temp.move.y].move = null;
		}
	}

	// Update is called once per frame
	void Update () {
		if (endTurn) {
			if (currentPlayer.Equals (player1)) {
				currentPlayer = player2;

			} else {
				currentPlayer = player1;
			}
			endTurn = false;
		}
		if (reset) {
			SceneManager.LoadScene("Reversi");
			reset = false;
		}
		if (skipped >= 2) 
		{
			// show win text
			winText.activate ();
			// disable interaction
			for(int x = 0; x < cubes.GetLength(0); x++) {
				for (int y = 0; y < cubes.GetLength (1); y++) {
					cubes [x, y].enableClick = false;
				}
			}
		}
		if (last.getGamePiece() != null && last.getGamePiece().flip == false)
			moveInProgress = false;
	}

	public void applyMove(Move move)
	{
		moveSelector.setBoard (move.board);
		//flip pieces
		//CollisionCube temp = cubes[(int)move.changes[move.changes.Count-1].x, (int)move.changes[move.changes.Count-1].y];
		last = cubes[(int)move.changes[move.changes.Count-1].x, (int)move.changes[move.changes.Count-1].y];
		foreach (Vector2 pos in move.changes) 
		{
			GamePiece gp = cubes [(int)pos.x, (int)pos.y].getGamePiece();
			// flip piece
			gp.enableFlip ();
//			// swap piece between players
//			if (player1.getPieces ().Contains (gp)) {
//				player1.removePiece (gp);
//			} else {
//				player1.addGamePiece (gp);
//			}
//			if (player2.getPieces ().Contains (gp)) {
//				player2.removePiece (gp);
//			} else {
//				player2.addGamePiece (gp);
//			}

			updatePlayer (player1, move);
			updatePlayer (player2, move);
		}
		endMyTurn ();

		// allow time for animations
		System.Diagnostics.Stopwatch s = new System.Diagnostics.Stopwatch();
//		s.Start ();
//		while (s.Elapsed < System.TimeSpan.FromSeconds (2)) {}
//		s.Stop ();

		//moveInProgress = false;
	}

	void updatePlayer(Player p, Move m)
	{
		p.removeAllPieces ();
		for (int x = 0; x < m.board.GetLength (0); x++) 
		{
			for (int y = 0; y < m.board.GetLength (1); y++) 
			{
				if (p.isWhite) {
					if (m.board [x, y] == -1) {
						p.addGamePiece (cubes [x, y].getGamePiece ());
					}
				} else {
					if (m.board [x, y] == 1) {
						p.addGamePiece (cubes [x, y].getGamePiece ());
					}
				}
			}
		}
	}








//	public List<CollisionCube> getValidMoves2()
//	{
//		// list of valid cubes
//		List<CollisionCube> tempCubes = new List<CollisionCube>();
//		// list of opponents GamePieces
//		List<GamePiece> tempPieces;
//		if (currentPlayer.Equals (player1)) {
//			tempPieces = player2.getPieces ();
//		} else {
//			tempPieces = player1.getPieces ();
//		}
//
//		// check adjacent squares for valid moves
//		foreach (GamePiece gp in tempPieces) 
//		{
//			// check all 8 adjacent squares
//			for (int x = gp.x - 1; x <= gp.x + 1; x++) 
//			{
//				for (int y = gp.y - 1; y <= gp.y + 1; y++) 
//				{
//					if (x == gp.x && y == gp.y)
//						continue;
//					// make sure square is within the bound of the board
//					Debug.Log("Checking bounds: " + x + ", " + y);
//					if (!isOutOfBounds (x, y)) 
//					{
//						Debug.Log ("Within bounds: " + x + ", " + y);
//						CollisionCube adjacent = cubes [x, y];
//						// make sure space is empty
//						if (adjacent.getGamePiece () == null) 
//						{
//							Debug.Log ("Adjacent is empty");
//							// make sure space isn't already in list
//							if (!tempCubes.Contains (adjacent)) {
//								//Debug.Log ("Adding cube to list");
//								// check for opponent colored pieces in spaces adjacent to the potential move
//								for (int i = x - 1; i <= x + 1; i++) 
//								{
//									for (int j = y - 1; j <= y + 1; j++) 
//									{
//										Debug.Log("Checking bounds: " + i + ", " + j);
//										// make sure square is within the bound of the board
//										if (!isOutOfBounds (i, j)) 
//										{
//											Debug.Log ("Within bounds: " + i + ", " + j);
//											CollisionCube adjacent2 = cubes [i, j];
//											// make sure space is not empty
//											if (adjacent2.getGamePiece () != null) 
//											{
//												// make sure the game piece belongs to the opponent
//												if (player1.isWhite != adjacent2.getGamePiece ())
//												{
//													int dx = i - x;
//													int dy = j - y;
//													Vector2 vect2 = new Vector2 (dx, dy);
//
//												}
//											}
//										}
//									}
//								}
//
//
//								// 
//								tempCubes.Add (adjacent);
//							}
//						}
//					}
//				}
//			}
//		}
//		return tempCubes;
//	}

//	private bool isValidMove2(CollisionCube space, int x, int y)
//	{
//		//  check if out of bounds
//		Debug.Log("Checking bounds: " + x + ", " + y);
//		if (!isOutOfBounds(x, y))
//		{
//			Debug.Log ("Within bounds: " + x + ", " + y);
//			CollisionCube adjacent = cubes [x, y];
//			// space is occupied by a GamePiece
//			if (adjacent.getGamePiece() != null) {
//				Debug.Log ("Adjacent has a GamePiece");
//				// player and GamePiece are different colors
//				Debug.Log ("Player is white:" + currentPlayer.isWhite + ", Adjacent piece is white: " + adjacent.getGamePiece().isWhite);
//				if (currentPlayer.isWhite != adjacent.getGamePiece().isWhite) {
//					return true;
//				}
//			}
//		}
//		return false;
//	}
}