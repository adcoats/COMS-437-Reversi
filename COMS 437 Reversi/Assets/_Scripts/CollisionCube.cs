using UnityEngine;
using System.Collections;

public class CollisionCube : MonoBehaviour {
	public GameManager gameManager;
	public GamePiece gamePiece;
	public Move move;
	private GamePiece myPiece;
	public bool enableClick;
	private int x, y;

	// Use this for initialization
	void Start () {
//		MeshRenderer mr = gameObject.GetComponent<MeshRenderer> ();
//		Material m = new Material (Shader.Find ("Unlit/Transparent"));
//		Color c = new Color (1.0f, 0.0f, 1.0f, 0.75f);
//
//		m.color = c;
//		mr.material = m;


		Renderer r = gameObject.GetComponent<Renderer> ();
		r.material.SetColor ("_Color", new Color (1.0f, 0.0f, 1.0f, 0.25f));
		r.enabled = false;
		enableClick = false;

		//r.material.color = new Color (1.0f, 0.0f, 1.0f, 0.25f);
	}

	void OnMouseDown()
	{
		applyMove ();
	}
	public void applyMove()
	{
		if (myPiece == null && enableClick) {
			gameManager.moveInProgress = true;
			// spawn and move GamePiece to CollisionCube
			if (gameManager.currentPlayer.Equals (gameManager.player1)) {
				myPiece = (GamePiece)Instantiate (gamePiece, gameManager.player1Spawn, Quaternion.identity);
			} else {
				myPiece = (GamePiece)Instantiate (gamePiece, gameManager.player2Spawn, Quaternion.identity);
			}
			myPiece.x = this.x;
			myPiece.y = this.y;
			myPiece.enableMove (transform.position);
			gameManager.applyMove (move);
			gameManager.addPiece (myPiece, x, y);
			gameManager.endMyTurn ();
		}
	}

	public void flipPiece()
	{
		myPiece.enableFlip ();
	}

	// Update is called once per frame
	void Update () {

	}
	public void setRenderer(bool status)
	{
		Renderer r = gameObject.GetComponent<Renderer> ();
		r.enabled = status;
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
		myPiece.x = this.x;
		myPiece.y = this.y;
	}
	public void setGamePiece(int x, int y)
	{
		myPiece = gameManager.pieces [x, y];
		myPiece.x = this.x;
		myPiece.y = this.y;
	}

	public GamePiece getGamePiece()
	{
		return myPiece;
	}
}
