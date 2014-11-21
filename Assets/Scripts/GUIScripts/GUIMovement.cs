using UnityEngine;
using System.Collections;

namespace GSP
{

	public class GUIMovement : MonoBehaviour {
		bool m_isInSelectPathToTakeState = false; //this value is rturned to the GameplayStateMachine, when false, it means ends turn
		GameObject m_PlayerEntity; //the current player object
		private int m_SelectState = (int)GamePlayState.SELECTPATHTOTAKE;	//this is the state we are concerned about
		GSP.GameplayStateMachine m_GameplayStateMachineScript; //script to acces state machine 
		Movement m_MovementScript;

		Vector3 m_displacementVector;	//value player moves relative to Space.World
		Vector3 m_origPlayerPosition;	//if player cancels movement, player resets to this original poisition
		int m_initialTravelDist; //initial dice roll
		int m_currTravelDist;	//dice roll left
		bool m_isMoving = false; //if the player is moving, one dice roll value is taken off

		//TODO: remove this variable when Brents functions are ready.
		int m_testCount =0;

		//+enum will hold the values of gameplay states
		private enum GamePlayState
		{
			BEGINTURN,
			ROLLDICE,
			CALCDISTANCE,
			DISPLAYDISTANCE,
			SELECTPATHTOTAKE,
			DOACTION,
			ENDTURN
		} //end public enum GamePlayState

		// Use this for initialization
		void Start () {
			m_GameplayStateMachineScript = GameObject.FindGameObjectWithTag("GamePlayStateMachineTag").GetComponent<GameplayStateMachine>();
	
		}	//end void Start()

		// custom overloaded constructor
		public void InitThis( GameObject p_PlayerEntity, int p_travelDistance )
		{
			//player entity
			m_PlayerEntity = p_PlayerEntity;

			//turn just started
			m_isInSelectPathToTakeState = true;

			//store orig position
			m_origPlayerPosition = p_PlayerEntity.transform.position;

			//how much can the player move
			m_initialTravelDist = p_travelDistance;
			m_currTravelDist = m_initialTravelDist;

			//resetDisplacement Value
			m_displacementVector = new Vector3 (0.0f, 0.0f, 0.0f); 

		}	//end public void InitThis()
		
		void OnGUI()
		{
			//listen for change in state on the GameplayStateMachine
			if ( m_GameplayStateMachineScript.GetState() != m_SelectState )
			{
				m_isInSelectPathToTakeState = false;
			}

			//if the GameplayStateMachin is in SELECTPATHTOTAKE state, show gui
			if (m_isInSelectPathToTakeState == true)
			{
				GUIMovementPads();
				if(m_isMoving == true )
				{
					MovePlayer();
				}
			}

		} //end OnGUI()

		private void GUIMovementPads()
		{
			int width = 32;
			int height = 32;
			int gridXShift =-8;
			int gridYShift =-8;

			//Button color
			GUI.backgroundColor = Color.red;
			if( m_currTravelDist > 0 )
			{
				//left
				if ( GUI.Button (new Rect ((Screen.width - (3 * width) + gridXShift), (Screen.height - (2 * height) + gridYShift), width, height), "<")) 
				{
					//TODO: MOVE DOWN FUNCTION FROM BRENTS MOVEMENT CLASS; MOVEMENT CLASS 
					//NEEDS TO BE ABSTRACT IF POSSIBLE (IF NOT CREATE AN INSTANCE IN THE UNITY SCENE
					m_displacementVector = new Vector3( -.32f, 0.0f, 0.0f ); //FOR TESTING
					//m_displacementVector = m_MovementScript.MoveLeft(); //uncomment this and comment above when Brents Movement class is ready
					MovePlayer();
				}
				//right
				if( GUI.Button( new Rect( (Screen.width -(1*width) +gridXShift), (Screen.height -(2*height) +gridYShift), width, height ), ">" ) )
				{
					//TODO: MOVE DOWN FUNCTION FROM BRENTS MOVEMENT CLASS; MOVEMENT CLASS 
					//NEEDS TO BE ABSTRACT IF POSSIBLE (IF NOT CREATE AN INSTANCE IN THE UNITY SCENE
					m_displacementVector = new Vector3(  .32f, 0.0f, 0.0f ); //FOR TESTING
					//m_displacementVector = m_MovementScript.MoveRight(); //uncomment this and comment above when Brents Movement class is ready
					MovePlayer();
				}
				//cancel
				if( GUI.Button( new Rect( (Screen.width -(2*width) +gridXShift), (Screen.height -(2*height) +gridYShift), width, height ), "X" ) )
				{
					//TODO: CANCEL MOVE, MOVE BACK TO ORIG POSITION
					CancelMove();
				}
				//up
				if( GUI.Button( new Rect( (Screen.width -(2*width) +gridXShift), (Screen.height -(3*height) +gridYShift), width, height ), "^" ) )
				{
					//TODO: MOVE DOWN FUNCTION FROM BRENTS MOVEMENT CLASS; MOVEMENT CLASS 
					//NEEDS TO BE ABSTRACT IF POSSIBLE (IF NOT CREATE AN INSTANCE IN THE UNITY SCENE
					m_displacementVector = new Vector3( 0.0f, -.32f, 0.0f ); //FOR TESTING
					//m_displacementVector = m_MovementScript.MoveUp(); //uncomment this and comment above when Brents Movement class is ready
					MovePlayer();
				}
				//down
				if( GUI.Button( new Rect( (Screen.width -(2*width) +gridXShift), (Screen.height -(1*height) +gridYShift), width, height ), "v" ) )
				{
					//TODO: MOVE DOWN FUNCTION FROM BRENTS MOVEMENT CLASS; MOVEMENT CLASS 
					//NEEDS TO BE ABSTRACT IF POSSIBLE (IF NOT CREATE AN INSTANCE IN THE UNITY SCENE
					m_displacementVector = new Vector3( 0.0f, -.32f, 0.0f ); //FOR TESTING
					//m_displacementVector = m_MovementScript.MoveDown(); //uncomment this and comment above when Brents Movement class is ready
					MovePlayer();
				}

			} //end if( m_currDistTravel > 0 )
			else
			{
				if( GUI.Button( new Rect( (Screen.width -(2*width) +gridXShift), (Screen.height -(2*height) +gridYShift), width, height ), "X" ) )
				{
					//TODO: CANCEL MOVE, MOVE BACK TO ORIG POSITION
					CancelMove();
				}
				//up
				GUI.Box( new Rect( (Screen.width -(3*width) +gridXShift), (Screen.height -(3*height) +gridYShift), 3*width, height ), "Out of Distance." );
			
			}

		}	//private void GUIMovementPads()

		private void MovePlayer( )
		{
			if( m_displacementVector == new Vector3 (0.0f, 0.0f, 0.0f) )
			{
				m_isMoving = false;
				return;
			}

			//player started moving take off a dice roll value
			if (m_isMoving == false) 
			{
				m_isMoving = true;
				m_currTravelDist = m_currTravelDist -1;
				//TODO:remove this testcount when Brents functions are ready
				m_testCount =0;	//reset test count
			}
			//........................................................
			//.......TODO: this else statement is strictly for testing
			//.....remove after Movement.cs is ready.......
			//........................................................
			else
			{
				//after 2 testcounts, player should reach new tile
				//normally this is checked by the Movement.cs script
				//and will return Vector3(0.0f,0.0f,0.0f)
				m_testCount = m_testCount +1;
				if(m_testCount >= 2)
				{
				 	m_displacementVector = new Vector3(0.0f,0.0f,0.0f);
				}
			}

			//move player
			m_PlayerEntity.transform.Translate (m_displacementVector, Space.World );
			//reset 

		} //end private void MovePlayer(Vector3 p_displacementVector )

		private void CancelMove()
		{
			m_PlayerEntity.transform.position = m_origPlayerPosition;
			m_currTravelDist = m_initialTravelDist;
			m_isMoving = false;
			m_displacementVector = new Vector3 (0.0f, 0.0f, 0.0f);
		}	//end private void CancelMove()


		public int GetTravelDistanceLeft()
		{
			return m_currTravelDist;

		} //end public int GetTravelDistanceLeft()

	} //end public class GUIMovement

} //end namespace GSP

