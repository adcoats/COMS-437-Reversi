using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour {
	public GamePiece gamePiece;
	public Player player1;
	public Player player2;
	public Player currentPlayer;
	public CollisionCube cube;

	public bool endTurn;
	public bool reset;

	private int width = 8;
	private int hieght = 8;
	public Vector3[,] grid;
	public GamePiece [,] pieces;
	public CollisionCube [,] cubes;
	public Vector3 initPos;

	// Use this for initialization
	void Awake () {
		endTurn = false;
		reset = false;
		grid = new Vector3[hieght, width];
		pieces = new GamePiece[hieght, width];
		cubes = new CollisionCube[hieght, width];
		initPos =  new Vector3(-3.5f, 1.1f, 3.5f);
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
		player1.addGamePiece (3, 3);
		cubes [3, 3].setGamePiece (3, 3);

		pieces[3,4] = (GamePiece) Instantiate (gamePiece, grid[3,4], Quaternion.identity);
		player2.addGamePiece (3, 4);
		cubes [3, 4].setGamePiece (3, 4);

		pieces[4,3] = (GamePiece) Instantiate (gamePiece, grid[4,3], Quaternion.identity);
		player2.addGamePiece (4, 3);
		cubes [4, 3].setGamePiece (4, 3);

		pieces[4,4] = (GamePiece) Instantiate (gamePiece, grid[4,4], Quaternion.identity);
		player1.addGamePiece (4, 4);
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
//			player1 = new Player ();
//			player2 = new Player ();
//			player1.isWhite = false;
//			player2.isAI = true;
//			player2.isWhite = true;
//			currentPlayer = player1;
//			Awake ();
//			Start ();
			SceneManager.LoadScene("Reversi");
			reset = false;
		}
	}
}