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

		//Holds the objects for referencing the player and its script functions.
		GameObject m_player;
		Character m_playerCharScript;
		Ally m_playerAllyScript;

		//Holds the objects for referencing prefabs
		GameObject m_charRef;
		PrefabReference m_prefabRefScript;

		//Holds the object for referencing the player's item script functions
		Items m_playerItem;

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

		// Use this for initialization
		void Start()
		{
			// Get the player, the character script, and ally script attached.
			m_player = GameObject.FindGameObjectWithTag( "Player" );
			m_playerCharScript = m_player.GetComponent<Character>();
			m_playerAllyScript = m_player.GetComponent<Ally>();

			// Get the prefab reference holder and its script.
			m_charRef = GameObject.FindGameObjectWithTag( "PrefabReferenceHolder" );
			m_prefabRefScript = m_charRef.GetComponent<PrefabReference>();
		} //end Start()

		//Calls map event, which needs to have access to player functions
		void Update()
		{
			//This will be replaced with the normal tile trigger
			if (Input.GetKeyDown (KeyCode.N)) 
			{
				int dieResult = m_die.Roll (1, 100);
				if(dieResult < 36)
				{
					//Declare what was landed on
					print("Map Event is ENEMY");

					//Create the enemy
					GameObject enemy = Instantiate( PrefabReference.prefabCharacter,
					  new Vector3( 0.7f, 0.5f, 0.0f ), new Quaternion() ) as GameObject;

					//Name and tag
					enemy.name = "Enemy";
					enemy.tag = "Enemy";

					//Get the character script attached to the enemy
					Character m_enemyScript = enemy.GetComponent<Character>();

					//Set stats
					m_enemyScript.AttackPower = m_die.Roll(1, 20);
					m_enemyScript.DefencePower = m_die.Roll(1, 20);

					//TODO: Make fight function and feed this enemy as the target
				} //end if ENEMY
				else if (dieResult < 51 && dieResult >= 36)
				{
					//Declare what was landed on
					print("Map Event is ALLY");

					//Instantiate the ally
					GameObject newAlly = Instantiate( PrefabReference.prefabCharacter,
					  new Vector3( -6.0f, 2.0f, 0.0f ), new Quaternion() ) as GameObject;

					//Name and tag ally
					newAlly.name = "Ally" + m_playerCharScript.NumAllies;
					newAlly.tag = "Ally";

					//Get the character script attached to the ally
					//Uncomment the following line once the weight bonus thing is implemented and the script is used.
					//Ally m_allyScript = newAlly.GetComponent<Ally>();

					//TODO: Have ally able to receive random weight bonus

					print ("Do you want this ally? Hit y for yes and n for no.");
					bool Loop = true;
					while(Loop)
					{
						if(Input.GetKeyDown(KeyCode.Y))
						{
							//Add to ally list
							m_playerAllyScript.AddAlly( newAlly );
							Loop = false;
						}
						else if(Input.GetKeyDown(KeyCode.N))
						{
							//Disable newAlly and end loop
							newAlly = null;
							Loop = false;
						} //end else if
					} //end while
				} //end else if ALLY
				else if(dieResult < 61 && dieResult >= 51)
				{
					//Declare what was landed on
					print ("Map Event is ITEM");

					//TODO: Item graphics and code

					//Determine what item was found
					int itemType = m_die.Roll(1, 4);

					//TODO: ERROR: You are trying to create a MonoBehavior using the 'new'
					//keyword. This is not allowed. MonoBehaviors can only be added
					//using AddComponent(). Alternatively, your script can inherit
					//from ScriptableObject or no base class at all.
					if(itemType == 1)
					{
						//Pick an item from the weapons enum
						int itemNumber = m_die.Roll(1, (int)Weapons.SIZE) - 1;

						//Assign chosen number as the item
						string itemName = Enum.GetName(typeof(Weapons), itemNumber);

						//Equip to player
						m_playerCharScript.EquipItem(itemName);

					} //end if Weapon
					else if(itemType == 2)
					{
						//Pick an item from the armor enum
						int itemNumber = m_die.Roll(1, (int)Armor.SIZE) - 1;
						
						//Assign chosen number as the item
						string itemName = Enum.GetName(typeof(Armor), itemNumber);
						
						//Equip to player
						m_playerCharScript.EquipItem(itemName);
					} //end else if Armor
					else if(itemType == 3)
					{
						//Pick an item from the inventory enum
						int itemNumber = m_die.Roll(1, (int)Inventory.SIZE) - 1;
						
						//Assign chosen number as the item
						string itemName = Enum.GetName(typeof(Inventory), itemNumber);
						
						//Equip to player
						m_playerCharScript.EquipItem(itemName);
					} //end else if Inventory
					else if(itemType == 4)
					{
						//Pick an item from the weight enum
						int itemNumber = m_die.Roll(1, (int)Weight.SIZE) - 1;

						//Assign chosen number as the item
						string itemName = Enum.GetName(typeof(Weight), itemNumber);
						
						//Equip to player
						m_playerCharScript.EquipItem(itemName);
					} //end else if Weight
				} //end else if ITEM
				else if (dieResult < 71 && dieResult >= 61)
				{
					print ("Map Event is WEATHER");

					//TODO: Create weather effect variable for each map, then refrence it here
				} //end else if WEATHER
				else
				{
					print("Map Event is NOTHING ");
				} //end else if NOTHING
			} //end if NORMAL TILE
			//This will be replaced by the resource tile trigger
			else if (Input.GetKeyDown (KeyCode.R)) 
			{
				//TODO: Figure out how to feed tile resource type into this.
				//Create temp resource
				Resource temp = new Resource();

				// Turn the resource into an ore.
				temp.SetResource(ResourceType.ORE.ToString());

				//Pick up ore
				m_playerCharScript.PickupResource( temp, 1 );

				//Declare what was landed on
				print ("Map Event is " + temp.ResourceName);
			} //end else if RESOURCE TILE
		} //end Update()
	} //end MapEvent class
} //end namespace GSP
