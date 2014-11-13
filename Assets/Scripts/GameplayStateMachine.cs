using UnityEngine;
using System.Collections;
using System;

namespace GSP
{
	public class StateMachine : MonoBehaviour
	{
		//Contains overall states of program
		enum OVERALLSTATES {INTRO, MENU, GAME, END};
		//Contains states of menu
		enum MENUSTATES {HOME, SOLO, MULTI, CREDITS, OPTIONS, QUIT};
		//Contains states of gameplay
		enum GAMESTATES {HOME, INITIALIZE, DETERMINE, ROLLDICE, CALCDISTANCE, PRESENT, CONFIRM, MAPEVENT, 
			ENDTURN, ENDGAME};

		//State machine variables
		GUI GuiStateMachine;			//GUI
		OVERALLSTATES m_programState;	//Current overall state
		MENUSTATES m_menuState;			//Current menu state
		GAMESTATES m_gameplayState;		//Current state of gameplay

		//Game state machine variables
		int m_dieRoll;					//Value of roll. A zero means no roll was made.
		int m_allowedTravelDist;		//Distance player can move after algorithm
		int m_currTravelDist;	 		//Holds number of spaces moved this turn (may be unnecessary variable)
		Vector2 m_highlightPosition; 	//Highlight will indicate chosen tile

		//Initialize variables
		void Start()
		{
			m_programState = OVERALLSTATES.INTRO;	//Initial beginning of game
			m_menuState = MENUSTATES.HOME;			//Prevents triggers from occuring before called
			m_gameplayState = GAMESTATES.HOME;      //Prevents triggers from occuring before called
			m_dieRoll = 0;							//Start at no roll
			m_allowedTravelDist = 0;				//Start at no travel
			m_currTravelDist = 0;					//Start at no movement
			m_highlightPosition = new Vector2 ();	//Initialize vector
		} //end Start

		//Main function for controlling game
		void Update()
		{
			//Will fill in later with game controls. Refer to UMLs for other details.
			//PROGRAM ENTRY POINT
			switch(m_programState)
			{
			//INTRO
			case OVERALLSTATES.INTRO:
				//Play video or whatever
				break;
			//MENU
			case OVERALLSTATES.MENU:
				//MENU ENTRY POINT
				switch(m_menuState)
				{
				//HOME - Menu hub, displays all buttons for menu
				case MENUSTATES.HOME:
					break;
				//SOLO - Single Player game
				case MENUSTATES.SOLO:
					break;
				//MULTI - Multiplayer game
				case MENUSTATES.MULTI:
					break;
				//CREDITS - Display names and instructions
				case MENUSTATES.CREDITS:
					break;
				//OPTIONS - Display and allow change to any options we want
				case MENUSTATES.OPTIONS:
					break;
				//QUIT - Change program to END to wrap up any loose ends
				case MENUSTATES.QUIT:
					break;
				} //end Menu Switch
				break;
			//GAME
			case OVERALLSTATES.GAME:
				//GAMEPLAY ENTRY POINT
				switch(m_gameplayState)
				{
				//HOME - Prevents game from starting prematurely
				case GAMESTATES.HOME:
					break;
				//INITIALIZE - Starts game variables with corresponding choices
				case GAMESTATES.INITIALIZE:
					break;
				//DETERMINE - Determines which player is going, this is the start of the turn
				case GAMESTATES.DETERMINE:
					break;
				//***********
				//NOTE: ROLL, CALC, and ENDTURN can be used to implement effects that might be added later
				//***********
				//ROLLDICE - Rolls dice
				case GAMESTATES.ROLLDICE:
					break;
				//CALCDISTANCE - Plugs roll into algorithm and returns allowed movement
				case GAMESTATES.CALCDISTANCE:
					break;
				//PRESENT - Presents the tiles the player can choose to move to
				case GAMESTATES.PRESENT:
					break;
				//CONFIRM - Player confirms if they like their choice
				case GAMESTATES.CONFIRM:
					break;
				//MAPEVENT - Call the appropriate map event
				case GAMESTATES.MAPEVENT:
					break;
				//ENDTURN - This is the end of the player's turn
				case GAMESTATES.ENDTURN:
					break;
				//ENDGAME - End of game, display results, return to menu when done
				case GAMESTATES.ENDGAME:
					break;
				} //end Gameplay Switch
				break;
			//END
			case OVERALLSTATES.END:
				//Wrap up any loose ends here since the program is now exiting.
				break;
			default:
				print ("Error - No program state " + m_programState + " found.");
				break;
			} //end Program Switch

			//Update and draw the GUI
			DrawGUIText ();
		} //end Update

		//State machine functions
		//Program state
		public int GetState()
		{
			return (int)m_programState;
		} //end GetState()

		//Menu state
		public int GetMenu()
		{
			return (int)m_menuState;
		} //end GetMenu()

		//Game state
		public int GetGame()
		{
			return (int)m_gameplayState;
		} //end GetGame()

		//GUI Text function
		public void DrawGUIText()
		{
			//Sample of collecting overall state. Can be changed for Menu and Game states
			var state = GameObject.FindGameObjectWithTag("GUITextTag").GetComponent<GUIText>();
			state.text = Enum.GetName (typeof(OVERALLSTATES), (int)m_programState);
		} //end DrawGUIText

		//GUI drawing
		public void OnDrawGizmos()
		{

		} //end OnDrawGizmos()
	} //end StateMachine class
} //end namespace GSP
	
	///Kept for historical purposes
	/*public class GameplayStateMachine : MonoBehaviour 
	//====================================================================
	// +will be in charge of designing the stateMachine
	// +GetState() returns int
	// +public void NextState() will move state machine to the next state
	//
	//====================================================================
	{
		public enum GamePlayState
		//--------------------------------------------
		// +enum will hold the values of gameplay states
		//--------------------------------------------
		{
			ROLLDICE,
			CALCDISTANCE,
			DISPLAYDISTANCE,
			SELECTPATHTOTAKE,
			DOACTION,
			ENDTURN
		} //end public enum GamePlayState
		private GUI GuiStateMachine;

		public GamePlayState m_gamePlayState = GamePlayState.ROLLDICE;	//beginning state
		public int m_diceRoll = 0; 		//if diceRoll equals zero, player has not rolled dice
		public int m_allowedTravelDist = 0;	//calculated using dice roll
		//public int m_currTravelDist = 0; 	//when this equals or is greater than allowed traveled distance, turn ends.
		public Vector2 m_startingHighlightPosition; //high light will begin in players position
		public bool m_stageTransition = false;
		public bool m_movementConfirm = false;	//this will allow the player to cancel their movement
		public bool m_doingAction = false;	//when player hits a map event, this becomes true
		public bool m_endTurn = false; 		//when true, will activate a timer and display to the player that turn is ending

		void Update()
		{
			StateMachine();
		}

		private void StateMachine()
		//----------------------------------------------------
		// +StateMachine in charge of displaying GUI that describes
		//	 what stage the gamePlay is in
		// +Uses a switch() statement for handling states
		//----------------------------------------------------
		{
			var state = GameObject.FindGameObjectWithTag("GUITextTag").GetComponent<GUIText>();
			switch (m_gamePlayState) 
			{
				case GamePlayState.ROLLDICE:
					
					state.text = "roll dice";
					//create a roll dice button
					//if button is clicked, destroy button
					//nextState()
					break;
				case GamePlayState.CALCDISTANCE:
			
					state.text = "Calculate Distance";
					//get dice value
					//calculate m_allowedTravelDistance
					//nextState()
					break;
				case GamePlayState.DISPLAYDISTANCE:
					//starting from playersNode positions, highlight
					// tiles that can be traveled on
					//nextState()
					break;
				case GamePlayState.SELECTPATHTOTAKE:
					//player object can request for m_allowedTravelDistance
						//...this code should be created in player object...
						//if GamePlayState.SELECTPATHTOTAKE
							//node selection
							//pathfind to node selection
						//if playerNodePos == targetNodePos
							//endTurn()
						//if Mapevent occurs
							//NextState()
					break;
				case GamePlayState.DOACTION:
					//map events
					//if map event is complete
						//NextState()
					break;
				case GamePlayState.ENDTURN:
					break;

				default:
					//stage transition
					break;
			
			} //end switch (m_gamePlayState)
		
		} //end private void StateMachine()

		public int GetState()
		{
			int state;
			state = (int)m_gamePlayState;
			return state;
		}

		public void NextState()
		//-----------------------------------
		// changes to the next state
		//-----------------------------------
		{
			m_gamePlayState = m_gamePlayState +1;
		} //end public void NextStage()

		public void EndTurn()
		//-----------------------------------
		// automatically changes to endTurn state
		//-----------------------------------
		{
				
		} //end public void ENDTURN()

		public void OnDrawGizmos()
		//-----------------------------------
		//to draw on the scene.
		//-----------------------------------
		{

		} //end public void OnDrawGizmos()

	}	//end public class GameplayStateMachine : MonoBehaviour */
