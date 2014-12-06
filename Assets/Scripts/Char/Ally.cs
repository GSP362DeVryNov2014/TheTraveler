using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GSP.Char
{
	public class Ally : MonoBehaviour
	{
		List<GameObject> m_allyList;		//Holds list of allies
		int maxAllies;						//Limit on number of allies

		// Use this for initialisation
		void Start()
		{
			// Initialise our list here.
			m_allyList = new List<GameObject>();
			//TODO: Adjust this back to 1 when creating game for realistic play
			maxAllies = 100;
		}
		
		// Update is called once per frame
		void Update()
		{
			// Nothing to do here at the moment.
		}

		// Get the ally by its index. This should stay readonly (get only).
		public GameObject this[int index]
		{
			get { Debug.LogError("Index: " + index); return m_allyList [index]; }
		}

		// Return ally object
		public GameObject GetObject( int index )
		{
			return m_allyList[index];
		}

		// Get the index of the ally.
		public int GetIndex( GameObject ally )
		{
			return m_allyList.IndexOf( ally );
		}

		// Gets the number of allies the character has.
		// Also sets the max number they can have.
		public int NumAllies
		{
			get { return m_allyList.Count; }
			set 
			{ 
				//Apply value
				maxAllies = value;

				//Clamp allyLimit to zero
				if(maxAllies < 0)
				{
					maxAllies = 0;
				} //end if
			} //end Set
		} // end NumAllies function

		// Adds an ally to the list.
		public void AddAlly( GameObject ally )
		{
			if(NumAllies != maxAllies)
			{
				m_allyList.Add( ally );
			} //end if
			else
			{
				print ("Ally limit reached. Add denied.");
			} //end else
		} // end AddAlly function

		// Removes an ally from the list by its object.
		public void RemoveAlly( GameObject ally )
		{
			m_allyList.Remove( ally );
		} // end RemoveAlly function

		// Removes an ally from the list by its index.
		// ISSUE: Doesn't seem to work all that well.
		public void RemoveAlly( int index )
		{
			int temp = NumAllies;
			m_allyList.RemoveAt( index );
			print ("Old count " + temp + " New count " + NumAllies);
		} // end RemoveAlly function

		// Clear the ally list of the character.
		public void ClearAllyList()
		{
		} // end ClearAllyList function.
	}
}
