using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GSP.Char;

namespace GSP
{
	public class Test : MonoBehaviour
	{
		
		// Update is called once per frame
		void Update()
		{
			// Tests getting player 1's name.
			if ( Input.GetKeyDown( KeyCode.A ) )
			{
				// Get the game object with the end scene data tag.
				GameObject endSceneDataObject = GameObject.FindGameObjectWithTag( "EndSceneDataTag" );

				// Get its script.
				EndSceneData endSceneScript = endSceneDataObject.GetComponent<EndSceneData>();

				// Get the end scene char data.
				EndSceneCharData charData = endSceneScript.GetData( 1 );

				Debug.Log("Player Name: " + charData.PlayerName );
			} // end if statement

			// Tests checking to player's currency.
			if ( Input.GetKeyDown( KeyCode.B ) )
			{
				// Get the game object with the end scene data tag.
				GameObject endSceneDataObject = GameObject.FindGameObjectWithTag( "EndSceneDataTag" );
				
				// Get its script.
				EndSceneData endSceneScript = endSceneDataObject.GetComponent<EndSceneData>();

				// Create the list for the player's currencies.
				List<int> playerCurrencies = new List<int>();

				// Add the player's currencies
			} // end if statement
		} // end Update function
	} // end Test class
} // end namespace
