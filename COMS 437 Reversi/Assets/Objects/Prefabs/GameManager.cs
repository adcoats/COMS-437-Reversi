using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public GamePiece gamePiece;
	public Player player1;
	public Player player2;

	private int width = 8;
	private int hieght = 8;
	public Vector3[,] grid;
	public GamePiece [,] pieces;
	public Vector3 initPos;

	// Use this for initialization
	void Start () {
		grid = new Vector3[hieght, width];
		pieces = new GamePiece[hieght, width];
		initPos =  new Vector3(-3.5f, 1.1f, 3.5f);
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
				x += 1;
			}
			z -= 1;
		}
		init ();
	}

	void init ()
	{
//		gamePiece.transform.position = grid [3, 3];
//		pieces [3, 3] = gamePiece.gameObject;

		pieces[3,3] = (GamePiece) Instantiate (gamePiece, grid[3,3], Quaternion.identity);
		player1.addGamePiece (3, 3);

		pieces[3,4] = (GamePiece) Instantiate (gamePiece, grid[3,4], Quaternion.identity);
		player2.addGamePiece (3, 4);

		pieces[4,3] = (GamePiece) Instantiate (gamePiece, grid[4,3], Quaternion.identity);
		player2.addGamePiece (4, 3);

		pieces[4,4] = (GamePiece) Instantiate (gamePiece, grid[4,4], Quaternion.identity);
		player1.addGamePiece (4, 4);
	}

	// Update is called once per frame
	void Update () {
	
	}
}