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
		float timeHolder;				//Holds waiting time
		
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
			timeHolder = Time.time + 3.0f;			//Initialize first wait period
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
				//Play video or whatever. Wait for seconds is placeholder
				print ("The intro is currently playing.");
				//After intro finishes, move to menu
				if(Time.time > timeHolder)
				{
					m_programState = OVERALLSTATES.MENU;
				} //end wait if
				break;
			//MENU
			case OVERALLSTATES.MENU:
				//MENU ENTRY POINT
				switch(m_menuState)
				{
				//HOME - Menu hub, displays all buttons for menu
				case MENUSTATES.HOME:
					//Button code goes here
					//Placeholder for testing
					print ("S for Solo, M for Multi, C for Credits, O for Options, Q for Quit");
					if(Input.GetKeyDown(KeyCode.S))
					{
						print ("Solo mode chosen.");
						timeHolder = Time.time + 1.5f;
						m_menuState = MENUSTATES.SOLO;
					} //end Solo chosen if
					else if(Input.GetKeyDown(KeyCode.M))
					{
						print ("Multiplayer mode chosen.");
						timeHolder = Time.time + 1.5f;
						m_menuState = MENUSTATES.MULTI;
					} //end Multiplayer chosen else if
					else if(Input.GetKeyDown(KeyCode.C))
					{
						print ("Credits chosen.");
						timeHolder = Time.time + 1.5f;
						m_menuState = MENUSTATES.CREDITS;
					} //end Credits chosen else if
					else if(Input.GetKeyDown(KeyCode.O))
					{
						print ("Options chosen.");
						timeHolder = Time.time + 1.5f;
						m_menuState = MENUSTATES.OPTIONS;
					} //end Options chosen else if
					else if(Input.GetKeyDown(KeyCode.Q))
					{
						print ("Quit chosen.");
						timeHolder = Time.time + 1.5f;
						m_menuState = MENUSTATES.QUIT;
					} //end Quit chosen else if
					break;
				//SOLO - Single Player game
				case MENUSTATES.SOLO:
					//Apply any choices for Single Player here
					if(Time.time > timeHolder)
					{
						print ("Setting up solo mode.");
						//Transition into game state
						m_programState = OVERALLSTATES.GAME;
						m_gameplayState = GAMESTATES.INITIALIZE;
						timeHolder = Time.time + 1.5f;
					} //end wait if
					break;
				//MULTI - Multiplayer game
				case MENUSTATES.MULTI:
					//Apply any choices for Multiplayer here
					if(Time.time > timeHolder)
					{
						print ("Setting up multiplayer mode.");
						//Transition into game state
						m_programState = OVERALLSTATES.GAME;
						m_gameplayState = GAMESTATES.INITIALIZE;
						timeHolder = Time.time + 1.5f;
					} //end wait if
					break;
				//CREDITS - Display names and instructions
				case MENUSTATES.CREDITS:
					//Show credits
					if(Time.time > timeHolder)
					{
						print ("This is currently displaying credits. Hit enter to return to menu.");
						//Return back to menu home when done
						if(Input.GetKeyDown(KeyCode.Return))
						{
							m_menuState = MENUSTATES.HOME;
						} //end Back chosen if
					} //end wait if
					break;
				//OPTIONS - Display and allow change to any options we want
				case MENUSTATES.OPTIONS:
					//Display options buttons
					if(Time.time > timeHolder)
					{
						print ("This is currently displaying options. Hit enter to return to menu.");
						//Return back to menu home when done
						if(Input.GetKeyDown(KeyCode.Return))
						{
							m_menuState = MENUSTATES.HOME;
						} //end Back chosen if
					} //end wait if
					break;
				//QUIT - Change program to END to wrap up any loose ends
				case MENUSTATES.QUIT:
					//Clear up anything before game ends
					if(Time.time > timeHolder)
					{
						print("Clearing up game files.");
						//Move to end of program
						m_programState = OVERALLSTATES.END;
					} //end wait if
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
					print ("You should not be seeing this message. This is only for error prevention.");
					break;
				//INITIALIZE - Starts game variables with corresponding choices
				case GAMESTATES.INITIALIZE:
					//Initialize any game variables here
					if(Time.time > timeHolder)
					{
						print ("Initializing game variables.");
						m_gameplayState = GAMESTATES.DETERMINE;
						timeHolder = Time.time + 1.5f;
					} //end wait if
					break;
				//DETERMINE - Determines which player is going, this is the start of the turn
				case GAMESTATES.DETERMINE:
					if(Time.time > timeHolder)
					{
						print ("Determining player for turn.");
						m_gameplayState = GAMESTATES.ROLLDICE;
						timeHolder = Time.time + 1.5f;
					} //end wait if
					break;
				//***********
				//NOTE: ROLL, CALC, and ENDTURN can be used to implement effects that might be added later
				//***********
				//ROLLDICE - Rolls dice
				case GAMESTATES.ROLLDICE:
					if(Time.time > timeHolder)
					{
						print ("Rolling dice for allowed movement.");
						m_gameplayState = GAMESTATES.CALCDISTANCE;
						timeHolder = Time.time + 1.5f;
					} //end wait if
					break;
				//CALCDISTANCE - Plugs roll into algorithm and returns allowed movement
				case GAMESTATES.CALCDISTANCE:
					if(Time.time > timeHolder)
					{
						print ("Calculating allowed movement based on roll.");
						m_gameplayState = GAMESTATES.PRESENT;
						timeHolder = Time.time + 1.5f;
					} //end wait if
					break;
				//PRESENT - Presents the tiles the player can choose to move to
				case GAMESTATES.PRESENT:
					if(Time.time > timeHolder)
					{
						print ("Presenting tiles player can move to.");
						m_gameplayState = GAMESTATES.CONFIRM;
						timeHolder = Time.time + 1.5f;
					} //end wait if
					break;
				//CONFIRM - Player confirms if they like their choice
				case GAMESTATES.CONFIRM:
					if(Time.time > timeHolder)
					{
						print ("Confirming player selection.");
						m_gameplayState = GAMESTATES.MAPEVENT;
						timeHolder = Time.time + 1.5f;
					} //end wait if
					break;
				//MAPEVENT - Call the appropriate map event
				case GAMESTATES.MAPEVENT:
					if(Time.time > timeHolder)
					{
						print ("Determining map event.");
						m_gameplayState = GAMESTATES.ENDTURN;
						timeHolder = Time.time + 1.5f;
					} //end wait if
					break;
				//ENDTURN - This is the end of the player's turn
				case GAMESTATES.ENDTURN:

					if(Time.time > timeHolder)
					{
						print ("Ending player's turn. Hit E to end game, or C to start next turn.");
					} //end wait if

					//Game is over
					if(Input.GetKeyDown(KeyCode.E))
					{
						m_gameplayState = GAMESTATES.ENDGAME;
						timeHolder = Time.time + 1.5f;
					} //end Game Over if
					//Loop back to start
					else if(Input.GetKeyDown(KeyCode.C))
					{
						m_gameplayState = GAMESTATES.DETERMINE;
						timeHolder = Time.time + 1.5f;
					} //end Next Turn else
					break;
				//ENDGAME - End of game, display results, return to menu when done
				case GAMESTATES.ENDGAME:
					if(Time.time > timeHolder)
					{
						print ("Game is now over. Displaying results. Then returning to menu.");
						m_gameplayState = GAMESTATES.HOME;
						m_menuState = MENUSTATES.HOME;
						m_programState = OVERALLSTATES.MENU;
						timeHolder = Time.time + 1.5f;
					} //end wait if
					break;
				} //end Gameplay Switch
				break;
			//END
			case OVERALLSTATES.END:
				//Wrap up any loose ends here since the program is now exiting.
				if(Time.time > timeHolder)
				{
					print ("Program is now ending. Clearing remaining files.");
					m_gameplayState = GAMESTATES.HOME;
					m_menuState = MENUSTATES.HOME;
					m_programState = OVERALLSTATES.INTRO;
					timeHolder = Time.time + 3.0f;
				} //end wait if
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
			if(m_gameplayState == GAMESTATES.HOME)
			{
				var state = GameObject.FindGameObjectWithTag("GUITextTag").GetComponent<GUIText>();
				state.text = Enum.GetName (typeof(OVERALLSTATES), (int)m_programState);

				if(m_programState == OVERALLSTATES.MENU)
				{
					state.text += " - " + Enum.GetName (typeof(MENUSTATES), (int)m_menuState);
				} //end draw menu state if
			} //end draw program state
			else
			{
				var state = GameObject.FindGameObjectWithTag("GUITextTag").GetComponent<GUIText>();
				state.text = Enum.GetName (typeof(GAMESTATES), (int)m_gameplayState);
			} //end draw game state
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
