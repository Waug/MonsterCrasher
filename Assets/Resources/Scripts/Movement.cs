using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

	public bool NoMovement;
	public bool Forward;
	public static MOVEMENT_NOTATION[] mv_Forward 				= 	{MOVEMENT_NOTATION.F};
	public static MOVEMENT_NOTATION[] mv_NoMovement 			=	{MOVEMENT_NOTATION.H};


	//Custom property drawer will help simplify movement options for monsters, look it up TODO
	public enum MOVEMENTS_AVAILABLE
	{
		NO_MOVEMENT,
		FORWARD
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

	public static MOVEMENT_NOTATION[] findMove(MOVEMENTS_AVAILABLE direction) {
		switch (direction) {
		case MOVEMENTS_AVAILABLE.NO_MOVEMENT:
			return mv_NoMovement;
		case MOVEMENTS_AVAILABLE.FORWARD:
			return mv_Forward;
		default:
			return mv_NoMovement;
		}
		return null;
	}

	public GameObject findCustomMove() {
		return null;
	}
}
