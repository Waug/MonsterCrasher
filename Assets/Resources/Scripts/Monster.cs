using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour {
	
	public int power;
	public int defense;
	public MonsterOrientation orientation;
	public GameObject homeSpace;
	
	private MonsterOrientation defaultOrientation;
	private int orientationIndex;
	
	public static MonsterOrientation[] orientationArray = new MonsterOrientation[] { MonsterOrientation.North, MonsterOrientation.NorthEast, MonsterOrientation.East, MonsterOrientation.SouthEast, MonsterOrientation.South, MonsterOrientation.SouthWest, MonsterOrientation.West, MonsterOrientation.NorthWest };
	
	
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
		
		defaultOrientation = MonsterOrientation.North;
		//IF is player one, default  = north
		//IF is player two, default = south TODO
		//IF default is different, make case for change in starting orientation.
		
		orientation = defaultOrientation;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	//REMOVE AFTER TESTING
	public void move() {
		move(Movement.MOVEMENTS_AVAILABLE.FORWARD);
	}
	public void move(Movement.MOVEMENTS_AVAILABLE movement) {
		//Debug.Log("I am here at " + homeSpace.name);
		string[] moveOrder = new string[10];
		homeSpace.GetComponent<BoardSpace>().findSpaces(movement, orientation).CopyTo(moveOrder, 0);
		foreach (string moveSpace in moveOrder) {
			if (moveSpace != null) {
				GameObject moveTarget = GameObject.Find(moveSpace);
				Vector3 tempPos = moveTarget.transform.position;
				tempPos.y = 0.75f;
				gameObject.transform.position = tempPos;
				homeSpace = moveTarget;
			}
			
		}
		
		
	}
	
	public GameObject getMovementSpaces() {
		return null;
	}
	
	public GameObject getHomeSpace() {
		return homeSpace;
	}
	
	//changes orientaiton and returns the result
	public void rotateClockwise() {
		int tempOriIndex = orientationIndex + 1;
		if (tempOriIndex >= orientationArray.Length)
			tempOriIndex -= orientationArray.Length;
		
		setOrientation(tempOriIndex);
		transform.Rotate(0, transform.rotation.y + 45, 0);
		//return FindEnumForOrientationFromIndex(tempOriIndex);
	}
	
	public void RotateCounterClockwise() {
		int tempOriIndex = orientationIndex - 1;
		if (tempOriIndex < 0)
			tempOriIndex += orientationArray.Length;
		
		setOrientation(tempOriIndex);
		transform.Rotate(0, transform.rotation.y - 45, 0);
		//return FindEnumForOrientationFromIndex(tempOriIndex);
		
	}
	
	public MonsterOrientation setOrientation(int indexOri) {
		orientation = FindEnumForOrientationFromIndex(indexOri);
		orientationIndex = indexOri;
		return orientation;
	}
	public MonsterOrientation setOrientation(MonsterOrientation enumOrientation) {
		orientation = enumOrientation;
		orientationIndex = FindIndexForOrientationFromEnum(enumOrientation);
		
		return defaultOrientation;
	}
	
	public MonsterOrientation getOrientation() {
		return orientation;
	}
	
	
	//RECURSIVE WATCH OUT LOL
	private int FindIndexForOrientationFromEnum(MonsterOrientation findOri) {
		for(int i = 0; i < orientationArray.Length; i++) {
			if(findOri == orientationArray[i]) {
				return i;
			}
			return FindIndexForOrientationFromEnum(defaultOrientation);
		}
		return 1; //unreachable
	}
	
	private MonsterOrientation FindEnumForOrientationFromIndex(int findOri) {
		return orientationArray[findOri];
	}
	
}