using UnityEngine;
using System.Collections;

public class CollisionCube : MonoBehaviour {
	public GameManager gameManager;
	public GamePiece gamePiece;
	private GamePiece myPiece;
	private int x, y;

	// Use this for initialization
	void Start () {
	
	}

	void OnMouseDown()
	{
		if (myPiece == null) {
			Debug.Log (gameManager);
			myPiece = (GamePiece)Instantiate (gamePiece, transform.position, Quaternion.identity);
			gameManager.addPiece (myPiece, x, y);
		}
	}

	// Update is called once per frame
	void Update () {

	}

	public void setIndices(int x, int y)
	{
		this.x = x;
		this.y = y;
	}

	public Vector2 getIndices()
	{
		return new Vector2 (x, y);
	}

	public void setGamePiece(GamePiece gp)
	{
		myPiece = gp;
	}
	public void setGamePiece(int x, int y)
	{
		myPiece = gameManager.pieces [x, y];
	}
}
