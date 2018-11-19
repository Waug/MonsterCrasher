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
	private int[] targetCoordinateAdjustment 	= new int[2];
	private int[,] orientationIndex			 	= new int[,] {{-1,-1}, {0,1}, {1,-1}, {-1,0}, {1,1}, {0,-1}, {-1,1}, {1,0}};
	//{  	
//								new int[,] 	{{-1,-1}, {0,1}, {1,-1}, {-1,0}, {1,1}, {0,-1}, {-1,1}, {1,0}},
								//new int[,] 	{{-1,0}, {0,-1}, {0,-1}, {1,0}, {1,0}, {0,1}, {0,1}, {-1,0}}
//	};

	private int orientationOffset;

	private int homeLetterIndex;
	private int homeNumberIndex;
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
		parseBoardIndex (pointerSpace, true);
	}
	
	// Update is called once per frame
	void Update () {

		if (SQUARETEST)
			parseNotation(NOTATIONTEST, ORITEST);
		
	}

	public string findSpace(Movement.MOVEMENTS_AVAILABLE direction, Monster.MonsterOrientation orientation) {
		Movement.MOVEMENT_NOTATION[] moveNotation = Movement.findMove(direction);

		string[] moveOrder = new string[moveNotation.Length];
		int i = 0;
		foreach (Movement.MOVEMENT_NOTATION note in moveNotation) {
			moveOrder[i] = parseNotation(note, orientation);
		}
		return null;


	}

	private string parseNotation(Movement.MOVEMENT_NOTATION notation, Monster.MonsterOrientation orientation) {
		//Orientation is needed to properly adjust the coordinates from an objective board point of view.
		//using the jagged, multidimensional array, I discovered that orientations rotate by the same difference starting at a different location in the sequence.
		//Using that offset, we just rotate thru the possible changes to end up on the one we want. Diagonals follow a different pattern,
		//position 1 tells us which array to use, position 2 tells us where to start rotating
		switch (orientation) {
		case Monster.MonsterOrientation.North:
			//x = 0;
			//z = 1;
			//orientationOffset = 0;
			orientationOffset = 1;
			break;
		case Monster.MonsterOrientation.NorthWest:
			//x = -1;
			//z = 1;
			//orientationOffset = 2;
			orientationOffset = 6;
			break;
		case Monster.MonsterOrientation.NorthEast:
			//x = 1;
			//z = 1;
			//orientationOffset = 4;
			orientationOffset = 4;
			break;
		case Monster.MonsterOrientation.South:
			//x = 0;
			//z = -1;
			//orientationOffset = 5;
			orientationOffset = 5;
			break;
		case Monster.MonsterOrientation.SouthWest:
			//x = -1;
			//z = -1;
			//orientationOffset = 7;
			orientationOffset = 0;
			break;
		case Monster.MonsterOrientation.SouthEast:
			//x = 1;
			//z = -1;
			//orientationOffset = 1;
			orientationOffset = 2;
			break;
		case Monster.MonsterOrientation.West:
			//x = -1;
			//z = 0;
			//orientationOffset = 2;
			orientationOffset = 3;
			break;
		case Monster.MonsterOrientation.East:
			//x = 1;
			//z = 0;
			//orientationOffset = 6;
			orientationOffset = 7;
			break;
		default:
			break;
		}

		switch (notation) {
		case Movement.MOVEMENT_NOTATION.H:
			break;
		case Movement.MOVEMENT_NOTATION.F:
			targetCoordinateAdjustment[0] = orientationIndex[cycle(orientationOffset,0),0];
			targetCoordinateAdjustment[1] = orientationIndex[cycle(orientationOffset,0),1];
			break;
		case Movement.MOVEMENT_NOTATION.B:
			targetCoordinateAdjustment[0] = orientationIndex[cycle(orientationOffset,4),0];
			targetCoordinateAdjustment[1] = orientationIndex[cycle(orientationOffset,4),1];
			break;
		case Movement.MOVEMENT_NOTATION.FP:
			targetCoordinateAdjustment[0] = orientationIndex[cycle(orientationOffset,5),0];
			targetCoordinateAdjustment[1] = orientationIndex[cycle(orientationOffset,5),1];
			break;
		case Movement.MOVEMENT_NOTATION.FS:
			targetCoordinateAdjustment[0] = orientationIndex[cycle(orientationOffset,3),0];
			targetCoordinateAdjustment[1] = orientationIndex[cycle(orientationOffset,3),1];
			break;
		case Movement.MOVEMENT_NOTATION.BP:
			targetCoordinateAdjustment[0] = orientationIndex[cycle(orientationOffset,7),0];
			targetCoordinateAdjustment[1] = orientationIndex[cycle(orientationOffset,7),1];
			break;
		case Movement.MOVEMENT_NOTATION.BS:
			targetCoordinateAdjustment[0] = orientationIndex[cycle(orientationOffset,1),0];
			targetCoordinateAdjustment[1] = orientationIndex[cycle(orientationOffset,1),1];
			break;
		case Movement.MOVEMENT_NOTATION.P:
			targetCoordinateAdjustment[0] = orientationIndex[cycle(orientationOffset,2),0];
			targetCoordinateAdjustment[1] = orientationIndex[cycle(orientationOffset,2),1];
			break;
		case Movement.MOVEMENT_NOTATION.S:
			targetCoordinateAdjustment[0] = orientationIndex[cycle(orientationOffset,6),0];
			targetCoordinateAdjustment[1] = orientationIndex[cycle(orientationOffset,6),1];
			break;
		}
		Debug.Log(targetCoordinateAdjustment[0] + ", " + targetCoordinateAdjustment[1]);

		return null;
	}

	//A square has a board coordinate. We can find it by name, but for orientation purposes, some adjustments may be needed to offset the expected space
	//This determines the objective index declared at the top.
	private string parseBoardIndex(string notation, bool boardInit) {

		//Home index initialization
		if (boardInit) {
			for (int i = 0; i < boardLetters.Length; i++) {
				if (notation[0].ToString ().Equals (boardLetters [i])) {
					homeLetterIndex = i;
					break;
				}
			}
			for (int i = 0; i < boardNumbers.Length; i++) {
				if (notation[1].ToString ().Equals (boardNumbers [i].ToString ())) {
					homeNumberIndex = i;
					break;
				}
			}
		} else {

		}
		return null;
	}

	private int cycle(int startingPos, int cyclePos) {
		int tempPos = startingPos + cyclePos;
		if (tempPos > 7) {
			return tempPos - 8;
		} else {
			return tempPos;
		}
	}
}
