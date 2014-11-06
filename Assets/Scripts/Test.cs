using UnityEngine;
using System.Collections;
using GSP.Char;

namespace GSP
{
	public class Test : MonoBehaviour
	{
		// Declare our private variables.
		GameObject m_charRef;				// This is the reference to the character prefab for instantiation.
		GameObject m_player;				// This is the player game object.
		Character m_playerCharScript;		// This is the character script on the player.
		PrefabReference m_prefabRefScript;	// This is the prefab reference script.

		GameObject ally;

		// Use this for initialization
		void Start()
		{
			// Get the prefab reference holder and its script.
			m_charRef = GameObject.FindGameObjectWithTag( "PrefabReferenceHolder" );
			m_prefabRefScript = m_charRef.GetComponent<PrefabReference>();


			// Get the player and the character script attached.
			m_player = GameObject.FindGameObjectWithTag( "Player" );
			m_playerCharScript = m_player.GetComponent<Character>();
		} // end Start function
		
		// Update is called once per frame
		void Update()
		{
			// Test the player's currency.
			if ( Input.GetKeyDown( KeyCode.A ) )
			{
				print( "Player's currency: " + m_playerCharScript.Currency );
			} // end if statement

			// Test the player's adding currency.
			if ( Input.GetKeyDown( KeyCode.B ) )
			{
				print( "Adding 10 currency" );
				m_playerCharScript.AddCurrency( 10 );
			} // end if statement

			// Test the player's removing currency.
			if ( Input.GetKeyDown( KeyCode.C ) )
			{
				print( "Removing 10 currency" );
				m_playerCharScript.RemoveCurrency( 10 );
			} // end if statement

			// Test the player's max weight.
			if ( Input.GetKeyDown( KeyCode.D ) )
			{
				print( "Player's max weight: " + m_playerCharScript.MaxWeight );
			} // end if statement

			// Test the player's increasing the max weight.
			if ( Input.GetKeyDown( KeyCode.E ) )
			{
				print( "Adding 10 to max weight" );
				m_playerCharScript.MaxWeight += 10;
			} // end if statement

			// Test the player's decreasing the max weight.
			if ( Input.GetKeyDown( KeyCode.F ) )
			{
				print( "Removing 10 from max weight" );
				m_playerCharScript.MaxWeight -= 10;
			} // end if statement

			// Test the player's setting the max weight.
			if ( Input.GetKeyDown( KeyCode.G ) )
			{
				print( "Setting max weight to 15" );
				m_playerCharScript.MaxWeight = 15;
			} // end if statement

			// Test the player's resource value.
			if ( Input.GetKeyDown( KeyCode.H ) )
			{
				print( "Player's resource value: " + m_playerCharScript.ResourceValue );
			} // end if statement

			// Test the player's resource weight.
			if ( Input.GetKeyDown( KeyCode.I ) )
			{
				print( "Player's resource weight: " + m_playerCharScript.ResourceWeight );
			} // end if statement

			// Test the player's picking up a resource.
			if ( Input.GetKeyDown( KeyCode.J ) )
			{
				print( "Picking up a resource of 10 value and 10 weight" );
				m_playerCharScript.PickupResource( 10, 10 );
			} // end if statement

			// Test the player's selling of their resources.
			if ( Input.GetKeyDown( KeyCode.K ) )
			{
				print( "Selling resources" );
				m_playerCharScript.SellResource();
			} // end if statement

			// Test the player's number of allies.
			if ( Input.GetKeyDown( KeyCode.L ) )
			{
				print( "Player's number of allies: " + m_playerCharScript.NumAllies );
			} // end if statement

			// Test the player's adding of an ally
			if ( Input.GetKeyDown( KeyCode.M ) )
			{
				// Instantiate a copy of the character prefab.
				print( "Creating an ally");
				ally = Instantiate( m_prefabRefScript.prefabCharacter ) as GameObject;

				// Change its name and give it the ally tag.
				ally.name = "Ally";
				ally.tag = "Ally";

				print( "Adding an ally" );
				m_playerCharScript.AddAlly( ally );
			} // end if statement

			// Test the player's removing of an ally
			if ( Input.GetKeyDown( KeyCode.N ) )
			{
				print( "Removing an ally" );
				m_playerCharScript.RemoveAlly( ally, true );
			} // end if statement

			// Test the ally's setting its owner
			if ( Input.GetKeyDown( KeyCode.O ) )
			{
				// Get the ally's character script.
				Character charScript = ally.GetComponent<Character>();

				print( "Setting ally's owner to player" );
				charScript.Owner = m_player;

				print("Ally's owner's name is: " + charScript.Owner.name);
			} // end if statement

			// Test the player's removing of all their allies
			if ( Input.GetKeyDown( KeyCode.P ) )
			{
				print( "Removing all allies" );
				m_playerCharScript.RemoveAllAllies( true );
			} // end if statement
		} // end Update function
	} // end Test class
}// end namespace
