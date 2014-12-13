using UnityEngine;
using System.Collections;

namespace GSP
{
	public class OptionsButtonCollision : MonoBehaviour 
	{
		void OnMouseDown()
		{
			BrentsStateMachine stateMachine = GameObject.FindGameObjectWithTag ("GameController").
				GetComponent<BrentsStateMachine>();
			stateMachine.ChangeMenuState (BrentsStateMachine.MENUSTATES.OPTIONS);
		} //end OnMouseDown()
	} //end SoloButtonCollision
} //end namespace GSP
