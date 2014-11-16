using UnityEngine;
using System.Collections;

	
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
			ENEMY, ALLY, RESOURCES, ITEM, WEATHER, NOTHING, SIZE
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
		
		//==========================================================
		//--------------------Functions-----------------------------
		//==========================================================
		
		// Use this for initialization
		void Start () {
			m_currEnumMpEvent = m_EnumMapEvent.NOTHING;
		}
		
		// Update is called once per frame
		void Update () 
		{
			
		} //end Update()
		
		//OnGUI called once per cycle
		void OnGUI()
		{
			m_currEnumMpEvent = GetMapEvent();


			if (m_showHideGUI == true) 
			{
				GUIContainer();
			}

		} //end void OnGUI()
		
		private m_EnumMapEvent GetMapEvent()
			//----------------------------------------------------------
			//	Gets (enum) MapEvent.normalTile{ ENEMY, ALLY, ITEM, 
			//									WEATHER, NOTHING, SIZE };
			//
			//----------------------------------------------------------
		{
			if( Input.GetKeyDown(KeyCode.Alpha1) )
			{
				m_showHideGUI = true;
				m_currEnumMpEvent = m_EnumMapEvent.ENEMY;
			}
			else if( Input.GetKeyDown(KeyCode.Alpha2) )
			{
				m_showHideGUI = true;
				m_currEnumMpEvent = m_EnumMapEvent.ALLY;			
			}
			else if( Input.GetKeyDown(KeyCode.Alpha3) )
			{
				m_showHideGUI = true;
				m_currEnumMpEvent = m_EnumMapEvent.ITEM;
			}
			else if( Input.GetKeyDown(KeyCode.Alpha4) )
			{
				m_showHideGUI = true;
				m_currEnumMpEvent = m_EnumMapEvent.WEATHER;
			}
			else if( Input.GetKeyDown(KeyCode.Alpha5) )
			{
				m_showHideGUI = true;
				m_currEnumMpEvent = m_EnumMapEvent.NOTHING;
			}
			else if( Input.GetKeyDown(KeyCode.Alpha6) )
			{
				m_showHideGUI = true;
				m_currEnumMpEvent = m_EnumMapEvent.SIZE;
			}
			
			return m_currEnumMpEvent;
		}
		
		private m_EnumResourceType GetResourceEnum()
			//----------------------------------------------------------
			//	Gets (enum) MapEvent.normalTile{ WOOL, WOOD, FISH, ORE,
			//									SIZE };
			//
			//----------------------------------------------------------
		{
			if( Input.GetKeyDown(KeyCode.Alpha1) )
			{
				m_showHideGUI = true;
				m_currRsrcType = m_EnumResourceType.WOOL;
			}
			else if( Input.GetKeyDown(KeyCode.Alpha2) )
			{
				m_showHideGUI = true;
				m_currRsrcType = m_EnumResourceType.WOOD;
			}
			else if( Input.GetKeyDown(KeyCode.Alpha3) )
			{
				m_showHideGUI = true;
				m_currRsrcType = m_EnumResourceType.FISH;
			}
			else if( Input.GetKeyDown(KeyCode.Alpha4) )
			{
				m_showHideGUI = true;
				m_currRsrcType = m_EnumResourceType.ORE;
			}
			else if( Input.GetKeyDown(KeyCode.Alpha5) )
			{
				m_showHideGUI = true;
				m_currRsrcType = m_EnumResourceType.SIZE;
			}
			
			return m_currRsrcType;
		}
		
		private void GUIMapEventsMachine()
			//----------------------------------------------------------
			//	Switch that displays the current MapEvent
			//		-has to be called from OnGUI() or a function within OnGUI
			//			in order for Unity's GUI features to work.
			//
			//----------------------------------------------------------
		{
			//GUI parameters
			int gap =2;
			int numOfContainers =2;
			int guiWidth = Screen.width /3;
			int guiHeight = Screen.height /numOfContainers;

			switch (m_currEnumMpEvent) 
			{
			case m_EnumMapEvent.ENEMY:
				
				break;

			case m_EnumMapEvent.ALLY:
				break;

			case m_EnumMapEvent.ITEM:
				break;

			case m_EnumMapEvent.RESOURCES:
				break;

			case m_EnumMapEvent.NOTHING:
				m_showHideGUI = false;
				break;

			case m_EnumMapEvent.SIZE:
				m_showHideGUI = false;
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
			GUI.Box (new Rect(0,65 +32, Screen.width /3, Screen.height /2 ), m_currEnumMpEvent.ToString() );

			//Check which GUI to Display
			GUIMapEventsMachine(); //runs once every cycle; checks, what state we are in
		
		} //end private void GUIContainer()
		
	} //class GUIMapEvents{}
	
} //end namespace GSP

