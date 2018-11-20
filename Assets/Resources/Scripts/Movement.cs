using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

	public bool NoMovement;
	public bool Forward;

	//All moves must be simplified to a single movement notation if possible, e.g. F-S-S  ->  FS-S
	public static MOVEMENT_NOTATION[] mv_Forward 				= 	{MOVEMENT_NOTATION.F};	//F
	public static MOVEMENT_NOTATION[] mv_NoMovement 			=	{MOVEMENT_NOTATION.H};	//H
	public static MOVEMENT_NOTATION[] mv_KnightRight 			=	{MOVEMENT_NOTATION.FS, MOVEMENT_NOTATION.S}; 	//FSS
	public static MOVEMENT_NOTATION[] mv_KnightLeft 			=	{MOVEMENT_NOTATION.FS, MOVEMENT_NOTATION.P};	//FPP


	//Custom property drawer will help simplify movement options for monsters, look it up TODO
	//Currently need to add array, movement available, and switch case in findMoves() that's a bunch of dumb stuff
	public enum MOVEMENTS_AVAILABLE
	{
		NO_MOVEMENT,
		FORWARD,
		KNIGHTRIGHT,
		KNIGHTLEFT
	}

	private enum MOVEMENT_DEFINITIONS
	{
		H,			//NO_MOVEMENT
		F 			//FORWARD
	}

	public enum MOVEMENT_NOTATION
	{
		H,
		F,
		B,
		FP,
		FS,
		BP,
		BS,
		P,
		S,
		COMMA
	};

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private string[] BuildMove(){
		return null;
	}

	public static MOVEMENT_NOTATION[] findMoves(MOVEMENTS_AVAILABLE direction) {
		switch (direction) {
		case MOVEMENTS_AVAILABLE.NO_MOVEMENT:
			return mv_NoMovement;
		case MOVEMENTS_AVAILABLE.FORWARD:
			return mv_Forward;
		case MOVEMENTS_AVAILABLE.KNIGHTRIGHT:
			return mv_KnightRight;
		case MOVEMENTS_AVAILABLE.KNIGHTLEFT:
			return mv_KnightLeft;
		default:
			return mv_NoMovement;
		}
		return null;
	}

	public GameObject findCustomMove() {
		return null;
	}
}
