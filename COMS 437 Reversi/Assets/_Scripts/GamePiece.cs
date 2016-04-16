using UnityEngine;
using System.Collections;

public class GamePiece : MonoBehaviour {

	bool flip;
	bool move;
	int caseNum;
	int rotated;
	int speed;
	//bool endTranslate;
	Quaternion target;
	Vector3 destination;
	void Awake()
	{
		flip = false;
		move = false;
		speed = 6;
		//endTranslate = false;
		caseNum = 0;
		//target = Quaternion.LookRotation(-transform.forward, Vector3.up);
		destination = transform.position;


	}

	// Use this for initialization
	void Start () {
		
	}

//	void OnMouseDown()
//	{
//		if (!flip)
//		{
//			//target.x *= -1;
//			target = Quaternion.LookRotation (-transform.forward, Vector3.up);
//			rotated = 0;
//			destination.y += 1;
//			flip = true;
//			caseNum = 1;
//			speed = 3;
//		}
//	}

	void enableFlip ()
	{
		if (!flip)
		{
			//target.x *= -1;
			target = Quaternion.LookRotation (-transform.forward, Vector3.up);
			rotated = 0;
			destination.y += 1;
			flip = true;
			caseNum = 1;
			//speed = 3;

		}
	}

	public void enableMove(Vector3 dest)
	{
		if (!move)
		{
			Debug.Log ("dest:");
			Debug.Log (dest);
			move = true;
			destination = dest;
		}
	}

	void movePiece()
	{
		if (Vector3.Distance (transform.position, destination) > 0.05f) {
			Debug.Log (Vector3.Distance (transform.position, destination));
			Debug.Log ("Current Position:");
			Debug.Log (transform.position);
			Debug.Log ("Destination:");
			Debug.Log (destination);

//			Vector3 direction = destination - transform.position;
//			direction.Normalize ();
//			this.transform.Translate (direction.x * speed * Time.deltaTime, direction.y * speed * Time.deltaTime, direction.z * speed * Time.deltaTime, Space.World);
			//Vector3 temp = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
			Debug.Log ("speed:");
			Debug.Log (speed);
			Vector3 temp = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime * 3);
			Debug.Log ("temo:");
			Debug.Log (temp);
			transform.position = temp;
		} else {
			transform.position = destination;
			move = false;
			Debug.Log ("done");
			//enableFlip ();
		}
	}

	// Update is called once per frame
	void Update () {
		if (flip) {
			flipPiece ();
		} else if (move) {
			movePiece ();
		}

	}

	void flipPiece()
	{
		switch (caseNum) {
		case 1:
			// rise by 1
			if (Vector3.Distance (transform.position, destination) > 0.05f) {
				Vector3 direction = destination - transform.position;
				direction.Normalize ();
				transform.Translate (direction.x * speed * Time.deltaTime, direction.y * speed * Time.deltaTime, direction.z * speed * Time.deltaTime, Space.World);
			} else {
				transform.position = destination;
				destination.y -= 1;
				caseNum = 2;
			}
			break;
		case 2:
			//flip piece over
			//if (Quaternion.Angle (transform.rotation, target) > 0.5) {
			if (rotated < 180) {
				//transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime);
				//transform.rotation = Quaternion.Lerp (transform.rotation, target, 0.05f);
				transform.Rotate(0,0,speed);
				rotated += speed;
				Debug.Log ("Rotated:\n" + rotated);

			} else {
				//transform.rotation = target;
				caseNum = 3;
			}
			break;
		case 3:
			// lower by 1
			if (Vector3.Distance (transform.position, destination) > 0.05f) {
				Vector3 direction = destination - transform.position;
				direction.Normalize ();
				transform.Translate (direction.x * speed * Time.deltaTime, direction.y * speed * Time.deltaTime, direction.z * speed * Time.deltaTime, Space.World);
			} else {
				caseNum = 0;
				flip = false;
			}
			break;
		default:

			break;
		}
	}
}
