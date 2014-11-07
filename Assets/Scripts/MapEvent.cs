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
		//NOTE!!!
		//SIZE must be the last item in the enum, this allows for functions refrencing the number
		//of items in the enum to automatically adjust to any new items.

		//Holds list of normal tile events
		enum normalTile {ENEMY, ALLY, ITEM, WEATHER, NOTHING, SIZE};

		//Holds list of resource tile events
		enum resourceTile {WOOL, WOOD, FISH, ORE, SIZE};

		//Calls map event, which will eventually have the resulting effects
		void Update()
		{
			if (Input.GetKeyDown (KeyCode.N)) 
			{
				int dieResult = m_die.Roll (1, (int)normalTile.SIZE) - 1;
				if(Enum.GetName (typeof(normalTile), dieResult) == "ENEMY")
				{
					//Declare what was landed on
					print("Map Event is ENEMY");

					//Refrence for enemy graphic
					GameObject m_charRef = GameObject.FindGameObjectWithTag( "PrefabReferenceHolder" );
					PrefabReference m_prefabRefScript = m_charRef.GetComponent<PrefabReference>();

					// Get the enemy and the character script attached.
					GameObject m_enemy = GameObject.FindGameObjectWithTag( "Enemy" );
					Character m_enemyCharScript = m_enemy.GetComponent<Character>();

					//ERROR: Object reference not set to an instance of an object
					GameObject enemy = Instantiate( m_prefabRefScript.prefabCharacter ) as GameObject;
					enemy.name = "Enemy";
					enemy.tag = "Enemy";
					m_enemyCharScript.AttackPower = m_die.Roll(1, 20);
					m_enemyCharScript.DefencePower = m_die.Roll(1, 20);
				} //end if
				else if (Enum.GetName (typeof(normalTile), dieResult) == "ALLY")
				{
					print("Map Event is ALLY");
				} //end else if
				else if(Enum.GetName (typeof(normalTile), dieResult) == "ITEM")
				{
					print ("Map Event is ITEM");
				} //end else if
				else if (Enum.GetName (typeof(normalTile), dieResult) == "WEATHER")
				{
					print ("Map Event is WEATHER");
				} //end else if
				else if(Enum.GetName (typeof(normalTile), dieResult) == "NOTHING")
				{
					print("Map Event is NOTHING");
				} //end else if
			} //end if
			else if (Input.GetKeyDown (KeyCode.R)) 
			{
				int dieResult = m_die.Roll (1, (int)resourceTile.SIZE) - 1;
				print ("Map Event is " + Enum.GetName (typeof(resourceTile), dieResult));
			} //end else if
		} //end Update()
	} //end MapEvent class
} //end namespace GSP
