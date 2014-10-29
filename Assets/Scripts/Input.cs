using System.Collections;
using UnityEngine;

namespace GSP
{
	public class Input : MonoBehaviour
	{
		private Die m_dice;

		// Gets the Die object.
		public Die Dice
		{
			get { return m_dice; }
		}

		// Use this for initialization
		void Start()
		{
			// Create a die object.
			m_dice = new Die();
		}
		
		// Update is called once per frame
		void Update()
		{
			// Check if the space key is pressed to roll the di(c)e.
			if ( UnityEngine.Input.GetKeyDown( KeyCode.Space ) )
			{
				print( "Die Roll: " +  Dice.Roll());
			}
		}
	}
}
