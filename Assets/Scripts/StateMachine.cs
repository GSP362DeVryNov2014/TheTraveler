using UnityEngine;
using System.Collections;
using System;

namespace GSP
{	
	public class GameplayStateMachine : MonoBehaviour 
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

		//...GUI Values...
		bool m_GUIActionPressed = false;
		int m_GUIPlayerTurn = 1;
		int m_GUIGoldVal = 0;
		int m_GUIWeight = 0;
		int m_GUIMaxWeight = 100;
		int m_GUIOre = 0;
		int m_GUIWool = 0;
		int m_GUIDiceDistVal = 0;

		//...Instances...
		public GSP.DieInput m_DieScript;

		void Start()
		{
			m_DieScript = GameObject.FindGameObjectWithTag("DieTag").GetComponent<DieInput>();
		}

		void Update()
		{
			StateMachine();
		}

		void OnGUI()
		{
			GUI.backgroundColor = Color.red;
			//This is the tool bar container
			GUI.Box( new Rect(0,0,Screen.width,64)," ");

			int gap = 2;
			int width = (Screen.width/4)-2;
			int height = 28;

			//...PLAYER AND GOLD COLUMN...
			int col = 0;
			GUI.Box (new Rect ((col*width)+(col+1)*gap, 2,width, height), "Player: "+m_GUIPlayerTurn.ToString());
			GUI.Box (new Rect ((col*width)+(col+1)*gap,32, width, height), "Gold: $"+m_GUIGoldVal.ToString());

			//...WEIGHT AND RESOURCE COLUMN...
			col = col+1;
			GUI.Box(new Rect ((col*width)+(col+1)*gap,2,width, height), "Weight: "+m_GUIGoldVal.ToString());
			GUI.Box(new Rect ((col*width)+(col+1)*gap,32,width, height), "Resources [v]");

			//...DICE ROLL COLUMN...
			col = col+1;
			if (m_gamePlayState == GamePlayState.ROLLDICE) 
			{
				GUI.Box (new Rect ((col * width) + (col + 1) * gap, 2, width, 2 * height), "DICE\nROLL");
			} 
			else 
			{
				GUI.Box (new Rect ((col * width) + (col + 1) * gap, 2, width, 2 * height), "Travel Dist.:\n"+m_GUIDiceDistVal);
				//GUI.Box(new Rect ((col*width)+(col)*gap,32,width, height), "");
			}

			//...ACTION COLUMN...
			col = col + 1;
			if ( !(m_GUIActionPressed) ) 
			{
				if( GUI.Button(new Rect ((col * width) + (col + 1) * gap, 2, width, 2 * height), "Action\nButton") )
				{
					m_GUIActionPressed = true;
				}
			}
			else 
			{
				GUI.Box (new Rect ((col * width) + (col + 1) * gap, 2, width, 2 * height), "Action\nButton");
			}
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
					break;
				case GamePlayState.SELECTPATHTOTAKE:
					state.text = "Select Path To Take";
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
					state.text = "Do Action/MapEvent";
					//map events
					//if map event is complete
						//NextState()
					break;
				case GamePlayState.ENDTURN:
					state.text = "End Turn";
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

	}	//end public class GameplayStateMachine : MonoBehaviour
} //end namespace GSP
