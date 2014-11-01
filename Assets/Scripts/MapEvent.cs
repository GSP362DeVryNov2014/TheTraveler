using UnityEngine;
using System;
using System.Collections;

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
				print ("Map Event is " + Enum.GetName (typeof(normalTile), dieResult));
			} //end if
			else if (Input.GetKeyDown (KeyCode.R)) 
			{
				int dieResult = m_die.Roll (1, (int)resourceTile.SIZE) - 1;
				print ("Map Event is " + Enum.GetName (typeof(resourceTile), dieResult));
			} //end else if
		} //end Update()
	} //end MapEvent class
} //end namespace GSP
