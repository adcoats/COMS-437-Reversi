using UnityEngine;
using System.Collections;

public class getSize : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log (this.gameObject.name + ":\n" + transform.position.x + ", " + transform.position.y + ", " + transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
