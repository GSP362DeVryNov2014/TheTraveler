using UnityEngine;
using System; //needed for Exception
using System.Collections;
using GSP.JAVIERGUI;

namespace GSP
{
	
	public class GUIMapEvents : MonoBehaviour
		//////////////////////////////////////////////////////////////
		// GUI features for all the map Events
		// 		-types of Map Events
		//			+Enemy
		//			+Ally
		//			+Resources
		//				++
		//////////////////////////////////////////////////////////////
	{
		//==========================================================
		//--------------Class Variables-----------------------------
		//==========================================================
		//Will remove this when I integrate with class GSP.MapEvent
		private enum m_EnumMapEvent
		{
			ENEMY, ALLY, RESOURCE, ITEM, WEATHER, NOTHING, DONE
		};
		
		//Will remove this when I integrate with class GSP.MapEvent
		private enum m_EnumResourceType
		{
			WOOL, WOOD, FISH, ORE, SIZE
		};
		
		//Enum holders
		private m_EnumMapEvent m_currEnumMpEvent;
		private m_EnumResourceType m_currRsrcType;
		
		//variables
		bool m_showHideGUI = false;
		bool m_isActionRunning = true;
		
		//playerObject
		private GameObject m_PlayerEntity;
		
		//scripts
		bool m_initScript = false; //this makes sure each script is initialized once per state
		GSP.Char.Ally m_AllyScripts;
		GSP.Char.ResourceList m_ResourceListScript;
		GSP.Char.Items m_ItemScript;
		GSP.JAVIERGUI.GUIEnemy m_GUIEnemyScript;
		GSP.JAVIERGUI.GUIAlly m_GUIAllyScript;
		GSP.JAVIERGUI.GUIItem m_GUIItemScript;
		GSP.JAVIERGUI.GUIResource m_GUIResourceScript;
		GSP.JAVIERGUI.GUINothing m_GUINothingScript;

		//main container values
		int m_mainStartX = 0;
		int m_mainStartY = 65 + 32; //65 is just below the main GUI, and I added a gap of 32 from the end of that
		int m_mainWidth = Screen.width / 3;
		int m_mainHeight = Screen.height / 2;
		string m_resultString;
		
		//==========================================================
		//--------------------Functions-----------------------------
		//==========================================================
		
		// Use this for initialization
		void Start () {
			m_currEnumMpEvent = m_EnumMapEvent.NOTHING;
			m_GUIEnemyScript = GameObject.FindGameObjectWithTag("GUIMapEventSpriteTag").GetComponent<GSP.JAVIERGUI.GUIEnemy>();
			m_GUIAllyScript = GameObject.FindGameObjectWithTag("GUIMapEventSpriteTag").GetComponent<GSP.JAVIERGUI.GUIAlly>();
			m_GUIItemScript = GameObject.FindGameObjectWithTag("GUIMapEventSpriteTag").GetComponent<GSP.JAVIERGUI.GUIItem>();
			m_GUIResourceScript = GameObject.FindGameObjectWithTag ("GUIMapEventSpriteTag").GetComponent<GSP.JAVIERGUI.GUIResource>();
			m_GUINothingScript = GameObject.FindGameObjectWithTag("GUIMapEventSpriteTag").GetComponent<GSP.JAVIERGUI.GUINothing>();
		}
		
		public void InitThis(GameObject p_PlayerEntity, string p_mapEventType, string p_result )
			//----------------------------------------------------
			//	own custome overloaded constructor
			//
			//----------------------------------------------------
		{
			//get player info
			m_PlayerEntity = p_PlayerEntity;
			m_AllyScripts = m_PlayerEntity.GetComponent<GSP.Char.Ally>();
			m_ResourceListScript = m_PlayerEntity.GetComponent<GSP.Char.ResourceList>();
			m_ItemScript = m_PlayerEntity.GetComponent<GSP.Char.Items>();

			// Holds the results of parsing.
			m_EnumMapEvent tmpEnumMapEvent = m_EnumMapEvent.NOTHING;

			try
			{
				// Attempt to parse the string into the enum value.
				tmpEnumMapEvent = (m_EnumMapEvent)Enum.Parse( typeof( m_EnumMapEvent ), p_mapEventType );

			} // end try statement
			catch (Exception)
			{
				// The parsing failed so make sure the resource is null.
				print( "Requested MapEvent type '" + p_mapEventType + "' was not found." );
			} // end catch statement
			
			//set to currEvent
			m_currEnumMpEvent = tmpEnumMapEvent;

			//tell StateMachin action is running
/*			if (m_currEnumMpEvent != m_EnumMapEvent.NOTHING) 
			{
*/				m_isActionRunning = true;
				m_showHideGUI = true;
/*			} 
			else 
			{
				m_showHideGUI = false;
				m_isActionRunning = false;
			}
*/
//			//LOAD RESOURCE IF NECESSARY
//			if ( (p_resourceType != null) && (p_mapEventType == "ITEM") )
//			{
//				// NOTE: I don't know what is trying to be done here. This needs to be fixed. -- Damien
//				m_ResourceListScript.GetResourcesByType( p_resourceType );
//			}
			m_resultString = p_result;
			
		} //end InitThis()
		
		public bool isActionRunning()
			//-----------------------------------------------
			// returs true if the map event is still running
			// returns false if action is complete
			//-------------------------------------------------
		{
			return m_isActionRunning;
			
		} //end public void isActionRunning()
		
		
		//OnGUI called once per cycle
		void OnGUI()
		{
			//function no longer exist, using a function in GamePlaySateMachine that start this
			//MapEvent GUI. READ COMMENTS further down on GetMapEventFunction
			//m_currEnumMpEvent = GetMapEvent();
			
			
			if (m_showHideGUI == true) 
			{
				GUIContainer();
			}
			
		} //end void OnGUI()


		private void GUIMapEventsMachine()
			//----------------------------------------------------------
			//	Switch that displays the current MapEvent
			//		-has to be called from OnGUI() or a function within OnGUI
			//			in order for Unity's GUI features to work.
			//
			//----------------------------------------------------------
		{
			//GUI parameters
	//		int gap =2;
	//		int numOfContainers =2;
	//		int guiWidth = Screen.width /3;
	//		int guiHeight = Screen.height /numOfContainers;
			
			switch (m_currEnumMpEvent) 
			{
			case m_EnumMapEvent.ENEMY:
				if( m_initScript == false )
				{
					m_GUIEnemyScript.InitThis( m_PlayerEntity, m_mainStartX, m_mainStartY, m_mainWidth, m_mainHeight, m_resultString );
					m_initScript = true;
				}
				break;
				
			case m_EnumMapEvent.ALLY:
				if( m_initScript == false )
				{
					m_GUIAllyScript.InitGUIAlly( m_PlayerEntity, m_mainStartX, m_mainStartY, m_mainWidth, m_mainHeight );
					m_initScript = true;
				}
				break;
				
			case m_EnumMapEvent.ITEM:
				if( m_initScript == false )
				{
					m_GUIItemScript.InitGUIItem(m_PlayerEntity, m_mainStartX, m_mainStartY, m_mainWidth, m_mainHeight, m_resultString );
					m_initScript = true;
				}
				break;
				
			case m_EnumMapEvent.RESOURCE:
				if(m_initScript == false)
				{
					m_GUIResourceScript.InitThis(m_PlayerEntity, m_mainStartX, m_mainStartY, m_mainWidth, m_mainHeight, m_resultString );
					m_initScript = true;
				}
				break;
				
			case m_EnumMapEvent.NOTHING:
				if(m_initScript == false)
				{
					m_GUINothingScript.InitThis(m_PlayerEntity, m_mainStartX, m_mainStartY, m_mainWidth, m_mainHeight, m_resultString );
					m_initScript = true;
				}
				break;
				
			case m_EnumMapEvent.DONE:
				m_showHideGUI = false;
				m_isActionRunning = false;
				m_initScript = false;
				break;
	
				
			default:
				print ("GUIMapEventsMachine is in Default");
				break;
			} //end switch (m_currEnumMpEvent
			
		} //end private void GUIMapEventsMachine()


		private void GUIResourceTypeMachine()
			//----------------------------------------------------------
			//	Gets (enum) MapEvent.normalTile{ WOOL, WOOD, FISH, ORE,
			//									SIZE };
			//
			//----------------------------------------------------------
		{
			switch(m_currRsrcType)
			{
			case m_EnumResourceType.WOOL:
				break;
			case m_EnumResourceType.WOOD:
				break;
			case m_EnumResourceType.FISH:
				break;
			case m_EnumResourceType.ORE:
				break;
			case m_EnumResourceType.SIZE:
				break;
			default:
				print("GUIResourceTypeMachine is in Default");
				break;
			} //end switch m_currRsrcType
			
		} //end private void GUIResourceTypeMachine()
		
		private void GUIContainer()
		{
			//main container
			GUI.Box (new Rect(m_mainStartX, m_mainStartY, m_mainWidth, m_mainHeight ), m_currEnumMpEvent.ToString() );

			//Check which GUI to Display
			GUIMapEventsMachine(); //runs once every cycle; checks, what state we are in
			
		} //end private void GUIContainer()

		public void MapeEventDone()
		{
			m_currEnumMpEvent = m_EnumMapEvent.DONE;
		}
		
	} //class GUIMapEvents{}
	
} //end namespace GSP

