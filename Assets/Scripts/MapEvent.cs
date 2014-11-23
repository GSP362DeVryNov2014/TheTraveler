using UnityEngine;
using System;
using System.Collections;
using GSP.Char;
using GSP.Tiles;

namespace GSP
{
	public class MapEvent : MonoBehaviour
	{
		//Holds object for refrencing die functions
		private Die m_die = new Die();

		//Holds the objects for referencing the player and its script functions.
		GameObject m_player;
		Character m_playerCharScript;

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

		//Calls map event and returns string
		public string DetermineEvent(GameObject player)
		{
			m_player = player;
			m_playerCharScript = m_player.GetComponent<Character>();
			////////////////////////////
			Vector3 tmp = m_player.transform.localPosition;
			tmp.z = 0.0f;
			Tile currentTile = TileDictionary.GetTile (TileManager.ToPixels (tmp));
			////////////////////////////

			//Tile currentTile = TileDictionary.GetTile (TileManager.ToPixels (m_player.transform.localPosition));
			//If no tile found
			if(currentTile == null)
			{
				return "This is not a valid \ntile. No event occured.";
			} //end if
			//If no resource at tile
			else if (currentTile.ResourceType == ResourceType.NONE) 
			{
				int dieResult = m_die.Roll (1, 100);
				if(dieResult < 21)
				{
					//Create the enemy
					//TODO Replace prefabCharacter with prefabEnemy
					GameObject m_enemy = Instantiate( PrefabReference.prefabCharacter,
					  new Vector3( 0.7f, 0.5f, 0.0f ), new Quaternion() ) as GameObject;
					
					//Get the character script attached to the enemy
					Character m_enemyScript = m_enemy.GetComponent<Character>();

					//Set stats
					m_enemyScript.AttackPower = m_die.Roll(1, 20);
					m_enemyScript.DefencePower = m_die.Roll(1, 20);

					//TODO Add Battle Scene

					//Battle characters
					Fight fighter = new Fight();
					string result = "Map event was enemy, \nand " + fighter.CharacterFight(m_enemy, m_player);
					Destroy(m_enemy);
					return result;
				} //end if ENEMY
				else if (dieResult < 46 && dieResult >= 21)
				{
					//Create ally
					GameObject m_ally = Instantiate( PrefabReference.prefabCharacter,
						m_player.transform.position, new Quaternion() ) as GameObject;

					//Generate script
					Character m_allyScript = m_ally.GetComponent<Character>();

					//Set max weight
					m_allyScript.MaxWeight = m_die.Roll(1, 20) * 6;

					print ("Do you want this ally? \nHit y for yes \nand n for no.");
					bool Loop = true;
					while(Loop)
					{
						if(Input.GetKeyDown(KeyCode.Y))
						{
							//Add to ally list
							Ally m_playerAllyScript = m_player.GetComponent<Ally>();
							m_playerAllyScript.AddAlly(m_ally);
							Loop = false;
						}
						else if(Input.GetKeyDown(KeyCode.N))
						{
							//Disable newAlly and end loop
							Destroy(m_ally);
							Loop = false;
						} //end else if
					} //end while
					return "Map event was ally.";
				} //end else if ALLY
				else if(dieResult < 66 && dieResult >= 46)
				{
					//String to return for display
					string result;

					//Determine what item was found
					int itemType = m_die.Roll(1, 4);

					if(itemType == 1)
					{
						//Pick an item from the weapons enum
						int itemNumber = m_die.Roll(1, (int)Weapons.SIZE) - 1;

						//Assign chosen number as the item
						result = Enum.GetName(typeof(Weapons), itemNumber);

						//Equip to player
						m_playerCharScript.EquipItem(result);

					} //end if Weapon
					else if(itemType == 2)
					{
						//Pick an item from the armor enum
						int itemNumber = m_die.Roll(1, (int)Armor.SIZE) - 1;
						
						//Assign chosen number as the item
						result = Enum.GetName(typeof(Armor), itemNumber);

						//Equip to player
						m_playerCharScript.EquipItem(result);
					} //end else if Armor
					else if(itemType == 3)
					{
						//Pick an item from the inventory enum
						int itemNumber = m_die.Roll(1, (int)Inventory.SIZE) - 1;
						
						//Assign chosen number as the item
						result = Enum.GetName(typeof(Inventory), itemNumber);

						//Equip to player
						m_playerCharScript.EquipItem(result);
					} //end else if Inventory
					else if(itemType == 4)
					{
						//Pick an item from the weight enum
						int itemNumber = m_die.Roll(1, (int)Weight.SIZE) - 1;

						//Assign chosen number as the item
						result = Enum.GetName(typeof(Weight), itemNumber);

						//Equip to player
						m_playerCharScript.EquipItem(result);
					} //end else if Weight
					else
					{
						result = "non-existant item. Nothing given.";
					} //end else

					return "Map event was item \nand you got " + result;
				} //end else if ITEM
				else
				{
					return "No map event occured.";
				} //end else if NOTHING
			} //end if NORMAL TILE
			//If resource at tile
			else
			{
				//Create temp resource
				Resource temp = new Resource();

				// Turn temp into type of resource
				temp.SetResource(currentTile.ResourceType.ToString());

				//Pick up resource
				m_playerCharScript.PickupResource( temp, 1 );

				//Declare what was landed on
				return "Map Event is " + temp.ResourceName;
			} //end else if RESOURCE TILE
		} //end DetermineEvent()
	} //end MapEvent class
} //end namespace GSP
