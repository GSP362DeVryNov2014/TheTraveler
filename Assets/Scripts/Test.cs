using UnityEngine;
using System;
using System.Collections;
using GSP.Char;

namespace GSP
{
	public class Test : MonoBehaviour
	{
		// Declare our private variables.
		GameObject m_player;				// This is the player game object.
		Character m_playerCharScript;		// This is the character script on the player.
		ResourceList m_playerResScript;		// This is the resource list script on the player.
		Ally m_playerAllyScript;			// This is the ally script on the player.

		// Use this for initialisation
		void Start()
		{
			// Get the player and its scripts attached.
			m_player = GameObject.FindGameObjectWithTag( "Player" );
			m_playerCharScript = m_player.GetComponent<Character>();
			m_playerAllyScript = m_player.GetComponent<Ally>();
			m_playerResScript = m_player.GetComponent<ResourceList>();
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
				m_playerCharScript.Currency += 10;
			} // end if statement

			// Test the player's removing currency.
			if ( Input.GetKeyDown( KeyCode.C ) )
			{
				print( "Removing 10 currency" );
				m_playerCharScript.Currency -= 10;
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
				// Create a new resource.
				Resource ore = new Resource();

				// Turn the resource into an ore.
				ore.SetResource(ResourceType.ORE.ToString());

				print( "Digging up an ore" );
				m_playerCharScript.PickupResource( ore, 1 );
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

			// Test the player's adding of an ally.
			if ( Input.GetKeyDown( KeyCode.M ) )
			{
				// Instantiate a copy of the character prefab.
				print( "Creating an ally");
				GameObject ally = Instantiate( PrefabReference.prefabCharacter ) as GameObject;

				// Change its name and give it the ally tag.
				ally.name = "Ally" + m_playerCharScript.NumAllies;
				ally.tag = "Ally";

				print( "Adding an ally" );
				m_playerAllyScript.AddAlly( ally );

				// Get the sprite renderer of the ally.
				SpriteRenderer sprRender = ally.GetComponent<SpriteRenderer>();
				// Tint the colour of the ally black to signify it has been added.
				// This is ony for this test.
				sprRender.color = Color.black;
			} // end if statement

			// Test the player's removing of an ally.
			if ( Input.GetKeyDown( KeyCode.N ) )
			{
				print( "Removing an ally" );

				if ( m_playerCharScript.NumAllies > 0 )
				{
					GameObject ally = m_playerAllyScript[m_playerCharScript.NumAllies - 1];

					// Get the sprite renderer of the ally.
					SpriteRenderer sprRender = ally.GetComponent<SpriteRenderer>();
					// Remove the colour tint to signify it's no longer an ally.
					// This is only for this tests.
					sprRender.color = Color.white;

					m_playerAllyScript.RemoveAlly( ally );
				} // end if statement
			} // end if statement

			// Test the player's removing of all their allies.
			if ( Input.GetKeyDown( KeyCode.O ) )
			{
				GameObject ally;

				print( "Clearing the ally list" );

				// Loop over the ally list.
				for (int index = 0; index < m_playerAllyScript.NumAllies; index++)
				{
					// Get the current ally.
					ally = m_playerAllyScript[index];
					
					// Get the sprite renderer of the ally.
					SpriteRenderer sprRender = ally.GetComponent<SpriteRenderer>();
					// Remove the colour tint to signify it's no longer an ally.
					// This is only for these tests.
					sprRender.color = Color.white;
				} // end for loop

				m_playerAllyScript.ClearAllyList();
			} // end if statement

			// Test the value of the player's attack.
			if( Input.GetKeyDown( KeyCode.P ) )
			{
				print ( "The player's attack value is " + m_playerCharScript.AttackPower );
			} // end if statement
		
			// Test the player's adding of attack value.
			if( Input.GetKeyDown(KeyCode.Q ) )
			{
				print ( "Adding 10 attack" );
				m_playerCharScript.AttackPower += 10;
			} // end if statement

			// Test the player's removing of attack value.
			if( Input.GetKeyDown( KeyCode.R ) )
			{
				print ( "Removing 10 attack." );
				m_playerCharScript.AttackPower -= 10;
			} // end if statement

			// Test the value of the player's defence.
			if( Input.GetKeyDown( KeyCode.S ) )
			{
				print ( "The player's defence value is " + m_playerCharScript.DefencePower );
			} // end if statement

			// Test the player's adding of defence value.
			if( Input.GetKeyDown( KeyCode.T ) )
			{
				print ( "Adding 10 defence." );
				m_playerCharScript.DefencePower += 10;
			} // end if statement

			// Test the player's removing of defence value.
			if( Input.GetKeyDown( KeyCode.U ) )
			{
				print ( "Removing 10 defence." );
				m_playerCharScript.DefencePower -= 10;
			} // end if statement

			// These are the new tests.
			// The number keys above the letters are used.
			#region New Tests

			// Test the creation and removal of an ally.
			// The coroutine is only for showing the ally on the screen before its removal.
			if ( Input.GetKeyDown( KeyCode.Alpha1 ) )
			{
				// Instantiate a copy of the character prefab.
				print( "Creating an ally" );
				GameObject newAlly = Instantiate( PrefabReference.prefabCharacter, new Vector3( -6.0f, 2.0f, 0.0f ), new Quaternion() ) as GameObject;

				// Change the ally's name.
				newAlly.name = "Ally" + m_playerCharScript.NumAllies;
				print( "Ally name changed to: " + newAlly.name );

				// Change the ally's tag.
				newAlly.tag = "Ally";
				print( "Ally tagged as: " + newAlly.tag );
				
				// Add the ally to the character this is attached to's ally list.
				print( "Adding an ally to the list" );
				m_playerAllyScript.AddAlly( newAlly );

				// Display the ally count.
				print( "Ally count is: " + m_playerCharScript.NumAllies );

				// Get the sprite renderer of the ally.
				SpriteRenderer sprRender = newAlly.GetComponent<SpriteRenderer>();
				// Tint the colour of the ally black to signify it has been added.
				// This is ony for this test.
				sprRender.color = Color.black;

				// Coroutine to remove the ally from the list after 3 seconds.
				StartCoroutine( "DelayedRemoveSingle", newAlly );

				// Display the ally count.
				print( "Ally count is now: " + m_playerCharScript.NumAllies );
			}

			// Test the creation and removal of many allies using ClearAllyList.
			// The coroutine is only for showing the ally on the screen before its removal.
			if ( Input.GetKeyDown( KeyCode.Alpha2 ) )
			{
				// The starting values for displaying all on the screen.
				float x = -6.0f;

				// Create five allies.
				for (int index = 0; index < 5; index++)
				{
					// Instantiate a copy of the character prefab.
					print( "Creating allies" );
					GameObject newAlly = Instantiate( PrefabReference.prefabCharacter, new Vector3( x, 2.0f, 0.0f ), new Quaternion() ) as GameObject;

					// Add 3 to the x value
					x += 3.0f;
					
					// Change the ally's name.
					newAlly.name = "Ally" + m_playerCharScript.NumAllies;
					print( "Ally name changed to: " + newAlly.name );
					
					// Change the ally's tag.
					newAlly.tag = "Ally";
					print( "Ally tagged as: " + newAlly.tag );
					
					// Add the ally to the character this is attached to's ally list.
					print( "Adding an ally to the list" );
					m_playerAllyScript.AddAlly( newAlly );

					// Get the sprite renderer of the ally.
					SpriteRenderer sprRender = newAlly.GetComponent<SpriteRenderer>();
					// Tint the colour of the ally black to signify it has been added.
					// This is ony for this test.
					sprRender.color = Color.black;
					
					// Display the ally count.
					print( "Ally count is now: " + m_playerCharScript.NumAllies );

					// Get the index of the ally.
					print( "Ally index:" + m_playerAllyScript.GetIndex( newAlly ) );
				} // end for statement
				
				// Coroutine to remove the allies after 3 seconds.
				StartCoroutine( "DelayedRemoveMultiple" );
			}

			// Test the creation and removal of many allies using ClearAllyList.
			// The coroutine is only for showing the ally on the screen before its removal.
			if ( Input.GetKeyDown( KeyCode.Alpha3 ) )
			{
				// The starting values for displaying all on the screen.
				float x = -6.0f;
				
				// Create five allies.
				for (int index = 0; index < 5; index++)
				{
					// Instantiate a copy of the character prefab.
					print( "Creating allies" );
					GameObject newAlly = Instantiate( PrefabReference.prefabCharacter, new Vector3( x, 2.0f, 0.0f ), new Quaternion() ) as GameObject;
					
					// Add 3 to the x value
					x += 3.0f;
					
					// Change the ally's name.
					newAlly.name = "Ally" + m_playerCharScript.NumAllies;
					print( "Ally name changed to: " + newAlly.name );
					
					// Change the ally's tag.
					newAlly.tag = "Ally";
					print( "Ally tagged as: " + newAlly.tag );
					
					// Add the ally to the character this is attached to's ally list.
					print( "Adding an ally to the list" );
					m_playerAllyScript.AddAlly( newAlly );
					
					// Get the sprite renderer of the ally.
					SpriteRenderer sprRender = newAlly.GetComponent<SpriteRenderer>();
					// Tint the colour of the ally black to signify it has been added.
					// This is ony for this test.
					sprRender.color = Color.black;
					
					// Display the ally count.
					print( "Ally count is now: " + m_playerCharScript.NumAllies );
					
					// Get the index of the ally.
					print( "Ally index:" + m_playerAllyScript.GetIndex( newAlly ) );
				} // end for statement
				
				print( "Removing the middle ally." );

				// Get the middle ally. Using the object version here as the index version doesn't seem to work well.
				// It's still easy to get the object via index.
				GameObject ally = m_playerAllyScript[2];

				// Coroutine to remove the ally after 3 seconds.
				StartCoroutine( "DelayedRemoveSingle", ally);
			}

			// Test the creation and removal of many allies using ClearAllyList.
			// The coroutine is only for showing the ally on the screen before its removal.
			// This is the index version
			if ( Input.GetKeyDown( KeyCode.Alpha4 ) )
			{
				// The starting values for displaying all on the screen.
				float x = -6.0f;
				
				// Create five allies.
				for (int index = 0; index < 5; index++)
				{
					// Instantiate a copy of the character prefab.
					print( "Creating allies" );
					GameObject newAlly = Instantiate( PrefabReference.prefabCharacter, new Vector3( x, 2.0f, 0.0f ), new Quaternion() ) as GameObject;
					
					// Add 3 to the x value
					x += 3.0f;
					
					// Change the ally's name.
					newAlly.name = "Ally" + m_playerCharScript.NumAllies;
					print( "Ally name changed to: " + newAlly.name );
					
					// Change the ally's tag.
					newAlly.tag = "Ally";
					print( "Ally tagged as: " + newAlly.tag );
					
					// Add the ally to the character this is attached to's ally list.
					print( "Adding an ally to the list" );
					m_playerAllyScript.AddAlly( newAlly );
					
					// Get the sprite renderer of the ally.
					SpriteRenderer sprRender = newAlly.GetComponent<SpriteRenderer>();
					// Tint the colour of the ally black to signify it has been added.
					// This is ony for this test.
					sprRender.color = Color.black;
					
					// Display the ally count.
					print( "Ally count is now: " + m_playerCharScript.NumAllies );
					
					// Get the index of the ally.
					print( "Ally index:" + m_playerAllyScript.GetIndex( newAlly ) );
				} // end for statement
				
				print( "Removing the middle ally." );
				
				// Coroutine to remove the ally after 3 seconds.
				StartCoroutine( "DelayedRemoveSingleInt", 2);
			}

			// Test for transfering currency from an ally.
			if ( Input.GetKeyDown( KeyCode.Alpha5 ) )
			{
				// Instantiate a copy of the character prefab.
				print( "Creating an ally" );
				GameObject newAlly = Instantiate( PrefabReference.prefabCharacter, new Vector3( -6.0f, 2.0f, 0.0f ), new Quaternion() ) as GameObject;
				
				// Change the ally's name.
				newAlly.name = "Ally" + m_playerCharScript.NumAllies;
				print( "Ally name changed to: " + newAlly.name );
				
				// Change the ally's tag.
				newAlly.tag = "Ally";
				print( "Ally tagged as: " + newAlly.tag );
				
				// Add the ally to the character this is attached to's ally list.
				print( "Adding an ally to the list" );
				m_playerAllyScript.AddAlly( newAlly );
				
				// Display the ally count.
				print( "Ally count is: " + m_playerCharScript.NumAllies );
				
				// Get the sprite renderer of the ally.
				SpriteRenderer sprRender = newAlly.GetComponent<SpriteRenderer>();
				// Tint the colour of the ally black to signify it has been added.
				// This is ony for this test.
				sprRender.color = Color.black;

				// Get ally's character script.
				Character allyCharScript = m_playerAllyScript[0].gameObject.GetComponent<Character>();

				// Give the ally some currency.
				allyCharScript.Currency += 100;
				print( "Ally now has " + allyCharScript.Currency + " currency" );

				// Give the currency to the player.
				print( "Player has " + m_playerCharScript.Currency + " currency" );
				allyCharScript.TransferCurrency(m_player, 100);
				print( "Player now has " + m_playerCharScript.Currency + " currency" );
				print( "Ally now has " + allyCharScript.Currency + " currency" );

				// Give non-existent currency to the player. Nothing should happen here.
				allyCharScript.TransferCurrency(m_player, 100);
				print( "Player now has " + m_playerCharScript.Currency + " currency" );
				print( "Ally now has " + allyCharScript.Currency + " currency" );
			}

			// Test for transfering a resource from an ally.
			if ( Input.GetKeyDown( KeyCode.Alpha6 ) )
			{
				// Instantiate a copy of the character prefab.
				print( "Creating an ally" );
				GameObject newAlly = Instantiate( PrefabReference.prefabCharacter, new Vector3( -6.0f, 2.0f, 0.0f ), new Quaternion() ) as GameObject;
				
				// Change the ally's name.
				newAlly.name = "Ally" + m_playerCharScript.NumAllies;
				print( "Ally name changed to: " + newAlly.name );
				
				// Change the ally's tag.
				newAlly.tag = "Ally";
				print( "Ally tagged as: " + newAlly.tag );
				
				// Add the ally to the character this is attached to's ally list.
				print( "Adding an ally to the list" );
				m_playerAllyScript.AddAlly( newAlly );
				
				// Display the ally count.
				print( "Ally count is: " + m_playerCharScript.NumAllies );
				
				// Get the sprite renderer of the ally.
				SpriteRenderer sprRender = newAlly.GetComponent<SpriteRenderer>();
				// Tint the colour of the ally black to signify it has been added.
				// This is ony for this test.
				sprRender.color = Color.black;
				
				// Get ally's resource list script.
				ResourceList allyResScript = m_playerAllyScript[0].GetComponent<ResourceList>();

				// Create a resource.
				Resource resource = new Resource();

				// Set it to an ore.
				resource.SetResource(ResourceType.ORE.ToString());

				// Give the player the resource.
				print( "Player has " + m_playerResScript.NumResources + " resources" );
				m_playerResScript.AddResource(resource, 1);
				print( "Giving the player an ore" );
				print( "Player now has " + m_playerResScript.NumResources + " resources" );

				// We need to delay the transfer as it's executing too quickly in this test.
				// Start its coroutine.
				StartCoroutine( DelayedTransfer( m_playerAllyScript[0].gameObject, m_playerResScript.GetResourceByIndex(0), allyResScript ) );
			}

			#endregion
		} // end Update function

		// Coroutine for delayed removal of a single ally.
		IEnumerator DelayedRemoveSingle(GameObject ally)
		{
			// Wait for 3 seconds.
			yield return new WaitForSeconds( 3.0f );

			// Remove the ally from list.
			print( "Removing the ally from the list" );
			m_playerAllyScript.RemoveAlly( ally );

			// Get the sprite renderer of the ally.
			SpriteRenderer sprRender = ally.GetComponent<SpriteRenderer>();
			// Remove the colour tint to signify it's no longer an ally.
			// This is only for these tests.
			sprRender.color = Color.white;
		}

		// Coroutine for delayed removal of a single ally via index.
		IEnumerator DelayedRemoveSingleInt(int index)
		{
			// Wait for 3 seconds.
			yield return new WaitForSeconds( 3.0f );

			// Get the sprite renderer of the ally.
			SpriteRenderer sprRender = m_playerAllyScript.GetObject(index).GetComponent<SpriteRenderer>();

			// Remove the ally from list.
			print( "Removing the ally from the list" );
			m_playerAllyScript.RemoveAlly( index );

			// Remove the colour tint to signify it's no longer an ally.
			// This is only for these tests.
			sprRender.color = Color.white;
		}

		// Coroutine for delayed removal of allies.
		IEnumerator DelayedRemoveMultiple()
		{
			GameObject ally;

			// Wait for 3 seconds.
			yield return new WaitForSeconds( 3.0f );
			
			// Loop over the ally list.
			for (int index = 0; index < m_playerAllyScript.NumAllies; index++)
			{
				// Get the current ally.
				ally = m_playerAllyScript[index];

				// Get the sprite renderer of the ally.
				SpriteRenderer sprRender = ally.GetComponent<SpriteRenderer>();
				// Remove the colour tint to signify it's no longer an ally.
				// This is only for these tests.
				sprRender.color = Color.white;
			} // end for loop

			// Remove the allies from list and destroy it.
			print( "Removing the allies from the list" );
			m_playerAllyScript.ClearAllyList();

			// Display the ally count.
			print( "Ally count is now: " + m_playerCharScript.NumAllies );
		}

		// Coroutine for delayed transfer of resource.
		IEnumerator DelayedTransfer( GameObject other, Resource resource, ResourceList resScript )
		{
			// Wait for 3 seconds.
			yield return new WaitForSeconds( 3.0f );
			
			// Transfer the resource to the ally.
			m_playerCharScript.TransferResource( other, resource );
			print( "Transferring the rock to the ally" );
			print( "Player now has " + m_playerResScript.NumResources + " resources" );
			print( "Ally now has " + resScript.NumResources + " resources" );
		}
	} // end Test class
}// end namespace
