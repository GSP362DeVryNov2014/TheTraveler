using UnityEngine;
using System.Collections;


namespace GSP.JAVIERGUI
{

	public class GUIBottomBar : MonoBehaviour 
	{
		//...Scripts...
		GSP.Char.Character m_CharacterScript;

		//....Bottom Bar Configuration values....
		private int m_gapInYdirection;
		private int m_gapInXdirection;
		private int m_numOfButtonsInXDirection;
		private int m_numOfButtonsInYdirection;
		private int m_buttonHeight;
		private int m_buttonWidth;
		private int m_barStartX;
		private int m_barStartY;


		//....Feedback Values....
		private bool m_viewItems = false;
		private int m_attackPower = -1;		//-1 means not initialized.
		private int m_defencePower = -1;	//spelled defence in Character.cs

		// Use this for initialization
		void Awake () {
			m_gapInYdirection = 1;
			m_gapInXdirection = 2;
			m_numOfButtonsInXDirection = 1;
			m_numOfButtonsInYdirection = 2;
			m_buttonHeight = 32;
			m_buttonWidth = 64;
			m_barStartX = (m_buttonWidth/4);
			m_barStartY = ( Screen.height - (m_buttonHeight*2) );
		}


		// Use this for initialization
		void Start () {

		}


		public void RefreshBottomBarGUI( GameObject p_playerEntity )
		{
			m_CharacterScript = p_playerEntity.GetComponent<GSP.Char.Character>();
			
		}	//end void RefreshBottomBarGUI()


		void OnGUI()
		{
			//default button color
			GUI.backgroundColor = Color.red;

			int col = 0;
			int row = 0;

			//.............
			//   Row 0
			//.............
				//,,,,,,,,,,,,
				//   Col 0
				//,,,,,,,,,,,,
			ConfigItemButton( (m_barStartX +(col*m_gapInXdirection)), (m_barStartY -(row*m_gapInYdirection)), m_buttonWidth, m_buttonHeight );

			//.............
			//	Row 1
			//.............
				//,,,,,,,,,,,,
				//	Col 0
				//,,,,,,,,,,,,
			row = row + 1;
			col = 0;
			ConfigItemBarDisplay( (m_barStartX +(col*m_buttonWidth +m_gapInXdirection)), (m_barStartY -(row*m_buttonHeight +m_gapInYdirection)), m_buttonWidth, m_buttonHeight );
		}	//end OnGUI()



		private void ConfigItemButton( int p_x, int p_y, int p_width, int p_height )
		{
			if( GUI.Button( new Rect(p_x, p_y, p_width, p_height), "ITEMS") )
			{
				if( m_viewItems == false )
				{
					m_viewItems = true;
				}
				else
				{
					m_viewItems = false;
				}
			}
		}	//end private void ConfigItemButton()



		private void ConfigItemBarDisplay( int p_x, int p_y, int p_width, int p_height )
		{
			if( m_viewItems == true )
			{
				int row = 0;
				int col = 0;
				string resultString = (m_CharacterScript.AttackPower).ToString();
				GUI.Box(new Rect (p_x +(col *p_width), p_y +(row *p_height), p_width, p_height), "AP: " +resultString );

				col = col+1;
				resultString = (m_CharacterScript.DefencePower).ToString();
				GUI.Box(new Rect (p_x +(col *p_width), p_y +(row *p_height), p_width, p_height), "DP: " +resultString );

			}
		}	//edn private void ConfigItemBarDisplay




	}	//end public class GUIBottomBar : MonoBehaviour

}	//end namespace GSP.JAVIERGUI