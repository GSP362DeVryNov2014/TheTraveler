using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GSP.Char
{
	public class Ally : MonoBehaviour
	{
		List<GameObject> m_allyList;

		// Use this for initialization
		void Start()
		{
			// Initialise our list here.
			m_allyList = new List<GameObject>();
		}
		
		// Update is called once per frame
		void Update()
		{
			// Nothing to do here at the moment.
		}

		// Get the ally by its index. This should stay readonly (get only).
		public GameObject this[int index]
		{
			get { return m_allyList[index]; }
		}

		// Get the index of the ally.
		public int GetIndex( GameObject ally )
		{
			return m_allyList.IndexOf( ally );
		}

		// Gets the number of allies the character has.
		public int NumAllies
		{
			get { return m_allyList.Count; }
		} // end NumAllies function

		// Adds an ally to the list.
		public void AddAlly( GameObject ally )
		{
			m_allyList.Add( ally );
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
			m_allyList.RemoveAt( index );
		} // end RemoveAlly function

		// Clear the ally list of the character.
		public void ClearAllyList()
		{
		} // end ClearAllyList function.
	}
}
