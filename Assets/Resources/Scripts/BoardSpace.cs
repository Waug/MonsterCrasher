using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardSpace : MonoBehaviour {

	public bool SQUARETEST;
	public Movement.MOVEMENT_NOTATION NOTATIONTEST;
	public Monster.MonsterOrientation ORITEST;

	public SpaceSelection SpaceType;
	private string pointerSpace;

	private string[] boardLetters 				= 	{"AA", "A", "B", "C", "D", "E", "F", "G", "H", "HH"};
	private int[] boardNumbers 					= 	{0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
	private int[] vectorIndex 	= new int[2];
	private int[,] orientationIndex			 	= new int[,] {{-1,-1}, {0,1}, {1,-1}, {-1,0}, {1,1}, {0,-1}, {-1,1}, {1,0}};
	//{  	
//								new int[,] 	{{-1,-1}, {0,1}, {1,-1}, {-1,0}, {1,1}, {0,-1}, {-1,1}, {1,0}},
								//new int[,] 	{{-1,0}, {0,-1}, {0,-1}, {1,0}, {1,0}, {0,1}, {0,1}, {-1,0}}
//	};

	private int orientationOffset;

	private int[] homeIndex		= new int[2];
	private int[] cursorIndex	= new int[2];

	private int x;
	private int z;

	public enum SpaceSelection
	{
		Black, 
		White
	};

	// Use this for initialization
	void Start () {
		pointerSpace = gameObject.name;
		//Set coordinates for array indexes
		indexSpace (pointerSpace).CopyTo(homeIndex, 0);


		if (SQUARETEST)
			findSpaces(Movement.MOVEMENTS_AVAILABLE.KNIGHTRIGHT, Monster.MonsterOrientation.North);
	}
	
	// Update is called once per frame
	void Update () {


		
	}

	//Direction is put in as the move
	public string[] findSpaces(Movement.MOVEMENTS_AVAILABLE direction, Monster.MonsterOrientation orientation) {
		Movement.MOVEMENT_NOTATION[] moveNotation = Movement.findMoves(direction);

		Debug.Log (gameObject.name + ": ");

		string[] moveOrder = new string[moveNotation.Length];
		int[] indexOrder = new int[moveNotation.Length];
		int i = 0;
		homeIndex.CopyTo (cursorIndex, 0);

		try{
			foreach (Movement.MOVEMENT_NOTATION note in moveNotation) {
				indexOrder = parseNotation(note, orientation);
				moveOrder[i] = translateVectorIndexToCoordinate(indexOrder);
				Debug.Log(moveOrder[i]);
		}
		}catch(UnityException e) {
			//creature attempted to move outside the board boundaries
		}


		return null;


	}

	//Orientation is needed to properly adjust the coordinates from an objective board point of view.
	//Using that offset, we just rotate thru the possible changes to end up on the one we want
	//Initially we start on a part of the array, and then later we offset and rotate thru to the desired direction.
	//A "vectorIndex" is returned. It's a relative space that is applied to another space to calculate the target coordinate.
	private int[] parseNotation(Movement.MOVEMENT_NOTATION notation, Monster.MonsterOrientation orientation) {
		switch (orientation) {
		case Monster.MonsterOrientation.North:
			orientationOffset = 1;
			break;
		case Monster.MonsterOrientation.NorthWest:
			orientationOffset = 6;
			break;
		case Monster.MonsterOrientation.NorthEast:
			orientationOffset = 4;
			break;
		case Monster.MonsterOrientation.South:
			orientationOffset = 5;
			break;
		case Monster.MonsterOrientation.SouthWest:
			orientationOffset = 0;
			break;
		case Monster.MonsterOrientation.SouthEast:
			orientationOffset = 2;
			break;
		case Monster.MonsterOrientation.West:
			orientationOffset = 3;
			break;
		case Monster.MonsterOrientation.East:
			orientationOffset = 7;
			break;
		default:
			break;
		}

		switch (notation) {
		case Movement.MOVEMENT_NOTATION.H:
			vectorIndex[0] = 0;
			vectorIndex[1] = 0;
			break;
		case Movement.MOVEMENT_NOTATION.F:
			vectorIndex[0] = orientationIndex[cycle(orientationOffset,0),0];
			vectorIndex[1] = orientationIndex[cycle(orientationOffset,0),1];
			break;
		case Movement.MOVEMENT_NOTATION.B:
			vectorIndex[0] = orientationIndex[cycle(orientationOffset,4),0];
			vectorIndex[1] = orientationIndex[cycle(orientationOffset,4),1];
			break;
		case Movement.MOVEMENT_NOTATION.FP:
			vectorIndex[0] = orientationIndex[cycle(orientationOffset,5),0];
			vectorIndex[1] = orientationIndex[cycle(orientationOffset,5),1];
			break;
		case Movement.MOVEMENT_NOTATION.FS:
			vectorIndex[0] = orientationIndex[cycle(orientationOffset,3),0];
			vectorIndex[1] = orientationIndex[cycle(orientationOffset,3),1];
			break;
		case Movement.MOVEMENT_NOTATION.BP:
			vectorIndex[0] = orientationIndex[cycle(orientationOffset,7),0];
			vectorIndex[1] = orientationIndex[cycle(orientationOffset,7),1];
			break;
		case Movement.MOVEMENT_NOTATION.BS:
			vectorIndex[0] = orientationIndex[cycle(orientationOffset,1),0];
			vectorIndex[1] = orientationIndex[cycle(orientationOffset,1),1];
			break;
		case Movement.MOVEMENT_NOTATION.P:
			vectorIndex[0] = orientationIndex[cycle(orientationOffset,2),0];
			vectorIndex[1] = orientationIndex[cycle(orientationOffset,2),1];
			break;
		case Movement.MOVEMENT_NOTATION.S:
			vectorIndex[0] = orientationIndex[cycle(orientationOffset,6),0];
			vectorIndex[1] = orientationIndex[cycle(orientationOffset,6),1];
			break;
		}
		return vectorIndex;
	}

	//Used in cycling thru the possible movement differences
	private int cycle(int startingPos, int cyclePos) {
		int tempPos = startingPos + cyclePos;
		if (tempPos > 7) {
			return tempPos - 8;
		} else {
			return tempPos;
		}
	}

	//It's easier to compare number order and index limitations than letters themselves. The home space is stored as array indexes to look up later.
	private int[] indexSpace(string startingCoordinate) {
		int[] indexArray = new int[2];

		for (int i = 0; i < boardLetters.Length; i++) {
			if (startingCoordinate[0].ToString ().Equals (boardLetters [i])) {
				indexArray[0] = i;
				break;
			}
		}
		for (int i = 0; i < boardNumbers.Length; i++) {
			if (startingCoordinate[1].ToString ().Equals (boardNumbers [i].ToString ())) {
				indexArray[1] = i;
				break;
			}
		}
		return indexArray;
	}

	private int[] findIndexUsingVectorIndex(int[] vectorIndex){
		return null;
	}

	//Converts an x and y coordinate into chessboard coordinate system
	private string translateIndexSpace(int letterIndex, int numberIndex) {
		string index = "";
		index += boardLetters [letterIndex] + "" + boardNumbers [numberIndex];
		return index;
	}

	private string translateIndexSpace(int[] indexArray) {
		return translateIndexSpace (indexArray [0], indexArray [1]);
	}

	//Letter index first, then number index
	private string translateVectorIndexToCoordinate(int[] vIndex) {
		cursorIndex [0] = vIndex [0] + cursorIndex [0];
		cursorIndex [1] = vIndex [1] + cursorIndex [1];
		return translateIndexSpace(cursorIndex);
	}

	//TODO Out of bounds check

}
