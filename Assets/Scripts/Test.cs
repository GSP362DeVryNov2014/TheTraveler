using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GSP.Char;

namespace GSP
{
	public class Test : MonoBehaviour
	{
		Dictionary<int, int> m_currencyDict;
		
		
		// Use this for ininitialisation.
		public void Start()
		{
			// Initialises the player currency dictionary to a new dictionary.
			m_currencyDict = new Dictionary<int, int>();
		}

		// Update is called once per frame.
		void Update()
		{
			// Tests getting the player's info
			if ( Input.GetKeyDown( KeyCode.A ) )
			{
				// Get the game object with the end scene data tag.
				GameObject endSceneDataObject = GameObject.FindGameObjectWithTag( "EndSceneDataTag" );

				// Get its script.
				EndSceneData endSceneScript = endSceneDataObject.GetComponent<EndSceneData>();

				// Get the number of players.
				int numPlayers = endSceneScript.Count;

				// Loop over the data in the end scene data script.
				for ( int index = 0; index < numPlayers; index++ )
				{
					// Increase index by one to get the player number.
					int playerNum = index + 1;

					// Get the player's char data.
					EndSceneCharData endScenechardata = endSceneScript.GetData( playerNum );

					// Debug what's in the char data object.
					Debug.Log( "Player Number: " + endScenechardata.PlayerNumber );
					Debug.Log( "Player Name: " + endScenechardata.PlayerName );
					Debug.Log( "Player Currency: " + endScenechardata.PlayerCurrency );
				} // end for loop
			} // end if statement

			// Tests checking to player's currency.
			if ( Input.GetKeyDown( KeyCode.B ) )
			{
				// Get the game object with the end scene data tag.
				GameObject endSceneDataObject = GameObject.FindGameObjectWithTag( "EndSceneDataTag" );
				
				// Get its script.
				EndSceneData endSceneScript = endSceneDataObject.GetComponent<EndSceneData>();

				// Get the number of players.
				int numPlayers = endSceneScript.Count;

				// Loop over the data in the end scene data script.
				for ( int index = 0; index < numPlayers; index++ )
				{
					// Increase index by one to get the player number.
					int playerNum = index + 1;

					// Only proceed if the player's currency hasn't been added.
					if ( !m_currencyDict.ContainsKey( playerNum ) )
					{
						// Get the player's char data.
						EndSceneCharData endScenechardata = endSceneScript.GetData( playerNum );

						// Add the player's number as the key and its currency as the value to the dictionary.
						m_currencyDict.Add( playerNum, endScenechardata.PlayerCurrency );
					} // end if statement
				} // end for loop

				// Now we sort the currency dictionary.
				IEnumerable<KeyValuePair<int, int>> sortedCurrency = from entry in m_currencyDict orderby entry.Value descending select entry;

				// Create a list from this ordering.
				List<KeyValuePair<int, int>> currencyList = sortedCurrency.ToList();

				// Loop through the list showing the key/value pairs in the console.
				foreach ( var item in currencyList )
				{
					Debug.Log( "List Key: " + item.Key );
					Debug.Log( "List Value: " + item.Value );
				} // end foreach loop.

				// Now get the player at the front of the list as the winner.
				// This only happens because it was sorted to have the highest at the top.
				Debug.Log( "Player #" + currencyList[0].Key + " is the winner!" );
			} // end if statement
		} // end Update function
	} // end Test class
} // end namespace
