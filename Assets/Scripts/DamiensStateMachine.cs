using UnityEngine;
using System.Collections;
using System;

namespace GSP.Damien
{
	public class StateMachine : MonoBehaviour
	{
		//Contains overall states of program
		enum OVERALLSTATES {INTRO, MENU, GAME, END};
		//Contains states of menu
		enum MENUSTATES {HOME, SOLO, MULTI, CREDITS, OPTIONS, QUIT};
		
		//State machine variables
		GUI GuiStateMachine;				//GUI
		OVERALLSTATES m_programState;		//Current overall state
		MENUSTATES m_menuState;				//Current menu state
		float timeHolder;					//Holds waiting time
		
		//Game state machine variables
		GameplayStateMachine m_gameMachine;	//Gamecontroller object
		int m_dieRoll;						//Value of roll. A zero means no roll was made.
		int m_allowedTravelDist;			//Distance player can move after algorithm
		int m_currTravelDist;	 			//Holds number of spaces moved this turn (may be unnecessary variable)
		Vector2 m_highlightPosition; 		//Highlight will indicate chosen tile
		
		//Initialize variables
		void Start()
		{
			m_programState = OVERALLSTATES.INTRO;		//Initial beginning of game
			m_menuState = MENUSTATES.HOME;				//Prevents triggers from occuring before called
			m_dieRoll = 0;								//Start at no roll
			m_allowedTravelDist = 0;					//Start at no travel
			m_currTravelDist = 0;						//Start at no movement
			m_highlightPosition = new Vector2 ();		//Initialize vector
			timeHolder = Time.time + 3.0f;				//Initialize first wait period
			m_gameMachine = new GameplayStateMachine(); //Initialize game controller
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
				break;
				//END
			case OVERALLSTATES.END:
				//Wrap up any loose ends here since the program is now exiting.
				if(Time.time > timeHolder)
				{
					print ("Program is now ending. Clearing remaining files.");
					m_menuState = MENUSTATES.HOME;
					m_programState = OVERALLSTATES.INTRO;
					timeHolder = Time.time + 3.0f;
				} //end wait if
				break;
			default:
				print ("Error - No program state " + m_programState + " found.");
				break;
			} //end Program Switch
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
		
		//GUI drawing
		public void OnDrawGizmos()
		{
			
		} //end OnDrawGizmos()
	} //end StateMachine class
} //end namespace GSP

