using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour {

	public int power;
	public int defense;
	private GameObject homeSpace;

	//Monster orientation is objective to the board.
	//Directions are dependent on Orientation.
	//P1 starts monsters oriented North; P2 starts monsters oriented South.
	public enum MonsterOrientation {
		North,
		NorthWest,
		NorthEast,
		South,
		SouthWest,
		SouthEast,
		West,
		East
	};

	private Movement[] MovementOptions;
	private Movement movesAvailable;

	// Use this for initialization
	void Start () {
		if (gameObject.GetComponent<Movement>() != null) {
			movesAvailable =  gameObject.GetComponent<Movement>();

		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public GameObject getMovementSpaces() {
		return null;
	}

	public GameObject getHomeSpace() {
		return homeSpace;
	}
}
