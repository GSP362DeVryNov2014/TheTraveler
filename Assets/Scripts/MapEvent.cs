using UnityEngine;
using System;
using System.Collections;
using GSP.Char;

namespace GSP
{
	public class MapEvent : MonoBehaviour
	{
		//Holds object for refrencing die functions
		private Die m_die = new Die();

		//NOTE!!
		//SIZE must be the last item in the enum so that anything based
		//on the length of the enum can be used as normal. It is best to
		//add items to the left of SIZE but after the current 2nd to last
		//item in the enum. For instance if the list was {ENEMY, ALLY, SIZE}
		//you should enter the new item between ALLY and SIZE.

		//Holds list of normal tile events
		enum normalTile {ENEMY, ALLY, ITEM, WEATHER, NOTHING, SIZE};

		//Holds list of resource tile events
		enum resourceTile {WOOL, WOOD, FISH, ORE, SIZE};

		//Calls map event, which needs to have access to player functions
		void Update()
		{
			// Get the player and the character script attached.
			GameObject m_player = GameObject.FindGameObjectWithTag( "Player" );
			Character m_playerCharScript = m_player.GetComponent<Character>();

			//This will be replaced with the normal tile trigger
			if (Input.GetKeyDown (KeyCode.N)) 
			{
				//TODO: Skew roll so outcomes have non-equal percentages.
				int dieResult = m_die.Roll (1, (int)normalTile.SIZE) - 1;
				if(Enum.GetName (typeof(normalTile), dieResult) == "ENEMY")
				{
					//Declare what was landed on
					print("Map Event is ENEMY");

					//Refrence for enemy graphic
					GameObject m_charRef = GameObject.FindGameObjectWithTag( "PrefabReferenceHolder" );
					PrefabReference m_prefabRefScript = m_charRef.GetComponent<PrefabReference>();

					// Get the enemy and the character script attached.
					Character m_enemyScript = m_player.GetComponent<Character>();
					GameObject enemy = Instantiate( m_prefabRefScript.prefabCharacter, 
						new Vector3( 0.7f, 0.5f, 0.0f ), new Quaternion() ) as GameObject;

					//Name and tag
					enemy.name = "Enemy";
					enemy.tag = "Enemy";

					//Set stats
					m_enemyScript.AttackPower = m_die.Roll(1, 20);
					m_enemyScript.DefencePower = m_die.Roll(1, 20);

					//TODO: Make fight function and feed this enemy as the target
				} //end if ENEMY
				else if (Enum.GetName (typeof(normalTile), dieResult) == "ALLY")
				{
					//Declare what was landed on
					print("Map Event is ALLY");

					//Refrence for ally graphic
					GameObject m_charRef = GameObject.FindGameObjectWithTag( "PrefabReferenceHolder" );
					PrefabReference m_prefabRefScript = m_charRef.GetComponent<PrefabReference>();

					//Set up ally script and instantiate
					Ally m_playerAllyScript = m_player.GetComponent<Ally>();
					GameObject newAlly = Instantiate( m_prefabRefScript.prefabCharacter, 
						new Vector3( -6.0f, 2.0f, 0.0f ), new Quaternion() ) as GameObject;

					//Name and tag ally
					newAlly.name = "Ally" + m_playerCharScript.NumAllies;
					newAlly.tag = "Ally";

					//TODO: Have ally able to receive random weight bonus

					//Add to ally list
					m_playerAllyScript.AddAlly( newAlly );

					//TODO: Have player able to refuse ally, and check if ally
					//list if full
				} //end else if ALLY
				else if(Enum.GetName (typeof(normalTile), dieResult) == "ITEM")
				{
					//Declare what was landed on
					print ("Map Event is ITEM");

					//Set up Item script and graphics
					//TODO: Item graphics and code
					Items m_playerItem = m_player.GetComponent<Items>();

					//Determine what item was found
					int itemType = m_die.Roll(1, 4);

					if(itemType == 1)
					{
						//Pick an item from the weapons enum
						int itemNumber = m_die.Roll(1, (int)Weapons.SIZE) - 1;

						//Assign chosen number as the item
						m_playerItem.SetItem(Enum.GetName(typeof(Weapons), itemNumber));

						//TODO: Allow player to accept or refuse item.
						//If accepted, assign item to player through EquipItem
					} //end if Weapon
					else if(itemType == 2)
					{
						//Pick an item from the armor enum
						int itemNumber = m_die.Roll(1, (int)Armor.SIZE) - 1;
						
						//Assign chosen number as the item
						//ERROR: Object reference not set to an instance of an object
						m_playerItem.SetItem(Enum.GetName(typeof(Armor), itemNumber));
						
						//TODO: Allow player to accept or refuse item.
						//If accepted, assign item to player through EquipItem
					} //end else if Armor
					else if(itemType == 3)
					{
						//Pick an item from the inventory enum
						int itemNumber = m_die.Roll(1, (int)Inventory.SIZE) - 1;
						
						//Assign chosen number as the item
						m_playerItem.SetItem(Enum.GetName(typeof(Inventory), itemNumber));
						
						//TODO: Allow player to accept or refuse item.
						//If accepted, assign item to player through EquipItem
					} //end else if Inventory
					else if(itemType == 4)
					{
						//Pick an item from the weight enum
						int itemNumber = m_die.Roll(1, (int)Weight.SIZE) - 1;
						
						//Assign chosen number as the item
						//ERROR: Object reference not set to instance of an object
						m_playerItem.SetItem(Enum.GetName(typeof(Weight), itemNumber));
						
						//TODO: Allow player to accept or refuse item.
						//If accepted, assign item to player through EquipItem
					} //end else if Weight
				} //end else if ITEM
				else if (Enum.GetName (typeof(normalTile), dieResult) == "WEATHER")
				{
					print ("Map Event is WEATHER");

					//TODO: Create weather effect variable for each map, then refrence it here
				} //end else if WEATHER
				else if(Enum.GetName (typeof(normalTile), dieResult) == "NOTHING")
				{
					print("Map Event is NOTHING");
				} //end else if NOTHING
			} //end if NORMAL TILE
			//This will be replaced by the resource tile trigger
			else if (Input.GetKeyDown (KeyCode.R)) 
			{
				int dieResult = m_die.Roll (1, (int)resourceTile.SIZE) - 1;
				print ("Map Event is " + Enum.GetName (typeof(resourceTile), dieResult));

				//TODO: Still waiting for defined resources.
			} //end else if RESOURCE TILE
		} //end Update()
	} //end MapEvent class
} //end namespace GSP
