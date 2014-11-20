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
				int width = 16;
				int height = 16;
				int gap = 2;

				GUI.Button( new Rect( (Screen.width-(3*width)-gap), (Screen.height-(2*height)-gap), width, height ), "Left" );
			}

		} //end OnGUI()

		public int GetTravelDistanceLeft()
		{
			return m_travelDist;

		} //end public int GetTravelDistanceLeft()

	} //end public class GUIMovement

} //end namespace GSP

