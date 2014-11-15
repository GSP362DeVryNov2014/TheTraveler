using UnityEngine;
using System.Collections;

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

	}	//end public class GameplayStateMachine : MonoBehaviour

}	//end namespace GSP
