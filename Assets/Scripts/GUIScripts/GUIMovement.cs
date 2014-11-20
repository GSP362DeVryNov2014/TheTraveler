using UnityEngine;
using System.Collections;

namespace GSP
{

	public class GUIMovement : MonoBehaviour {
		bool m_canStillMove = false; //this value is rturned to the GameplayStateMachine, when false, it means ends turn
		int m_travelDist;	//how far can player travel

		// Use this for initialization
		void Start () {
		
		}

		// custom overloaded constructor
		public void InitThis( int p_travelDistance )
		{
			//turn just started
			m_canStillMove = true;

			//how much can the player move
			m_travelDist = p_travelDistance;

		}	//end public void InitThis()

		void OnGUI()
		{
			if (m_canStillMove == true)
			{
				int width = 32;
				int height = 32;
				int gridXShift =-8;
				int gridYShift =-8;

				GUI.backgroundColor = Color.red;
				GUI.Button( new Rect( (Screen.width -(3*width) +gridXShift), (Screen.height -(2*height) +gridYShift), width, height ), "<" );
				GUI.Button( new Rect( (Screen.width -(1*width) +gridXShift), (Screen.height -(2*height) +gridYShift), width, height ), ">" );
				GUI.Button( new Rect( (Screen.width -(2*width) +gridXShift), (Screen.height -(2*height) +gridYShift), width, height ), "X" );
				GUI.Button( new Rect( (Screen.width -(2*width) +gridXShift), (Screen.height -(3*height) +gridYShift), width, height ), "^" );
				GUI.Button( new Rect( (Screen.width -(2*width) +gridXShift), (Screen.height -(1*height) +gridYShift), width, height ), "v" );
			}

		} //end OnGUI()

		public int GetTravelDistanceLeft()
		{
			return m_travelDist;

		} //end public int GetTravelDistanceLeft()

	} //end public class GUIMovement

} //end namespace GSP

