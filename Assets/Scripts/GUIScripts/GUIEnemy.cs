using UnityEngine;
using System.Collections;

namespace GSP.JAVIERGUI
{

	public class GUIEnemy : MonoBehaviour {
		GSP.GUIMapEvents m_GUIMapEventsScript;
		GameObject m_PlayerEntity;

		//main container values
		int m_mainWidth = -1;
		int	m_mainHeight = -1;
		int m_mainStartX = -1;
		int m_mainStartY = -1;

		// Use this for initialization
		void Start () {
		
		}

		public void InitThis( GameObject p_PlayerEntity, int p_startX, int p_startY, int p_startWdth, int p_startHght)
		{
			m_mainStartX = p_startX;
			m_mainStartY = p_startY;
			m_mainWidth = p_startWdth;
			m_mainHeight = p_startHght;

			m_PlayerEntity = p_PlayerEntity;
		}

		// Update is called once per frame
		void OnGUI () 
		{
			//done button
			int doneWidth = m_mainWidth/2;
			int doneHeight = m_mainHeight / 8;
			int doneStartX = m_mainStartX +(m_mainWidth -doneWidth) /2;
			int doneStartY = m_mainStartY +(doneHeight *7);
			GUI.backgroundColor = Color.red;
			
			if ( GUI.Button (new Rect( doneStartX, doneStartY, doneWidth, doneHeight), "DONE") )
			{
				//once nothing is happening, program returns to Controller's End Turn State
				m_GUIMapEventsScript.MapeEventDone();
			}
		}	//end void OnGUI()
	}	//END public class GUIEnemy

}	//end namepsace GSP.JAVIERGUI