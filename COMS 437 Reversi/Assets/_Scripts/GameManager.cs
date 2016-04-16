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

	private bool endTurn;
	private bool reset;
	private bool movesDisplayed;

	private int width = 8;
	private int hieght = 8;
	public Vector3[,] grid;
	public GamePiece [,] pieces;
	public CollisionCube [,] cubes;
	public Vector3 initPos;
	public Vector3 player1Spawn;
	public Vector3 player2Spawn;

	// Use this for initialization
	void Awake () {
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
		Debug.Log ("Setting up Vector3 grid");
		for (int i = 0; i < hieght; i++) 
		{
			x = initPos.x;
			for (int j = 0; j < width; j++) 
			{
				// set vector coordinates
				grid [i, j] = new Vector3 (x, y, z);
				Debug.Log (x + ", " + z + "\n");
				//pieces [i, j] = (GameObject)Instantiate (gamePiece, new Vector3 (x, y, z), Quaternion.identity);
				cubes [i, j] = (CollisionCube)Instantiate (cube, new Vector3 (x, y, z), Quaternion.identity);
				cubes [i, j].setIndices (i, j);
				x += 1;
			}
			z -= 1;
		}

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
	}
	public void resetGame()
	{
		reset = true;
	}

	public List<CollisionCube> getValidMoves()
	{
		List<CollisionCube> tempList = new List<CollisionCube>();
		// loop through every square
		for (int i = 0; i < cubes.GetLength (0); i++) {
			for (int j = 0; j < cubes.GetLength (1); j++) {
				// check if square is adjacent to any square of the opposite color
				CollisionCube temp = cubes[i,j];
				int x = i;
				int y = j;


				tempList.Add(temp);

			}
		}

		return tempList;


		// for each adjacent piece of opposite color, continue searching in that
		// direction until you find a gap (invalid) or a piece of the same color.

		// for each of the opponents pieces colected in search, build new board state.

		// send this board state to the minimax algorithm, scored by number of pieces each
		// player has on the board.
	}

	public void displayAvailableMoves()
	{
		setMoves (true, getValidMoves());
		movesDisplayed = true;
	}

	public void hideMoves ()
	{
		setMoves (false, cubes);
		movesDisplayed = false;
	}

//	public void setMoves(bool status, CollisionCube [,] tempCubes)
//	{
//		for (int i = 0; i < cubes.GetLength(0); i++) {
//			for (int j = 0; j < cubes.GetLength (1); j++) {
//				tempCubes [i, j].setRenderer (status);
//			}
//		}
//	}
	public void setMoves(bool status, List<CollisionCube> tempCubes)
	{
		for (int i = 0; i < cubes.GetLength(0); i++) {
			tempCubes[i].setRenderer (status);
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
	}
}