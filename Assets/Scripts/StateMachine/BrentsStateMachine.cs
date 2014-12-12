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
		GameObject introBackground;			//Intro Background
		GameObject menuBackground;			//Menu Background
		GameObject soloButton;				//Solo play button
		GameObject multiButton;				//Multiplayer button
		GameObject creditsButton;			//Credits button
		GameObject optionsButton;			//Options button
		GameObject quitButton;				//Quit button
		GameObject backButton;				//Back button
		public string m_ButtonState;		//Holds what state the button is in

		#region Menu Data Declaration Stuff

		// Holds the reference to the game object.
		GameObject m_menuData;

		// Holds the reference to the menu data's script.
		MenuData m_menuDataScript;

		#endregion
		
		//Initialize variables
		void Start()
		{
			m_programState = OVERALLSTATES.INTRO;		//Initial beginning of game
			m_menuState = MENUSTATES.HOME;				//Prevents triggers from occuring before called
			timeHolder = Time.time + 3.0f;				//Initialize first wait period

			#region Menu Data Initialisation Stuff
			
			// Create the empty game object.
			m_menuData = new GameObject( "MenuData" );
			
			// Tag it as menu data.
			m_menuData.tag = "MenuDataTag";
			
			// Add the menu data component.
			m_menuData.AddComponent<MenuData>();
			
			// Set it to not destroy on load.
			DontDestroyOnLoad( m_menuData );

			// Get the menu data object's script.
			m_menuDataScript = m_menuData.GetComponent<MenuData>();
			
			#endregion

			//Create Intro Background
			introBackground = new GameObject("IntroBackground");
			introBackground.tag = "IntroBackground";
			var spriteRender = introBackground.AddComponent<SpriteRenderer>();
			spriteRender.sprite = SpriteReference.spriteIntroBackground;
			SpriteReference.ResizeSpriteToScreen(introBackground, Camera.main, 1, 1);
		} //end Start
		
		//Main function for controlling game
		void Update()
		{
			//Will fill in later with game controls. Refer to UMLs for other details.
			//PROGRAM ENTRY POINT
			var state = GameObject.FindGameObjectWithTag ("GUITextTag").GetComponent<GUIText> ();
			switch(m_programState)
			{
				//INTRO
			case OVERALLSTATES.INTRO:
				//Create background
				print ("The intro is currently playing.");
				state.text = "Welcome To The Traveler!";
				//After intro finishes, move to menu
				if(Time.time > timeHolder)
				{
					//Change state
					m_programState = OVERALLSTATES.MENU;

					//Destroy old background and text
					Destroy (GameObject.FindGameObjectWithTag("IntroBackground"));
					state.text = "";

					//Create new background
					menuBackground = new GameObject("MenuBackground");
					menuBackground.tag = "MenuBackground";
					var spriteRender = menuBackground.AddComponent<SpriteRenderer>();
					spriteRender.sprite = SpriteReference.spriteMenuBackground;
					SpriteReference.ResizeSpriteToScreen(menuBackground, Camera.main, 1, 1);

					//Create menu buttons
					CreateButtons();
				} //end wait if
				break;
				//MENU
			case OVERALLSTATES.MENU:
				//MENU ENTRY POINT
				state.transform.position = new Vector3(0.5f, 0.5f, 0.0f);
				switch(m_menuState)
				{
				case MENUSTATES.HOME:
					//HOME - Menu hub, displays all buttons for menu

					if(Input.GetKeyDown(KeyCode.S))
					{
						DestroyButtons();
						print ("Solo mode chosen.");
						state.text = "Solo mode chosen, please wait.";
						timeHolder = Time.time + 1.5f;
						m_menuState = MENUSTATES.SOLO;
					} //end Solo chosen if
					else if(Input.GetKeyDown(KeyCode.M))
					{
						DestroyButtons();
						print ("Multiplayer mode chosen.");
						state.text = "Multiplayer mode chosen, please wait.";
						timeHolder = Time.time + 1.5f;
						m_menuState = MENUSTATES.MULTI;
					} //end Multiplayer chosen else if
					else if(Input.GetKeyDown(KeyCode.C))
					{
						DestroyButtons();
						print ("Credits chosen.");
						state.text = "Loading credits, please wait.";
						timeHolder = Time.time + 1.5f;
						m_menuState = MENUSTATES.CREDITS;
					} //end Credits chosen else if
					else if(Input.GetKeyDown(KeyCode.O))
					{
						DestroyButtons();
						print ("Options chosen.");
						state.text = "Loading options, please wait.";
						timeHolder = Time.time + 1.5f;
						m_menuState = MENUSTATES.OPTIONS;
					} //end Options chosen else if
					else if(Input.GetKeyDown(KeyCode.Q))
					{
						DestroyButtons();
						print ("Quit chosen.");
						state.text = "Closing game, please wait.";
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
						state.text = "Setting up solo mode, one moment.";
						
						#region Menu Data Adding Stuff
						
						// Set the number of players to one for solo mode.
						m_menuDataScript.NumberPlayers = 1;
						
						#endregion
						
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
						state.text = "Setting up multiplayer mode, one moment.";
						
						#region Menu Data Adding Stuff
						
						// Get the number of players some how.
						
						// Until then, set the number of players to two for multi-mode.
						m_menuDataScript.NumberPlayers = 2;
						
						#endregion
						
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
						state.text = "The Traveler\n\nCreated by:\nBrent Spector - Lead Game Designer\n" +
							"Damien Robbs - Lead Programmer\nJavier Mendoza - Lead UI\n" +
							"Jacky Yuen - Lead Graphics and Audio.\n\nHit Enter/Return to go back to the menu.";
						//Return back to menu home when done
						if(Input.GetKeyDown(KeyCode.Return))
						{
							m_menuState = MENUSTATES.HOME;
							CreateButtons();
							state.text = "";
						} //end Back chosen if
					} //end wait if
					break;
					//OPTIONS - Display and allow change to any options we want
				case MENUSTATES.OPTIONS:
					//Display options buttons
					if(Time.time > timeHolder)
					{
						print ("This is currently displaying options. Hit enter to return to menu.");
						state.text = "There are currently no options. Hit Enter/Return to go back" +
							" to the menu.";
						//Return back to menu home when done
						if(Input.GetKeyDown(KeyCode.Return))
						{
							m_menuState = MENUSTATES.HOME;
							CreateButtons();
							state.text = "";
						} //end Back chosen if
					} //end wait if
					break;
					//QUIT - Change program to END to wrap up any loose ends
				case MENUSTATES.QUIT:
					//Clear up anything before game ends
					if(Time.time > timeHolder)
					{
						print("Clearing up game files.");
						state.text = "Cleaning up game files, one moment.";
						//Move to end of program
						m_programState = OVERALLSTATES.END;
					} //end wait if
					break;
				} //end Menu Switch
				break;
				//GAME
			case OVERALLSTATES.GAME:
				//GAMEPLAY ENTRY POINT
				Destroy (GameObject.FindGameObjectWithTag("MenuBackground"));
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
					state.text = "Cleaning remaining program files, thank you for playing!";
					m_menuState = MENUSTATES.HOME;
					m_programState = OVERALLSTATES.INTRO;
					timeHolder = Time.time + 3.0f;
					Application.Quit();
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

		void CreateButtons()
		{
			//Destroy Backbutton
			if(GameObject.FindGameObjectWithTag("BackButton") != null)
			{
				Destroy (GameObject.FindGameObjectWithTag("BackButton"));
			} //end if

			#region CreateButtons
			//Solo Button
			soloButton = new GameObject("Solo Button");
			soloButton.tag = "SoloButton";
			var soloRender = soloButton.AddComponent<SpriteRenderer>();
			soloRender.sprite = SpriteReference.spriteStart;
			soloButton.AddComponent<BoxCollider2D>();
			soloButton.transform.localPosition = new Vector3(0.0f, 3.0f, 0.0f);
			soloRender.sortingOrder = 2;
			
			//Multi Button
			multiButton = new GameObject("Multi Button");
			multiButton.tag = "MultiButton";
			var multiRender = multiButton.AddComponent<SpriteRenderer>();
			multiRender.sprite = SpriteReference.spriteContinue;
			multiButton.AddComponent<BoxCollider2D>();
			multiButton.transform.localPosition = new Vector3(0.0f, 1.5f, 0.0f);
			multiRender.sortingOrder = 2;
			
			//Credits Button
			creditsButton = new GameObject("Credits Button");
			creditsButton.tag = "CreditsButton";
			var creditsRender = creditsButton.AddComponent<SpriteRenderer>();
			creditsRender.sprite = SpriteReference.spriteCredit;
			creditsButton.AddComponent<BoxCollider2D>();
			creditsButton.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
			creditsRender.sortingOrder = 2;
			
			//Options Button
			optionsButton = new GameObject("Options Button");
			optionsButton.tag = "OptionsButton";
			var optionsRender = optionsButton.AddComponent<SpriteRenderer>();
			optionsRender.sprite = SpriteReference.spriteOptions;
			optionsButton.AddComponent<BoxCollider2D>();
			optionsButton.transform.localPosition = new Vector3(0.0f, -1.5f, 0.0f);
			optionsRender.sortingOrder = 2;
			
			//Quit Button
			quitButton = new GameObject("Quit Button");
			quitButton.tag = "QuitButton";
			var quitRender = quitButton.AddComponent<SpriteRenderer>();
			quitRender.sprite = SpriteReference.spriteExit;
			quitButton.AddComponent<BoxCollider2D>();
			quitButton.transform.localPosition = new Vector3(0.0f, -3.0f, 0.0f);
			quitRender.sortingOrder = 2;
			#endregion
		} //end CreateButtons()

		void DestroyButtons()
		{
			//Destroy menu buttons
			Destroy (GameObject.FindGameObjectWithTag("SoloButton"));
			Destroy (GameObject.FindGameObjectWithTag("MultiButton"));
			Destroy (GameObject.FindGameObjectWithTag("CreditsButton"));
			Destroy (GameObject.FindGameObjectWithTag("OptionsButton"));
			Destroy (GameObject.FindGameObjectWithTag("QuitButton"));

			//Create Backbutton
			//Back Button
			backButton = new GameObject("Back Button");
			backButton.tag = "BackButton";
			var backRender = backButton.AddComponent<SpriteRenderer>();
			backRender.sprite = SpriteReference.spriteExit;
			backButton.AddComponent<BoxCollider2D>();
			backButton.transform.localPosition = new Vector3(0.0f, -3.0f, 0.0f);
			backRender.sortingOrder = 2;
		} //end DestroyButtons()
	} //end StateMachine class
} //end namespace GSP
