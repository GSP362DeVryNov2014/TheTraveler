using UnityEngine;
using System.Collections;

namespace GSP.Char
{
	public class EndSceneCharData
	{
		// Declare our private variables.
		int m_playerNumber;		// Holds the player's number.
		string m_PlayerString;	// Holds the player's name based of its number.
		int m_playerCurrency;		// Holds the player's currency amount.
		
		// Constructor for creating the end scene data.
		public EndSceneCharData( int playerNum, GameObject player )
		{
			// Set the player's number.
			m_playerNumber = playerNum;
			
			// Set the player's name based on the above number.
			m_PlayerString = "Player " + m_playerNumber.ToString();
			
			// Get the character script of the player.
			Character charScript = player.GetComponent<Character>();

			// Set the player's currency.
			m_playerCurrency = charScript.Currency;
		} // end EndSceneCharData constructor
	} // end EndSceneCharData class
} // end namespace
