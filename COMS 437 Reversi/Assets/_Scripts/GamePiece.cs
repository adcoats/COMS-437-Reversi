using UnityEngine;
using System.Collections;

public class GamePiece : MonoBehaviour {

	bool flip;
	int caseNum;
	int rotated;
	int speed;
	//bool endTranslate;
	Quaternion target;
	Vector3 destination;
	void Awake()
	{
		flip = false;
		//endTranslate = false;
		caseNum = 0;
		//target = Quaternion.LookRotation(-transform.forward, Vector3.up);
		destination = transform.position;


	}

	// Use this for initialization
	void Start () {
		
	}

	void OnMouseDown()
	{
		if (!flip)
		{
			//target.x *= -1;
			target = Quaternion.LookRotation (-transform.forward, Vector3.up);
			rotated = 0;
			destination.y += 1;
			flip = true;
			caseNum = 1;
			speed = 3;
		}
	}

	// Update is called once per frame
	void Update () {
		if (flip) {
			flipPiece ();
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
				transform.Rotate(0,0,3);
				rotated += 3;
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
