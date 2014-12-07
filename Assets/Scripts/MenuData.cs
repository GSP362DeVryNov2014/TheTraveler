using UnityEngine;
using System.Collections;

namespace GSP
{
	public class MenuData : MonoBehaviour
	{
		// Holds the number of players for the game.
		int m_numberPlayers;
		
		public int NumberPlayers
		{
			get { return m_numberPlayers; }
		} // end NumberPlayers property
		
		// Use this for initialisation.
		void Start()
		{
			// Initialise the variables to zero.
			m_numberPlayers = 0;
		} // end Start function
	} // end MenuData class
}
