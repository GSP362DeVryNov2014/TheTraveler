using UnityEngine;
using System.Collections;
using System;

namespace GSP
{
	public class BrentsStateMachine : MonoBehaviour
	{
		//Contains overall states of program
		enum OVERALLSTATES {INTRO, MENU, GAME, END};
		//Contains states of menu
		enum MENUSTATES {HOME, SOLO, MULTI, CREDITS, OPTIONS, QUIT};
		
		//State machine variables
		OVERALLSTATES m_programState;		//Current overall state
		MENUSTATES m_menuState;				//Current menu state
		float timeHolder;					//Holds waiting time
		
		//Initialize variables
		void Start()
		{
			m_programState = OVERALLSTATES.INTRO;		//Initial beginning of game
			m_menuState = MENUSTATES.HOME;				//Prevents triggers from occuring before called
			timeHolder = Time.time + 3.0f;				//Initialize first wait period
		} //end Start
		
		//Main function for controlling game
		void Update()
		{
			//Will fill in later with game controls. Refer to UMLs for other details.
			//PROGRAM ENTRY POINT
			var state = GameObject.FindGameObjectWithTag("GUITextTag").GetComponent<GUIText>(); //GUI TEMP
			switch(m_programState)
			{
				//INTRO
			case OVERALLSTATES.INTRO:
				//Play video or whatever. Wait for seconds is placeholder
				print ("The intro is currently playing.");
				state.text = "Intro";
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
					state.text = "Menu - Home";
					if(Input.GetKeyDown(KeyCode.S))
					{
						print ("Solo mode chosen.");
						state.text = "Menu - Solo";
						timeHolder = Time.time + 1.5f;
						m_menuState = MENUSTATES.SOLO;
					} //end Solo chosen if
					else if(Input.GetKeyDown(KeyCode.M))
					{
						print ("Multiplayer mode chosen.");
						state.text = "Menu - Multi";
						timeHolder = Time.time + 1.5f;
						m_menuState = MENUSTATES.MULTI;
					} //end Multiplayer chosen else if
					else if(Input.GetKeyDown(KeyCode.C))
					{
						print ("Credits chosen.");
						state.text = "Menu - Credits";
						timeHolder = Time.time + 1.5f;
						m_menuState = MENUSTATES.CREDITS;
					} //end Credits chosen else if
					else if(Input.GetKeyDown(KeyCode.O))
					{
						print ("Options chosen.");
						state.text = "Menu - Options";
						timeHolder = Time.time + 1.5f;
						m_menuState = MENUSTATES.OPTIONS;
					} //end Options chosen else if
					else if(Input.GetKeyDown(KeyCode.Q))
					{
						print ("Quit chosen.");
						state.text = "Menu - Quit";
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
						state.text = "Solo";
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
						state.text = "Multi";
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
						state.text = "Credits";
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
						state.text = "Options";
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
						state.text = "Quit";
						//Move to end of program
						m_programState = OVERALLSTATES.END;
					} //end wait if
					break;
				} //end Menu Switch
				break;
				//GAME
			case OVERALLSTATES.GAME:
				//GAMEPLAY ENTRY POINT
				Application.LoadLevel("area01");
				m_programState = OVERALLSTATES.MENU;
				m_menuState = MENUSTATES.HOME;
				print ("Program state : " + Enum.GetName(typeof(OVERALLSTATES), m_programState) + 
				       " Menu State : " + Enum.GetName(typeof(MENUSTATES), m_menuState));
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
	} //end StateMachine class
} //end namespace GSP
