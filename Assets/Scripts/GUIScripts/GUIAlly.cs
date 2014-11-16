using UnityEngine;
using System.Collections;

public class GUIAlly : MonoBehaviour 
////////////////////////////////////////////////////////////////////////
//	Creates the GUI for ALLY MapEvent
//		on Start()		
//			+gets information from Char
//			+ask to add Ally
//				yes: addAlly, then checkAllies()
//				no: skip to checkAllies()
//
//		on CheckAllies()
//			if Allies Exist
//				+would you like to transfer weight?
//					yes: display Allies one at a time, add/remove weight to
//						currAlly, display DoneButton()
//					no: exit()
//			else
//				+tell Players no allies exist
//				+display DoneButton()
//				exit()
//
//		//on DoneButton()
//			exit()
//
//		on Exit() 
///			+adds values back into the Char
//			+Prompt Results with a ok button that returns false
//				
//
//	Steps:
//		1. Would you like to add Ally?
//		2. Would you like to take Some Weight off your shoulder?
//		3. Are you sure thats it?
////////////////////////////////////////////////////////////////////////
{

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	private void getPlayerAllyValues()
	{


	}	//end private void getPlayerAllyValues()
	
}
