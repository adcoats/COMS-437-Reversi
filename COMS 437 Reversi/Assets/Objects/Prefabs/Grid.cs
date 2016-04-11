using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {
	public GameObject gamePiece;
	private int width = 8;
	private int hieght = 8;
	public Vector3[,] grid;
	public GameObject [,] pieces;
	public Vector3 initPos;

	// Use this for initialization
	void Start () {
		grid = new Vector3[hieght, width];
		pieces = new GameObject[hieght, width];
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
		pieces [3, 3] = gamePiece;
		pieces[3,4] = (GameObject)Instantiate (gamePiece, grid[3,4], Quaternion.identity);
		pieces[4,3] = (GameObject)Instantiate (gamePiece, grid[4,3], Quaternion.identity);
		pieces[4,4] = (GameObject)Instantiate (gamePiece, grid[4,4], Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
