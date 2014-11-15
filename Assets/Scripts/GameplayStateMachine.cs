using UnityEngine;
using System.Collections;

namespace GSP
{

	public class GameplayStateMachine : MonoBehaviour 
		//====================================================================
		// +will be in charge of designing the stateMachine
		// +OnGUI() //need this function to use Unity's GUI
		//			//also runs StateMachine() every cycle
		// -StateMachine() 	//runs all the State Specific GUI tasks
		// -ShowResources()	//toggles between hiding and showing Resources in the GUI bar
		//
		// +GetState() returns int
		//		//ROLLDICE
		//		//CALCDISTANCE
		//		//DISPLAYDISTANCE
		//		//SELECTPATHTOTAKE
		//		//DOACTION
		//		//ENDTURN
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
		
		//		private GUI GuiStateMachine;
		
		//		public int m_diceRoll = 0; 		//if diceRoll equals zero, player has not rolled dice
		//		public int m_allowedTravelDist = 0;	//calculated using dice roll
		//		//public int m_currTravelDist = 0; 	//when this equals or is greater than allowed traveled distance, turn ends.
		//		public Vector2 m_startingHighlightPosition; //high light will begin in players position
		//		public bool m_stageTransition = false;
		//		public bool m_movementConfirm = false;	//this will allow the player to cancel their movement
		//		public bool m_doingAction = false;	//when player hits a map event, this becomes true
		//		public bool m_endTurn = false; 		//when true, will activate a timer and display to the player that turn is ending
		
		//......Holds Current State......
		public GamePlayState m_gamePlayState = GamePlayState.ROLLDICE;	//beginning state
		
		//......GUI Values......
		string m_GUIActionString;	//Changes the String in the Action button According to State player is in
		bool m_GUIActionPressed = false;	//determines if the Action Button has been pressed.
		int m_GUIPlayerTurn = 1;	//whos turn is it
		int m_GUIGoldVal = 0;
		int m_GUIWeight = 0;		//Players actual weight
		int m_GUIMaxWeight = 100;	//Players max weight
		bool m_GUIShowResources = false;	// GUI for Resources can be hidden at push of button
		int m_GUIOre = 0;
		int m_GUIWool = 0;
		int m_GUIDiceDistVal = 0;	//HOlds the dice value, then is converted into Distance Value
		int m_GUINumOfPlayers = 2; 	// how many players playing
		
		//...Instances...
		public GSP.DieInput m_DieScript;	//Access the sigleton Die and its functions
		
		//=================================================================================================
		//-------------------------------------------------------------------------------------------------
		//=================================================================================================
		
		void Start()
			//------------------------------------------------------------------
			//  Start() runs when the object if first instantiated
			//		-Because this object will occur once through the game, 
			//			these values are the beginning of game values
			//		-Values should be updated at EndTurn State of StateMachine()
			//-------------------------------------------------------------------
		{
			m_DieScript = GameObject.FindGameObjectWithTag("DieTag").GetComponent<DieInput>();
			m_GUIActionString = "Action\nButton";
		}
		
		void OnGUI()
			//-------------------------------------------------------------------
			//	OnGUI() is needed in order to run GUI. functions. Runs once per cycle
			//	-will run StateMachine() every cycle to make sure on correct State
			//	-Uses some config functions for Cleaning up GUI look and special occasions
			//--------------------------------------------------------------------
		{
			StateMachine();	//update any values that affect GUI before creating GUI
			
			//Buttons will be red
			GUI.backgroundColor = Color.red;
			
			//This is the tool bar container
			GUI.Box( new Rect(0,0,Screen.width,64)," ");
			
			//Scalable values for GUI miniContainers
			int gap = 2;
			int width = (Screen.width/4)-2;
			int height = 28;
			
			//..................................
			//   ...PLAYER AND GOLD COLUMN...
			//..................................
			int col = 0;
			GUI.Box (new Rect ((col*width)+(col+1)*gap, 2,width, height), "Player: "+m_GUIPlayerTurn.ToString());		//Whose Turn
			GUI.Box (new Rect ((col*width)+(col+1)*gap,32, width, height), "Gold: $"+m_GUIGoldVal.ToString());			//How Much Gold
			
			//..................................
			// ...WEIGHT AND RESOURCE COLUMN...
			//..................................
			col = col+1;
			GUI.Box(new Rect ((col*width)+(col+1)*gap,2,width, height), "Weight: "+m_GUIGoldVal.ToString());	//weight container
			ResourceButtonConfig (gap, col, width, height);
			//Show/hides Resources
			ShowResources();
			
			//..................................
			//      ...DICE ROLL COLUMN...
			//..................................
			col = col+1;
			DiceBoxConfig(gap, col, width, height);
			
			//..................................
			//      ...ACTION COLUMN...
			//..................................
			col = col + 1;
			ActionButtonConfig (gap, col, width, height);
			
		}	//end OnGUI()
		
		private void ShowResources()
			//----------------------------------------------------
			//	Hides and shows the Resources
			//
			//----------------------------------------------------
		{
			if (m_GUIShowResources) 
			{
				//Resrouces GUI Container attributes
				const int NUMOFRSRCS = 2; //change this to meet our needs
				int gap = 2;
				int width = (Screen.width/NUMOFRSRCS)-2;
				int height = 28;
				
				//...............
				//   ...Ore...
				//..............
				int col = 0;
				GUI.Box(new Rect ((col*width)+(col+1)*gap,64,width, height), "Ore: "+m_GUIOre.ToString());
				
				//...............
				//  ...Wool...
				//...............
				col = col +1;
				GUI.Box(new Rect ((col*width)+(col+1)*gap,64,width, height), "Wool: "+m_GUIWool.ToString());
				//GUI.Box(new Rect ((col*width)+(col+1)*gap,2,width, height), "Weight: "+m_GUIGoldVal.ToString());
				//GUI.Box(new Rect ((col*width)+(col+1)*gap,2,width, height), "Weight: "+m_GUIGoldVal.ToString());
			}
			
		} //end private void ShowResources()
		
		
		private void ResourceButtonConfig(int p_gap, int p_col, int p_width, int p_height)
			//--------------------------------------------------------------------------------
			//	-Configures Resource Button to display correctly on screen
			//	-Toggles betweend ShowResources or Hide Resources boolean
			//
			//--------------------------------------------------------------------------------
		{
			if (GUI.Button (new Rect ((p_col * p_width) + (p_col + 1) * p_gap, 32, p_width, p_height),"Resources")) 		//resrouces toggle Show/Hide Button
			{
				
				if ( !(m_GUIShowResources) ) 
				{	m_GUIShowResources = true; }	//Show Resources Containers
				else 
				{ m_GUIShowResources = false; }		//Hide Resources' Containers
				
			}	//end if(GUI.Button(Resources))
			
		}	//end private void ResourceButtonConfig( int p_gap, int p_col, int p_width, int p_height )
		
		
		private void DiceBoxConfig( int p_gap, int p_col, int p_width, int p_height )
			//--------------------------------------------------------------------------
			//	-Configures DiceBox container in GUI
			//	-Special Occassion: When the Dice has not been rolled, is should display
			//		"DICE ROLL [Press Action Button]"
			//
			//--------------------------------------------------------------------------
		{
			//if in first state, tell the user to push the Action Button to begin rolling the Die
			if (m_gamePlayState == GamePlayState.ROLLDICE) 
			{
				GUI.Box (new Rect ((p_col * p_width) + (p_col + 1) * p_gap, 2, 
				                   p_width, 2 * p_height), "DICE ROLL\n[Press Action\nButton]");
			} 
			else
				//else display the Rolled Value
			{
				GUI.Box (new Rect ((p_col * p_width) + (p_col + 1) * p_gap, 2,
				                   p_width, 2 * p_height), "Travel Dist.\n\n"+m_GUIDiceDistVal);
			}
			
		}	//end private void DiceBoxConfig( int gap, in col, int width, int height )
		
		
		private void ActionButtonConfig( int gap, int col, int width, int height )
			//-----------------------------------------------------------------------------
			//	Action button has a few different settings to consider,
			//		-In most states it is a simple click to change state,
			//			and it can only be clicked once
			//		-But in the SelectPathToTake phase, it can be clicked 
			//			multiple times to confirm path, and cancel path
			//		 
			//------------------------------------------------------------------------------
		{
			//If state is not SELECTPATHTOTAKE, Action Button changes states
			if ( (m_gamePlayState != GamePlayState.SELECTPATHTOTAKE)) {
				if (!(m_GUIActionPressed)) {
					if (GUI.Button (new Rect ((col * width) + (col + 1) * gap, 2, width, 2 * height), m_GUIActionString)) 
					{
						m_GUIActionPressed = true;
					}
				} 
				else 
				{
					GUI.Box (new Rect ((col * width) + (col + 1) * gap, 2, width, 2 * height), m_GUIActionString);
				}
			} 
			//Else Action Button Has multiple uses in one phase
			else 
			{
				if (GUI.Button (new Rect ((col * width) + (col + 1) * gap, 2, width, 2 * height), m_GUIActionString)) 
				{
					if( m_GUIActionPressed )
					{
						m_GUIActionPressed = false;
					}
					else
					{
						m_GUIActionPressed = true;
					}
				}	
			} //end if (Gameplaystate == SelectPathToTake){} else {}
			
		} //end private void ActionButtonConfig(int gap, int col, int width, int height)
		
		
		private void StateMachine()
			//---------------------------------------------------------------------
			// +StateMachine in charge of displaying GUI that describes
			//	 what stage the gamePlay is in
			// +Uses a switch() statement for handling states
			//
			//---------------------------------------------------------------------
		{
			var state = GameObject.FindGameObjectWithTag("GUITextTag").GetComponent<GUIText>();
			switch (m_gamePlayState) 
			{
			case GamePlayState.ROLLDICE:
				
				state.text = "roll dice";
				//create a roll dice button
				m_GUIActionString = "Action\nRoll Dice";
				//if button is clicked, destroy button
				if( m_GUIActionPressed )
				{
					m_GUIDiceDistVal = m_DieScript.Dice.Roll();
					//nextState()
					m_gamePlayState = GamePlayState.CALCDISTANCE;
				}					
				break;
			case GamePlayState.CALCDISTANCE:
				
				state.text = "Calculate Distance";
				//get dice value /calculate m_allowedTravelDistance
				m_GUIDiceDistVal = (m_GUIMaxWeight-m_GUIWeight)/m_GUIMaxWeight;
				//nextState()
				m_GUIActionPressed = false;
				m_gamePlayState = GamePlayState.DISPLAYDISTANCE;
				break;
			case GamePlayState.DISPLAYDISTANCE:
				state.text = "DisplayDistance";
				//starting from playersNode positions, highlight
				// tiles that can be traveled on
				//nextState()
				m_gamePlayState = GamePlayState.SELECTPATHTOTAKE;
				break;
			case GamePlayState.SELECTPATHTOTAKE:
				state.text = "Select Path To Take\nPress 1 to EndTurn,\nPress 2 to Do Action";
				//player object can request for m_allowedTravelDistance
				//...this code should be created in player object...
				//if GamePlayState.SELECTPATHTOTAKE
				//node selection
				//pathfind to node selection
				//if playerTravelCount == 0
				//m_gamePlayState = GamePlayState.ENDTURN
				if ( Input.GetKeyDown( KeyCode.Alpha1 ) )
				{
					m_gamePlayState = GamePlayState.ENDTURN;
				}
				//if Mapevent occurs
				//m_gamePlayState = GamePlayState.DOACTION
				if ( Input.GetKeyDown( KeyCode.Alpha2 ) )
				{
					m_gamePlayState = GamePlayState.DOACTION;
				}
				break;
			case GamePlayState.DOACTION:
				state.text = "Do Action/MapEvent";
				//map events
				//if map event is complete
				//NextState()
				break;
			case GamePlayState.ENDTURN:
				state.text = "End Turn";
				
				//next players turn
				m_GUIPlayerTurn = m_GUIPlayerTurn +1;
				//if exceeds the number of player playing, start back at player 1
				if( m_GUIPlayerTurn > m_GUINumOfPlayers )
				{
					m_GUIPlayerTurn = 1;
				}
				
				m_gamePlayState = GamePlayState.ROLLDICE;
				break;
				
			default:
				//stage transition
				break;
				
			} //end switch (m_gamePlayState)
			
		} //end private void StateMachine()
		
		public int GetState()
			//---------------------------------------------------------
			//	-If another class wants to get the Current State, 
			//		this function will return int form of Enum States
			//
			//----------------------------------------------------------
		{
			int state;
			state = (int)m_gamePlayState;
			return state;
		}
		
		public void NextState()
			//---------------------------------------------------------
			// changes to the next state
			//		-MIGHT NOT BE NEEDED IF GUI CONTROLS
			//			WHICH STATES COME NEXT
			//----------------------------------------------------------
		{
			m_gamePlayState = m_gamePlayState +1;
		} //end public void NextStage()
		
		public void EndTurn()
			//---------------------------------------------------------
			// automatically changes to endTurn state
			// and resets values
			//---------------------------------------------------------
		{
			m_gamePlayState = GamePlayState.ENDTURN;
			
		} //end public void ENDTURN()
		
		private void ResetValues()
			//---------------------------------------------------------
			// Resets Values at the end of a Turn right before a New 
			// 	New Players Turn
			//
			//---------------------------------------------------------
		{
			m_GUIActionString = "Action\nButton";
			bool m_GUIActionPressed = false;
			int m_GUIGoldVal = 0;
			int m_GUIWeight = 0;
			int m_GUIMaxWeight = 100;
			bool m_GUIShowResources = false;
			int m_GUIOre = 0;
			int m_GUIWool = 0;
			int m_GUIDiceDistVal = 0;
			
		}	//end private void ResetValues();
		
		public void OnDrawGizmos()
			//---------------------------------------------------------
			//to draw on the scene.
			//	-ORIGINALLY GOING TO BE USED FOR A DIFFERENT
			//		TYPE OF UNITY GUI, BUT NO LONGER NEEDED
			//---------------------------------------------------------
		{
			
		} //end public void OnDrawGizmos()
		
	}	//end public class GameplayStateMachine : MonoBehaviour

}	//end namespace GSP
