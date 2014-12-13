using UnityEngine;
using System.Collections;
using System;

namespace GSP
{
	public class BrentsStateMachine : MonoBehaviour
	{
		//Contains overall states of program
		public enum OVERALLSTATES {INTRO, MENU, GAME, END};
		//Contains states of menu
		public enum MENUSTATES {HOME, SOLO, MULTI, CREDITS, OPTIONS, QUIT};
		
		//State machine variables
		OVERALLSTATES m_programState;		//Current overall state
		MENUSTATES m_menuState;				//Current menu state
		float timeHolder;					//Holds waiting time

		//Backgrounds
		GameObject introBackground;			//Intro Background
		GameObject menuBackground;			//Menu Background

		//Buttons
		GameObject soloButton;				//Solo play button
		GameObject multiButton;				//Multiplayer button
		GameObject creditsButton;			//Credits button
		GameObject optionsButton;			//Options button
		GameObject quitButton;				//Quit button
		GameObject backButton;				//Back button
		bool menuCreated;					//Determines if menu buttons were created

		//Map and Player Selection
		GameObject desertMap;				//Desert Map
		GameObject snowMap;					//Snowy Map
		GameObject metroMap;				//Metropolis Map
		GameObject euroMap;					//European Map
		public string m_mapSelection;		//Holds scene name of map
		bool mapsCreated;					//Determines if selection maps were created
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
			menuCreated = false;						//Menu has not been created yet
			m_mapSelection = "nothing";					//Nothing has been chosen yet
			mapsCreated = false;						//Map selection has not been created yet

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
				//INTRO ENTRY POINT
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
				//Center text
				state.transform.position = new Vector3(0.5f, 0.5f, 0.0f);
				switch(m_menuState)
				{
				case MENUSTATES.HOME:
					//HOME - Menu hub, displays all buttons for menu
					if(!menuCreated)
					{
						CreateButtons();
						state.text = "";
					} //end if
					if(mapsCreated)
					{
						DestroyMaps();
					} //end if
					break;
					//SOLO - Single Player game
				case MENUSTATES.SOLO:
					//Clear buttons if not cleared yet
					if(menuCreated)
					{
						DestroyButtons();
					} //end if

					//Display maps for selection
					state.transform.position = new Vector3(0.5f, 0.4f, 0.0f);
					if(!mapsCreated)
					{
						CreateMaps();
						state.text = "Click which map you want to play.";
					} //end if

					//Only continue once a map has been chosen
					if(m_mapSelection != "nothing")
					{
						//Pick player amount
						#region Menu Data Adding Stuff

						// Set the number of players to one for solo mode.
						m_menuDataScript.NumberPlayers = 1;

						#endregion

						//Transition into game state
						m_programState = OVERALLSTATES.GAME;
					} //end if
					break;
					//MULTI - Multiplayer game
				case MENUSTATES.MULTI:
					//Clear buttons if not cleared yet
					if(menuCreated)
					{
						DestroyButtons();
					} //end if
					
					//Display maps for selection
					state.transform.position = new Vector3(0.5f, 0.4f, 0.0f);
					if(!mapsCreated)
					{
						CreateMaps();
						state.text = "Click which map you want to play.";
					} //end if
					
					//Only continue once a map has been chosen
					if(m_mapSelection != "nothing")
					{
						state.text = "Enter the number of players (1-4)";
						//Pick player amount
						#region Menu Data Adding Stuff

						//Set number of players
						if(Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
						{
							m_menuDataScript.NumberPlayers = 1;
							//Transition into game state
							m_programState = OVERALLSTATES.GAME;
						} //end if
						else if(Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
						{
							m_menuDataScript.NumberPlayers = 2;
							//Transition into game state
							m_programState = OVERALLSTATES.GAME;
						} //end if
						else if(Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
						{
							m_menuDataScript.NumberPlayers = 3;
							//Transition into game state
							m_programState = OVERALLSTATES.GAME;
						} //end if
						else if(Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
						{
							m_menuDataScript.NumberPlayers = 4;
							//Transition into game state
							m_programState = OVERALLSTATES.GAME;
						} //end if
						#endregion
					} //end if
					break;
					//CREDITS - Display names and instructions
				case MENUSTATES.CREDITS:
					//Clear buttons if not cleared yet
					if(menuCreated)
					{
						DestroyButtons();
					} //end if

					//Show credits
					state.text = "The Traveler\n\nCreated by:\nBrent Spector - Lead Game Designer\n" +
						"Damien Robbs - Lead Programmer\nJavier Mendoza - Lead UI\n" +
						"Jacky Yuen - Lead Graphics and Audio.\n\nPress Back to return to the menu.";
					//Return back to menu home when done
					break;
					//OPTIONS - Display and allow change to any options we want
				case MENUSTATES.OPTIONS:
					//Clear buttons if not cleared yet
					if(menuCreated)
					{
						DestroyButtons();
					} //end if

					//Display options buttons
					state.text = "There are currently no options. Press Back to return to the menu.";
					break;
					//QUIT - Change program to END to wrap up any loose ends
				case MENUSTATES.QUIT:
					//Clear buttons if not cleared yet
					if(menuCreated)
					{
						DestroyButtons();
					} //end if

					//Clear up anything before game ends
					state.text = "Cleaning up game files, one moment.";

					//Move to end of program
					m_programState = OVERALLSTATES.END;

					break;
				} //end Menu Switch
				break;
				//GAME
			case OVERALLSTATES.GAME:
				//GAMEPLAY ENTRY POINT
				//Destroy old background and back button
				Destroy (GameObject.FindGameObjectWithTag("MenuBackground"));
				Destroy (GameObject.FindGameObjectWithTag("BackButton"));

				//Load selected level
				Application.LoadLevel(m_mapSelection);

				//Reset states
				m_programState = OVERALLSTATES.MENU;
				m_menuState = MENUSTATES.HOME;

				break;
				//END
			case OVERALLSTATES.END:
				//END ENTRY POINT - WATCH OUT UNIVERSE!
				//Wrap up any loose ends here since the program is now exiting.
				state.text = "Cleaning remaining program files, thank you for playing!";

				//Delete exit button if it exists
				if(GameObject.FindGameObjectWithTag("BackButton") != null)
				{
					Destroy (GameObject.FindGameObjectWithTag("BackButton"));
				} //end if

				//Reset state machine to initial
				m_menuState = MENUSTATES.HOME;
				m_programState = OVERALLSTATES.INTRO;

				//Reset time holder to initial
				timeHolder = Time.time + 3.0f;

				//Exit program if possible
				Application.Quit();

				break;
				//DEFAULT - Catches any exceptions end prints errored state
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
			soloRender.sprite = SpriteReference.spriteSolo;
			soloButton.AddComponent<BoxCollider2D>();
			soloButton.AddComponent<SoloButtonCollision>();
			soloButton.transform.localPosition = new Vector3(0.0f, 3.0f, 0.0f);
			soloRender.sortingOrder = 2;
			
			//Multi Button
			multiButton = new GameObject("Multi Button");
			multiButton.tag = "MultiButton";
			var multiRender = multiButton.AddComponent<SpriteRenderer>();
			multiRender.sprite = SpriteReference.spriteMulti;
			multiButton.AddComponent<BoxCollider2D>();
			multiButton.AddComponent<MultiButtonCollision>();
			multiButton.transform.localPosition = new Vector3(0.0f, 1.5f, 0.0f);
			multiRender.sortingOrder = 2;
			
			//Credits Button
			creditsButton = new GameObject("Credits Button");
			creditsButton.tag = "CreditsButton";
			var creditsRender = creditsButton.AddComponent<SpriteRenderer>();
			creditsRender.sprite = SpriteReference.spriteCredit;
			creditsButton.AddComponent<BoxCollider2D>();
			creditsButton.AddComponent<CreditsButtonCollision>();
			creditsButton.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
			creditsRender.sortingOrder = 2;
			
			//Options Button
			optionsButton = new GameObject("Options Button");
			optionsButton.tag = "OptionsButton";
			var optionsRender = optionsButton.AddComponent<SpriteRenderer>();
			optionsRender.sprite = SpriteReference.spriteOptions;
			optionsButton.AddComponent<BoxCollider2D>();
			optionsButton.AddComponent<OptionsButtonCollision>();
			optionsButton.transform.localPosition = new Vector3(0.0f, -1.5f, 0.0f);
			optionsRender.sortingOrder = 2;
			
			//Quit Button
			quitButton = new GameObject("Quit Button");
			quitButton.tag = "QuitButton";
			var quitRender = quitButton.AddComponent<SpriteRenderer>();
			quitRender.sprite = SpriteReference.spriteExit;
			quitButton.AddComponent<BoxCollider2D>();
			quitButton.AddComponent<QuitButtonCollision>();
			quitButton.transform.localPosition = new Vector3(0.0f, -3.0f, 0.0f);
			quitRender.sortingOrder = 2;
			#endregion

			//Menu is visible, so menuCreated is true
			menuCreated = true;
		} //end CreateButtons()

		void DestroyButtons()
		{
			//Destroy menu buttons
			Destroy (GameObject.FindGameObjectWithTag("SoloButton"));
			Destroy (GameObject.FindGameObjectWithTag("MultiButton"));
			Destroy (GameObject.FindGameObjectWithTag("CreditsButton"));
			Destroy (GameObject.FindGameObjectWithTag("OptionsButton"));
			Destroy (GameObject.FindGameObjectWithTag("QuitButton"));

			//Menu is no longer visible, so menuCreated is false
			menuCreated = false;

			//Create Backbutton
			//Back Button
			backButton = new GameObject("Back Button");
			backButton.tag = "BackButton";
			var backRender = backButton.AddComponent<SpriteRenderer>();
			backRender.sprite = SpriteReference.spriteBack;
			backButton.AddComponent<BoxCollider2D>();
			backButton.AddComponent<BackButtonCollision>();
			backButton.transform.localPosition = new Vector3(0.0f, -3.0f, 0.0f);
			backRender.sortingOrder = 2;
		} //end DestroyButtons()

		void CreateMaps()
		{
			//Desert Map
			desertMap = new GameObject("Desert Map");
			desertMap.tag = "DesertMap";
			var desertRender = desertMap.AddComponent<SpriteRenderer>();
			desertRender.sprite = SpriteReference.spriteMapDesert;
			desertMap.AddComponent<BoxCollider2D>();
			desertMap.AddComponent<DesertMapCollision>();
			desertMap.transform.localPosition = new Vector3(-8.5f, 2.5f, 0.0f);
			desertRender.sortingOrder = 2;

			//Euro Map
			euroMap = new GameObject("Euro Map");
			euroMap.tag = "EuroMap";
			var euroRender = euroMap.AddComponent<SpriteRenderer>();
			euroRender.sprite = SpriteReference.spriteMapEuro;
			euroMap.AddComponent<BoxCollider2D>();
			euroMap.AddComponent<EuroMapCollision>();
			euroMap.transform.localPosition = new Vector3(-3.0f, 2.5f, 0.0f);
			euroRender.sortingOrder = 2;

			//Metro Map
			metroMap = new GameObject("Metro Map");
			metroMap.tag = "MetroMap";
			var metroRender = metroMap.AddComponent<SpriteRenderer>();
			metroRender.sprite = SpriteReference.spriteMapMetro;
			metroMap.AddComponent<BoxCollider2D>();
			metroMap.AddComponent<MetroMapCollision>();
			metroMap.transform.localPosition = new Vector3(3.0f, 2.5f, 0.0f);
			metroRender.sortingOrder = 2;

			//Snow Map
			snowMap = new GameObject("Snow Map");
			snowMap.tag = "SnowMap";
			var snowRender = snowMap.AddComponent<SpriteRenderer>();
			snowRender.sprite = SpriteReference.spriteMapSnowy;
			snowMap.AddComponent<BoxCollider2D>();
			snowMap.AddComponent<SnowMapCollision>();
			snowMap.transform.localPosition = new Vector3(8.5f, 2.5f, 0.0f);
			snowRender.sortingOrder = 2;

			//Maps are visible, set mapsCreated to true
			mapsCreated = true;
		} //end CreateMaps()

		void DestroyMaps()
		{
			//Destroy the maps
			Destroy (GameObject.FindGameObjectWithTag("DesertMap"));
			Destroy (GameObject.FindGameObjectWithTag("EuroMap"));
			Destroy (GameObject.FindGameObjectWithTag("MetroMap"));
			Destroy (GameObject.FindGameObjectWithTag("SnowMap"));

			//Maps no longer visible, set mapsCreated to false
			mapsCreated = false;
		} //end DestroyMaps()
		public void ChangeMenuState(MENUSTATES state)
		{
			m_menuState = state;
		} //end ChangeMenuState(MENUSTATES state)
	} //end StateMachine class
} //end namespace GSP
